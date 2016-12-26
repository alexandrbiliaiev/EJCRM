angular.module('EuroJobsCrm.controllers').controller('ClientManageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService, employeesService) {

        $scope.userRole = $cookies.get('user_role');
        $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
        $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User';
        $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User' || $scope.userRole == 'Normal User';

        $scope.expandDetails = false;
        $scope.expandContactPersons = false;
        $scope.expandAddresses = false;
        $scope.expandDocuments = true;
        $scope.expandEmployees = true;
        $scope.expandPayments = true;
        $scope.expandNotes = true;
        $scope.expandOffers = true;
        $scope.filteredEmployees;

        $scope.countries = countriesService.countries;
        $scope.birthdate = null;

        $scope.goBack = function () {
            $state.go('clients');
        }

        $scope.close = function () {
            console.log($scope);
            $mdDialog.hide();
        }

        //clients
        $scope.saveClientClick = function () {
            if ($scope.clientForm.$invalid) {
                return;
            }

            client = $scope.client;

            clientsService.saveClient(client).success(function (response) {
                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });
        }

        $scope.showEditClientDialog = function (ev) {
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/clients/client_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            })
                .then(function (answer) {

                }, function () {

                });
        }
        //End clients


        //Addresses
        $scope.setDefaultAddress = function () {
            return {
                address: "",
                city: "",
                contragentId: $state.params.id,
                country: "PL",
                id: 0,
                pay: "0",
                postCode: "",
                type: "1"
            }
        }

        $scope.address = $scope.setDefaultAddress();

        $scope.saveAddressClick = function () {
            if ($scope.addressForm.$invalid) {
                return;
            }

            address = $scope.address;
            address.clientId = $scope.client.id;
            address.contragentId = 0;
            addressesService.saveAddress(address).success(function (response) {
                if (address.id == 0) {
                    $scope.client.addresses.push(response);
                }
                $scope.address = $scope.setDefaultAddress();
                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });

        }

        $scope.showNewAddressDialog = function (ev) {
            $scope.address = $scope.setDefaultAddress();

            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/addresses/address_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            }).then(function (answer) {

            }, function () {

            });
        }

        $scope.showEditAddressDialog = function (address) {
            $scope.address = address;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/addresses/address_dialog_tmpl.html',

                clickOutsideToClose: true,
            })

                .then(function (answer) {

                }, function () {

                });
        }

        $scope.showDeleteAddressConfirmDialog = function (addresId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('ADDRESS_DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('ADDRESS_DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                addressesService.deleteAddress(addresId).success(function (response) {
                    if (response != true) {
                        return;
                    }

                    addresses = $scope.client.addresses;
                    for (i in addresses) {
                        if (addresses[i].id != addresId) {
                            continue;
                        }

                        addresses.splice(i, 1);
                        return;
                    }

                }).error(function (response) {
                    $state.go('error');
                });
            }, function () {

            });
        };
        //End Addresses


        //Contact Persons
        $scope.setDefaultContactPerson = function () {
            return {
                id: 0,
                contragentId: null,
                clientId: null,
                name: "",
                surname: "",
                position: "",
                email: "",
                phoneNumber: "",
                skype: "",
                messanger: ""
            }
        }

        $scope.contactperson = $scope.setDefaultContactPerson();

        $scope.saveContactPersonClick = function () {
            if ($scope.contactpersonForm.$invalid) {
                return;
            }

            contactperson = $scope.contactperson;
            contactperson.clientId = $scope.client.id;
            contactpersonsService.saveContactPerson(contactperson).success(function (response) {
                if ($scope.contactperson.id == 0 || $scope.contactperson.id == undefined) {
                    $scope.client.contactPersons.push(response);
                }

                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });
        }

        $scope.showNewContactPersonDialog = function (ev) {
            $scope.contactperson = $scope.setDefaultContactPerson();
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/contactpersons/contact_person_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            })

                .then(function (answer) {

                }, function () {

                });
        }

        $scope.showEditContactPersonDialog = function (contactPerson) {
            $scope.contactperson = contactPerson;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/contactpersons/contact_person_dialog_tmpl.html',
                clickOutsideToClose: true,
            })

                .then(function (answer) {

                }, function () {

                });
        }

        $scope.showDeleteCtpConfirmDialog = function (contactPersonId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('CPN_DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('CPN_DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                contactpersonsService.deleteContactPerson(contactPersonId).success(function (response) {
                    if (response != true) {
                        return;
                    }

                    contactpersons = $scope.client.contactPersons;
                    for (i in contactpersons) {
                        if (contactpersons[i].id != contactPersonId) {
                            continue;
                        }

                        contactpersons.splice(i, 1);
                        return;
                    }

                }).error(function (response) {
                    $state.go('error');
                });
            }, function () {

            });
        };
        //End Contact Persons

        //Offers
        $scope.setDefaultOffer = function () {
            return {
                id: 0,
                accomodationPrice: 0,
                accomodationType: 0,
                additionalInfo: null,
                advanceAmount: null,
                ageFrom: "20",
                ageTo: "60",
                branch: 1,
                clientId: 0,
                comments: null,
                contractType: 1,
                distanceToWork: null,
                documents: null,
                education: 1,
                endingDate: null,
                experience: null,
                facilities: null,
                hoursPerMonth: "160",
                languages: null,
                overtimeRate: null,
                paymentMethod: 1,
                ratePerHour: null,
                ratePerMonth: null,
                responsibilities: null,
                roomPeopleNumber: 0,
                startingDate: null,
                transportPrice: null,
                transportToWork: null,
                vacanciesNumber: null,
                workEnd: null,
                workPlace: null,
                workStart: null,
                workMo: true,
                workTu: true,
                workWe: true,
                workTh: true,
                workFr: true,
                workSa: false,
                workSu: false,
                position: null,
                gender: 0
            }
        }

        $scope.offer = $scope.setDefaultOffer();

        $scope.saveOfferClick = function () {
            if ($scope.offerForm.$invalid) {
                return;
            }

            offer = $scope.offer;
            offer.clientId = $scope.client.id;
            offersService.saveOffer(offer).success(function (response) {
                if (offer.id == 0) {
                    $scope.client.offers.push(response);
                }
                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });

        }

        $scope.showNewOfferDialog = function (ev) {
            $scope.offer = $scope.setDefaultOffer();

            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/offers/offer_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: false,
            }).then(function (answer) {

            }, function () {

            });
        }

        $scope.showEditOfferDialog = function (offer) {
            $scope.offer = offer;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/offers/offer_dialog_tmpl.html',

                clickOutsideToClose: false,
            })

                .then(function (answer) {

                }, function () {

                });
        }

        $scope.showDeleteOfferConfirmDialog = function (offId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('OFFER_DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('OFFER_DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                offersService.deleteOffer(offId).success(function (response) {
                    if (response != true) {
                        return;
                    }

                    offers = $scope.client.offers;
                    for (i in offers) {
                        if (offers[i].id != offId) {
                            continue;
                        }

                        offers.splice(i, 1);
                        return;
                    }

                }).error(function (response) {
                    $state.go('error');
                });
            }, function () {

            });
        };

        $scope.disableEndDate = function (isUnlimited) {
            if (isUnlimited) {
                $scope.endingDateBeforeChange = $scope.offer.endingDate;
                $scope.offer.endingDate = null;
            }
            else {
                $scope.offer.endingDate = $scope.endingDateBeforeChange;
            }
        }

        $scope.disableOvertime = function (overtimeDisabled) {
            if (overtimeDisabled) {
                $scope.overtimeRateOld = $scope.offer.overtimeRate;
                $scope.offer.overtimeRate = "0";
            }
            else {
                $scope.offer.overtimeRate = $scope.overtimeRateOld;
            }
        }

        $scope.disableAdvance = function (advanceEnabled) {
            if (advanceEnabled) {
                $scope.advanceAmountOld = $scope.offer.advanceAmount;
                $scope.offer.advanceAmount = null;
            }
            else {
                $scope.offer.advanceAmount = $scope.advanceAmountOld;
            }
        }

        $scope.disableFreeAccomodation = function (accomodationFreeEnabled) {
            if (accomodationFreeEnabled) {
                $scope.accomodationPriceOld = $scope.offer.accomodationPrice;
                $scope.offer.accomodationPrice = 0;
            }
            else {
                $scope.offer.accomodationPrice = $scope.accomodationPriceOld;
            }
        }


        //End Offers


        if (clientsService.clients != undefined) {
            $scope.client = clientsService.getClient($state.params.id);
            return;
        }

        clientsService.load().success(function (response) {
            clientsService.clients = response;
            $scope.client = clientsService.getClient($state.params.id);
        }).error(function () {
            $state.go('error');
        });





    });