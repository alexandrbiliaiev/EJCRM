angular.module('EuroJobsCrm.controllers').controller('ContragentManageController', function ($scope, $location, $translate, $http, $state, contragentsService, contactpersonsService, $mdDialog, $routeParams) {
    $scope.expandDetails = true;
    $scope.expandContactPersons = true;
    $scope.expandAddresses = true;
    $scope.expandEmployees = true;

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

    $scope.goBack = function () {
        $state.go('contragents');
    }

    $scope.saveContragentClick = function () {
        if ($scope.contragentForm.$invalid) {
            return;
        }

        contragent = {
            id: $scope.contragent.id,
            name: $scope.contragent.name,
            licenseNumber: $scope.contragent.licenseNumber,
            status: $scope.contragent.status
        };

        contragentsService.saveContragent(contragent).success(function (response) {
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

        contactperson = {
            id: $scope.contactperson.id,
            contragentId: $scope.contragent.id,
            clientId: $scope.contragent.clientId,
            name: $scope.contactperson.name,
            surname: $scope.contactperson.surname,
            position: $scope.contactperson.position,
            email: $scope.contactperson.email,
            phoneNumber: $scope.contactperson.phoneNumber,
            skype: $scope.contactperson.skype,
            messanger: $scope.contactperson.messanger
        }

        contactpersonsService.saveContactPerson(contactperson).success(function (response) {
            if ($scope.contactperson.id == 0 || $scope.contactperson.id == undefined) {
                $scope.contragent.contactPersons.push(response);
            }

            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });

        $scope.close = function () {
            console.log($scope);
            $mdDialog.hide();
        }
    }

    $scope.showEditContragentDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/contragent_add_tmpl.html',
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
            templateUrl: '/templates/contragent_add_contact_person_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }

    $scope.showDeleteCtpConfirmDialog = function (contactPersonId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('CTG_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('CTG_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            contactpersonsService.deleteContactPerson(contactPersonId).success(function (response) {
                if (response != true) {
                    return;
                }

                contactpersons = $scope.contragent.contactPersons;
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

    $scope.showEditContactPersonDialog = function (contactPerson) {
        $scope.contactperson = contactPerson;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/contragent_add_contact_person_tmpl.html',
            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }



    if (contragentsService.contragents != undefined) {
        $scope.contragent = contragentsService.getContragent($state.params.id);
        return;
    }

    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
        $scope.contragent = contragentsService.getContragent($state.params.id);
    }).error(function () {
        $state.go('error');
    });

});