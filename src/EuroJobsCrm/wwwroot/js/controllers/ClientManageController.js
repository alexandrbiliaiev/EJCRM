angular.module('EuroJobsCrm.controllers').controller('ClientManageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService, employeesService, fileService) {

        $scope.moment = moment;

        $scope.userRole = $cookies.get('user_role');
        $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
        $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user';
        $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user' || $scope.userRole == 'Normal user';
        $scope.detailClaim = $scope.addClaim;
        $scope.Saving = false;

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

        $scope.isActive = false;

        clientsService.loadClient($state.params.id).success(function (response) {
            $scope.client = response;
            $scope.isActive = true;
        }).error(function () {
            $state.go('error');
        });

        $scope.goBack = function () {
            $state.go('clients');
        }

        $scope.viewEmployeeDatails = function (employeeId) {
            $state.go('employee', {
                id: employeeId
            });
        }

        $scope.close = function () {
            console.log($scope);
            $mdDialog.hide();
        }

        $scope.goToOfferDetails = function (offerId) {
            $state.go('offer', {
                id: offerId
            });
        }

        //clients
        $scope.saveClientClick = function () {
            if ($scope.clientForm.$invalid) {
                return;
            }

            client = $scope.client;
            $scope.Saving = true;

            clientsService.saveClient(client).success(function (response) {
                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });
        }

        $scope.showEditClientDialog = function (ev) {
            $scope.Saving = false;
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
            $scope.Saving = true;
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
            $scope.Saving = false;
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
            $scope.Saving = false;
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
            $scope.Saving = false;
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

            $scope.Saving = true;

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
            $scope.Saving = false;
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
            $scope.Saving = false;
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
            $scope.Saving = false;
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
            $scope.Saving = true;
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
            $scope.Saving = false;
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
            $scope.Saving = false;
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
            $scope.Saving = false;
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

        $scope.showNewFileDialog = function () {
            $scope.Saving = false;
            $scope.file = {
                name: '',
                description: '',
                contragentId: $scope.client.id
            };
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/files/file_dialog.html',
                clickOutsideToClose: true,
            })

            .then(function (answer) {

            }, function () {

            });
        }

        $scope.processFileForm = function () {

            var data = new FormData();
            data.append("file", $scope.newFile);
            data.append("name", $scope.file.name);
            data.append("description", $scope.file.description);
            data.append("ownerId", $scope.client.id);
            data.append("ownerType", "client");

            fileService.saveFile(data).success(function (data) {
                if (!data.success) {
                    alert(data.errorMessage);

                } else {
                    $scope.client.files.push(data);
                }

                $mdDialog.hide();
            });
        }

        $scope.showDeleteFileConfirmDialog = function (fileId) {
            $scope.Saving = false;
            var confirm = $mdDialog.confirm()
                .title($translate.instant('FILE_DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('FILE_DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                fileService.deleteFile(fileId).success(function (response) {
                    if (response != true) {
                        return;
                    }

                    files = $scope.client.files;
                    for (i in files) {
                        if (files[i].id != fileId) {
                            continue;
                        }

                        files.splice(i, 1);
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

    });