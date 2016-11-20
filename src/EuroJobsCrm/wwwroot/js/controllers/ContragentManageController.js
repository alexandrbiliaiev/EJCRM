angular.module('EuroJobsCrm.controllers').controller('ContragentManageController', function ($scope, $location, $translate, $http, $state, contragentsService, countriesService, contactpersonsService, addressesService, $mdDialog, $routeParams, employeesService) {
    $scope.expandDetails = true;
    $scope.expandContactPersons = true;
    $scope.expandAddresses = false;
    $scope.expandEmployees = false;
    $scope.countries = countriesService.countries;
    $scope.birthdate = null;
    $scope.showSearch = false;
    $scope.expandNotes = false;


    $scope.goBack = function () {
        $state.go('contragents');
    }

    $scope.close = function () {
        console.log($scope);
        $mdDialog.hide();
    }

    //contragents
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

    $scope.showEditContragentDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/contragents/contragent_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }
    //End Contragents

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
        addressesService.saveAddress(address).success(function (response) {
            if (address.id == 0) {
                $scope.contragent.addresses.push(response);
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

                addresses = $scope.contragent.addresses;
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



    //ContactPersons
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
            messanger: "",
            status: 1
        }
    }

    $scope.contactperson = $scope.setDefaultContactPerson();

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
    //End ContactPersons


    //Employees
    $scope.setDefaultEmployee = function () {
        return {
            id: 0,
            firstName: null,
            middleName: null,
            lastName: null,
            birthdate: null,
            description: null
        }
    }

    $scope.employee = $scope.setDefaultEmployee();

    $scope.saveEmployeeClick = function () {
        if ($scope.employeeForm.$invalid) {
            return;
        }

        employee = {
            id: $scope.employee.id,
            contragentId: $scope.contragent.id,
            firstName: $scope.employee.firstName,
            middleName: $scope.employee.middleName,
            lastName: $scope.employee.lastName,
            birthDate: $scope.birthdate,
            description: $scope.employee.description,
            status: $scope.employee.status
        }

        employeesService.saveEmployee(employee).success(function (response) {
            if ($scope.employee.id == 0 || $scope.employee.id == undefined) {
                $scope.contragent.employees.push(response);
            }

            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.showNewEmployeeDialog = function (ev) {
        $scope.employee = $scope.setDefaultEmployee();
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/employees/employee_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })

            .then(function (answer) {

            }, function () {

            });
    }

    $scope.editEmployee = function (employee) {
       employeesService.setCurrentEmployee(employee);
       $state.go('employee', {
            id: employee.id
        });
    }

    $scope.showDeleteEmployeeConfirmDialog = function (employeeId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('EMP_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('EMP_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            employeesService.deleteEmployee(employeeId).success(function (response) {
                if (response != true) {
                    return;
                }

                employees = $scope.contragent.employees;
                for (i in employees) {
                    if (employees[i].id != employeeId) {
                        continue;
                    }

                    employees.splice(i, 1);
                    return;
                }

            }).error(function (response) {
                $state.go('error');
            });
        }, function () {

        });
    };
    //End Employees

    $scope.getFormatedDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    };

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