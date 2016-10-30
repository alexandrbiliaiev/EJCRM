var debug = false;
var countriesEU = ['AT', 'BE', 'HR', 'BG', 'CY', 'CZ', 'DK', 'EE', 'FI', 'FR', 'DE', 'GR', 'HU', 'IE',
    'IT', 'LV', 'LT', 'LU', 'MT', 'NL', 'PL', 'PT', 'RO', 'SK', 'SI', 'ES', 'SE', 'GB'];

angular.module('EuroJobsCrm.controllers').controller('TabController', function ($scope, $state, $location, $http, $translate, $mdDialog, dataLoadService) {
    //properties
    $scope.itemsCaptionHide = true;
    $scope.doOrderProgressHide = true;
    $scope.payMethods = {
        paypal: false,
        payu: false,
        bank: false
    };
    //end properties

    //tab serving
    $scope.selectedTab = 0;
    $scope.shopingTabDisabled = true;
    $scope.customerTabDisabled = true;
    $scope.propertyTabDisabled = true;
    $scope.summaryTabDisabled = true;
    $scope.userAgreementAccepted = false;

    $scope.tabClick = function () {
        $scope.summaryTabDisabled = true;
    }

    $scope.goToShopingTab = function () {

        if (!debug && !$scope.commonForm.$valid) {
            return;
        }
        $scope.shopingTabDisabled = false;
        $scope.selectedTab = 1;
    };

    $scope.goToCustomerTab = function () {
        if (!debug && !$scope.shopForm.$valid) {
            return;
        }
        $scope.customerTabDisabled = false;
        $scope.selectedTab = 2;
    };

    $scope.goToPropertyTab = function () {
        if (!debug && !$scope.customerForm.$valid) {
            return;
        }
        $scope.propertyTabDisabled = false;
        $scope.selectedTab = 3;
    };

    $scope.makeOrder = function () {

        if (!debug && !$scope.propertyForm.$valid) {
            return;
        }

        $scope.doOrderProgressHide = false;

        $scope.buildSummaryItems();
        $scope.addVat();

        $scope.doOrder().success(function (response) {
            $scope.orderId = response.orderId;
            $scope.bankTransfer = response.bankTransfer;
            $scope.selectedTab = 4;
            $scope.doOrderProgressHide = true;
            console.log(response);
        }).error(function (response){
            $state.go('error');
        });

        $scope.summaryTabDisabled = false;
    };
    //end tabs serving

    //data model
    $scope.data = {
        lang: '',
        currency: '',
        propertyType: '',
        programVersion: '',
        rooms: '',
        licenseDuration: '1',
        payMethod: '',
        installationCount: 0,
    };

    $scope.customer = {
        name: '',
        surname: '',
        email: '',
        phone: ''
    };

    $scope.company = {
        name: '',
        address: '',
        country: '',
        postalCode: '',
        taxId: ''
    };

    $scope.property = {
        name: '',
        address: '',
        city: '',
        country: '',
        postalCode: '',
        email: '',
        phone: '',
        website: ''
    }
    //end data model

    //controls events
    $scope.languageChange = function () {
        $scope.setLanguageDefaults();
        $translate.use($scope.data.lang);
        $scope.generateItems();
        $scope.calculatePrice();
    }
    $scope.propertyTypeChange = function () {
        $scope.calculatePrice();
    };
    $scope.currencyChange = function () {
        $scope.generateItems();
        $scope.calculatePrice();
    }
    $scope.programVersionChange = function () {
        $scope.generateItems();
        $scope.calculatePrice();
    };
    $scope.roomsCountChange = function () {
        $scope.generateItems();
        $scope.calculatePrice();
    };
    $scope.licenseDurationChange = function () {
        $scope.calculatePrice();
    };

    $scope.itemClick = function (item) {
        if (item.checked && item.isInput) {
            $scope.showInstallationDialog();
        }
        $scope.excludeItems(item);
        $scope.calculatePrice();
    };
    $scope.hideInstallationDialog = function () {
        $scope.calculatePrice();
        $mdDialog.hide();
    }
    $scope.selectPayMethod = function (method) {
        for (i in $scope.payMethods) {
            if (i != method) {
                $scope.payMethods[i] = false;
                continue;
            }

            if (method == 'paypal') {
                $scope.showPaypalMessage();
            }

            $scope.payMethods[i] = true;
            $scope.data.payMethod = i;
        }
        console.log(method);
    }
    $scope.showPaypalMessage = function (ev) {
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title($translate.instant('PAY_PAL_MSG_TITLE'))
                .textContent($translate.instant('PAY_PAL_MSG'))
                .ariaLabel('')
                .ok($translate.instant('OK'))
                .targetEvent(ev)
        );
    };
    $scope.showBankTransfer = function (ev) {
        $mdDialog.show({

            contentElement: '#banktransferDialog',
            parent: angular.element(document.querySelector('#popupContainer')),
            targetEvent: ev,
            clickOutsideToClose: true
        });
    };

    $scope.pay = function () {
        console.log('paying by ' + $scope.data.payMethod);
        switch ($scope.data.payMethod) {
            case 'paypal':
                url = '/payment/paypal?';
                url += 'orderid=' + $scope.orderId;
                url += '&name=' + $scope.programItem.name;
                url += '&amount=' + $scope.totalPriceWithTax * 1.04;
                url += '&currency=' + $scope.data.currency.name;
                url += '&lang=' + $scope.data.lang;

                var win = window.open(url, '_blank');
                win.focus();
                break;
            case 'payu':
                url = '/payment/payu?';
                url += 'orderid=' + $scope.orderId;
                url += '&name=' + $scope.programItem.name;
                url += '&amount=' + $scope.totalPriceWithTax;
                url += '&currency=' + $scope.data.currency.name;
                url += '&firstname=' + $scope.customer.name;
                url += '&lastname=' + $scope.customer.surname;
                url += '&email=' + $scope.customer.email;

                var win = window.open(url, '_blank');
                win.focus();
                break;
            case 'bank':
                $scope.showBankTransfer();
                break;
        }
    }
    //end controls events

    $scope.generateItems = function () {
        if ($scope.data.lang == '' ||
            $scope.data.programVersion == '' ||
            $scope.data.rooms == '' ||
            $scope.data.currency == ''
        ) {
            console.log('Please, select requried fields to generate items');
            return;
        }

        console.log('geneating items...');

        items = $scope.getCurrentItems();

        $scope.shopItems = Object.keys(items).map(function (key) { return items[key] });
        if ($scope.shopItems.length > 0) {
            $scope.itemsCaptionHide = false;
        }

        $scope.programItem = $scope.getCurrentProgram();
    };

    $scope.excludeItems = function (item) {
        id = item.id;
        for (i in $scope.exclusions[id]) {
            for (j = 0; j < $scope.shopItems.length; j++) {
                if ($scope.shopItems[j].id == $scope.exclusions[id][i]) {
                    $scope.shopItems[j].visible = !item.checked;
                    $scope.shopItems[j].checked = false;
                }
            }
        }
    };

    $scope.calculatePrice = function () {
        if ($scope.programItem == undefined) {
            return;
        }

        currencyId = $scope.data.currency.id;
        price = $scope.programItem.prices[currencyId].price;
        prolongationPrice = $scope.programItem.prices[currencyId].priceProlongation;

        //additional modules price calculation
        for (i = 0; i < $scope.shopItems.length; i++) {
            if ($scope.shopItems[i].checked != true) {
                continue;
            }

            multiplier = 1;
            if ($scope.shopItems[i].isInput) {
                multiplier = $scope.data.installationCount;
            }

            price += $scope.shopItems[i].prices[currencyId].price * multiplier;
            prolongationPrice += $scope.shopItems[i].prices[currencyId].priceProlongation;
        }

        // 10% discount for prolongation
        //prolongationPrice *= $scope.data.licenseDuration * 0.9;

        // total price with 10% discount for prolongation
        if ($scope.data.licenseDuration > 1) {
            prolongationPrice = prolongationPrice * ($scope.data.licenseDuration - 1) * 0.9;
            price += prolongationPrice;
        }

        $scope.totalPrice = price;

        //15% discount for hostel with > 16 beds
        if ($scope.data.propertyType === "hostel" && $scope.data.rooms > 16) {
            price *= 0.85;
            prolongationPrice *= 0.85;
        }

        $scope.totalPriceWithDiscount = price;
        $scope.totalPriceStr = $scope.totalPriceWithDiscount.toFixed(2);
        $scope.prolongationPrice = prolongationPrice;
        $scope.prolongationPriceStr = $scope.prolongationPrice.toFixed(2);

    };

    $scope.buildSummaryItems = function () {
        summaryItems = new Array();

        currencyId = $scope.data.currency.id;

        summaryItems.push({
            id: $scope.programItem.id,
            name: $scope.programItem.name,
            price: $scope.programItem.prices[currencyId].price.toFixed(2),
            program: true,
            prolongation: false
        });


        if ($scope.data.licenseDuration > 1) {
            programProlPrice = $scope.programItem.prices[currencyId].priceProlongation * ($scope.data.licenseDuration - 1) * 0.9;
            summaryItems.push({
                id: $scope.programItem.id,
                name: $scope.programItem.name + $translate.instant('PROLONGATION_TEXT'),
                price: programProlPrice.toFixed(2),
                program: true,
                prolongation: true
            });
        }

        items = $scope.shopItems;
        for (i in items) {
            if (!items[i].checked) {
                continue;
            }

            itemName = items[i].name;
            itemPrice = items[i].prices[currencyId].price;

            if (items[i].isInput){
                itemName += ' x ' + $scope.data.installationCount;
                itemPrice *= $scope.data.installationCount;
            }

            summaryItems.push({
                id: items[i].id,
                name: itemName,
                price: itemPrice.toFixed(2),
                module: true,
                prolongation: false
            });

            if ($scope.data.licenseDuration == 1 || items[i].isInput) {
                continue;
            }

            itemProlPrice = items[i].prices[currencyId].priceProlongation * ($scope.data.licenseDuration - 1) * 0.9;
            summaryItems.push({
                name: items[i].name + $translate.instant('PROLONGATION_TEXT'),
                price: itemProlPrice.toFixed(2),
                module: true,
                prolongation: true
            });
        }

        if ($scope.data.propertyType === "hostel" && $scope.data.rooms > 16) {
            discountValue = -1 * $scope.totalPrice * 0.15;
            summaryItems.push({
                name: $translate.instant('HOSTEL_DISCOUNT'),
                price: discountValue.toFixed(2),
                taxDiscount: true
            });
        }

        //added chceking of EU country
        countryCode = $scope.company.country.toUpperCase();
        if ($scope.company.taxId.length == 0 &&
            countriesEU.indexOf(countryCode) > -1) {
            taxValue = $scope.totalPriceWithDiscount * 0.23;
            summaryItems.push({
                name: $translate.instant('SUMMARY_VAT_ITEM'),
                price: taxValue.toFixed(2),
                taxDiscount: true
            });
        }

        $scope.summaryItems = summaryItems;
    };

    $scope.addVat = function () {
        //add chceking of EU country
        countryCode = $scope.company.country.toUpperCase();
        $scope.totalPriceWithTax = $scope.totalPriceWithDiscount;

        if ($scope.company.taxId.length == 0 &&
            countriesEU.indexOf(countryCode) > -1) {

            taxValue = $scope.totalPriceWithDiscount * 0.23;
            $scope.totalPriceWithTax += taxValue;
        }

        $scope.totalPriceWithTaxStr = $scope.totalPriceWithTax.toFixed(2);
    };

    $scope.doOrder = function () {
        order = {
            data: $scope.data,
            items: $scope.summaryItems,
            customer: $scope.customer,
            company: $scope.company,
            property: $scope.property,
            totalPrice: $scope.totalPriceWithTax,
            totalNetPrice: $scope.totalPriceWithDiscount
        };

        return $http({
            url: '/api/shop',
            method: "POST",
            data: order
        });

    };

    $scope.initLanguage = function () {
        var requestedLang = $location.search().lang;
        if (requestedLang === undefined) {
            requestedLang = 'en';
        }
        $scope.data.lang = requestedLang;
        $translate.use(requestedLang);
        $scope.setLanguageDefaults();
    }

    $scope.setLanguageDefaults = function () {
        countryId = 0;
        currencyId = 0;

        for (i in $scope.currencies) {
            if ($scope.languages[i].langCode == $scope.data.lang) {
                countryId = $scope.languages[i].langCountry;
                currencyId = $scope.languages[i].langCurrency;
                break;
            }
        }

        //set default currency for the selected language
        for (i in $scope.currencies) {
            if ($scope.currencies[i].id == currencyId) {
                $scope.data.currency = $scope.currencies[i];
                break;
            }
        }

        //set default country for the selected language
        for (i in $scope.countries) {
            if ($scope.countries[i].id == countryId) {
                $scope.company.country = $scope.countries[i].code;
                $scope.property.country = $scope.countries[i].code;
                break;
            }
        }
    }

    $scope.showInstallationDialog = function () {
        $mdDialog.show({
            controller: function ($scope, $mdDialog) {
                $scope.hide = function () {
                    scopeRef.calculatePrice();
                    $mdDialog.hide();
                };
            },
            contentElement: '#installationCountDialog',
            parent: angular.element(document.querySelector('#popupContainer')),
            clickOutsideToClose: false,
            ok: 'OK'
        });
    }

    
    //dataLoadService.load().success(function (response) {
    //    $scope.languages = response.languages;
    //    $scope.programs = response.programs;
    //    $scope.itemsPack = response.itemsPacks;
    //    $scope.currencies = response.currencies;
    //    $scope.countries = response.countries;
    //    $scope.programsPack = response.programsPacks;
    //    $scope.exclusions = response.exclusions;

    //    $scope.getCurrentItems = function () {
    //        programPacks = $scope.itemsPack[$scope.data.lang].programPacks;
    //        roomsCountPacks = programPacks[$scope.data.programVersion].roomsCountPacks;
    //        return roomsCountPacks[$scope.data.rooms].items;
    //    }

    //    $scope.getCurrentProgram = function () {
    //        programPacks = $scope.programsPack[$scope.data.lang].programPacks;
    //        roomsCountPacks = programPacks[$scope.data.programVersion].roomsCountPacks;
    //        programItem = roomsCountPacks[$scope.data.rooms].items[$scope.data.programVersion];

    //        $scope.selectedProgram = programItem;

    //        return programItem;
    //    }

    //    $scope.initLanguage();

    //}).error(function () {
    //    $state.go('error');
    //});
    //end data loading
});