angular.module('EuroJobsCrm.controllers').controller('ClientManageController', function ($scope, $location, $translate, $http, $state, clientsService, countriesService, contactpersonsService, addressesService, $mdDialog, $routeParams, employeesService) {
    $scope.expandDetails = true;
    $scope.expandContactPersons = true;
    $scope.expandAddresses = true;

    $scope.countries = countriesService.countries;
    $scope.birthdate = null;

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
    $scope.address = $scope.setDefaultAddress();

    $scope.goBack = function () {
        $state.go('clients');
    }

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

    $scope.saveAddressClick = function () {
        if ($scope.addressForm.$invalid) {
            return;
        }

        address = $scope.address;
        address.clientId = $scope.client.id;
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

    $scope.close = function () {
        console.log($scope);
        $mdDialog.hide();
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