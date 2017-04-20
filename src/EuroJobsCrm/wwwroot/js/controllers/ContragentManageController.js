angular.module('EuroJobsCrm.controllers').controller('ContragentManageController',
    function ($scope, $location, $translate, $http, $state, contragentsService, $cookies,
        countriesService, contactpersonsService, addressesService, $mdDialog, $routeParams, employeesService, usersService,
        fileService, calendarService) {

        $scope.user = usersService.getUser();

        $scope.expandDetails = false;
        $scope.expandContactPersons = false;
        $scope.expandAddresses = false;
        $scope.expandEmployees = false;
        $scope.expandResponsiblePerson = false;
        $scope.expandNotes = false;
        $scope.countries = countriesService.countries;
        $scope.birthdate = null;
        $scope.showSearch = false;
        $scope.users = new Array();
        $scope.responsibleUsers = new Array();
        $scope.responsibleUserName = null;
        $scope.isActive = false;
        $scope.Saving = false;
        $scope.expandContragentUsers = true;

        $scope.showEmployeeSearch = false;



        $scope.userRole = $cookies.get('user_role');
        $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'CONTRAGENT';
        $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user' | $scope.userRole == 'CONTRAGENT';
        $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user' || $scope.userRole == 'Normal user' | $scope.userRole == 'CONTRAGENT';
        $scope.detailClaim = $scope.addClaim;
        $scope.isContragent = $scope.userRole == 'CONTRAGENT';

        $scope.moment = moment;

        contragentsService.getContragent($state.params.id).success(function (response) {
            if (!response.success) {
                $state.go('denied');
            }

            $scope.contragent = response;

            for (var i in $scope.responsibleUsers) {
                if ($scope.responsibleUsers[i].id == $scope.contragent.responsibleUser.id) {
                    $scope.responsibleUserName = $scope.responsibleUsers[i].name;
                }
            }

            $scope.isActive = true;
        }).error(function () {
            $state.go('error');
        });

        $scope.focusOnSearch = function () {
            document.getElementById("search").focus();
        };

        $scope.goBack = function () {
            $state.go('contragents');
        }

        $scope.close = function () {
            console.log($scope);
            $mdDialog.hide();
        }


        //users
        $scope.saveContragentUser = function () {
            if ($scope.userForm.$invalid) {
                return;
            }
            $scope.Saving = true;
            contragentUser = {
                userName: $scope.user.userName,
                name: $scope.user.userName,
                password: $scope.user.password,
                email: $scope.user.email,
                ctgId: $scope.contragent.id,
            };

            contragentsService.AddContragentUser(contragentUser, $scope.contragent.id).success(function (response) {
                $scope.Saving = false;
                $mdDialog.hide();
                $scope.contragent.contragentUsers.push(contragentUser);
            }).error(function () {
                $scope.Saving = false;
                $state.go('error');
                $mdDialog.hide();
            });
        }

        //users
        $scope.showAddContragentUserDialog = function (ev) {
            $scope.Saving = false;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/contragents/contragent_user_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            })
                .then(function (answer) {

                }, function () {

                });
        }



        //contragents
        $scope.saveContragentClick = function () {
            if ($scope.contragentForm.$invalid) {
                return;
            }
            $scope.Saving = true;
            contragent = {
                id: $scope.contragent.id,
                name: $scope.contragent.name,
                licenseNumber: $scope.contragent.licenseNumber,
                status: $scope.contragent.status,
                inn: $scope.contragent.inn,
                subscription: $scope.contragent.subscription
            };

            contragentsService.saveContragent(contragent).success(function (response) {
                $scope.Saving = false;
                $mdDialog.hide();
            }).error(function () {
                $scope.Saving = false;
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
            $scope.Saving = true;
            address = $scope.address;
            addressesService.saveAddress(address).success(function (response) {
                if (address.id == 0) {
                    $scope.contragent.addresses.push(response);
                }
                $scope.address = $scope.setDefaultAddress();
                $scope.Saving = false;
                $mdDialog.hide();
            }).error(function () {
                $scope.Saving = false;
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
            $scope.Saving = true;
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
                $scope.Saving = false;
                $mdDialog.hide();
            }).error(function () {
                $scope.Saving = false;
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
            $scope.Saving = true;
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
                $scope.Saving = false;
                $mdDialog.hide();
            }).error(function () {
                $scope.Saving = false;
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

        $scope.showNewFileDialog = function () {
            $scope.file = {
                name: '',
                description: '',
                contragentId: $scope.contragent.id
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
            data.append("ownerId", $scope.contragent.id);
            data.append("ownerType", "contragent");
            $scope.Saving = true;
            fileService.saveFile(data).success(function (data) {
                if (!data.success) {
                    alert(data.errorMessage);
                    $scope.Saving = false;

                } else {
                    $scope.contragent.files.push(data);
                    $scope.Saving = false;
                }

                $mdDialog.hide();
            });
        }

        $scope.showDeleteFileConfirmDialog = function (fileId) {
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

                    files = $scope.contragent.files;
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

        $scope.showAddResponsiblePersonDialog = function (ev) {
            $scope.employee = $scope.setDefaultEmployee();
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/contragents/contragent_responsiblePerson_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            })
                .then(function (answer) {

                }, function () {


                });
        }

        $scope.saveResponsiblePersoon = function (usr) {

            if ($scope.responsiblePersonForm.$invalid) {
                return;
            }
            $scope.Saving = true;
            param = {
                contragentId: $scope.contragent.id,
                userId: usr.id
            };

            contragentsService.addResponsiblePersonToContragent(param).success(function (response) {
                $scope.Saving = false;
                $mdDialog.hide();
                $scope.contragent.responsibleUser = usr;
            }).error(function () {
                $scope.Saving = false;
                $state.go('error');
                $mdDialog.hide();
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

        $scope.addNoteDialog = function () {
            $scope.currentEvent = {
                start: new DayPilot.Date(new Date()),
                end: new DayPilot.Date(new Date()),
                id: DayPilot.guid(),
                text: "",
                startDate: new Date(),
                endDate: new Date(),
                contragentId: $scope.contragent.id,
                clientName: $scope.contragent.name,
            };

            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/calendar/event_tmpl.html',
                clickOutsideToClose: true,
            })
                .then(function (answer) {

                }, function () {

                });

        }

        $scope.editNoteDialog = function (note) {
            note.startDate = new Date(note.startDate).addHours(-1);
            note.endDate = new Date(note.endDate).addHours(-1);
            note.remindDate = new Date(note.remindDate).addHours(-1);
            $scope.currentEvent = note;

            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/calendar/event_tmpl.html',
                clickOutsideToClose: true,
            })
                .then(function (answer) {

                }, function () {

                });

        }

        $scope.deleteNoteDialog = function (noteId) {
            $scope.Saving = false;
            var confirm = $mdDialog.confirm()
                .title($translate.instant('DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                calendarService.deleteEvent(noteId).success(function (response) {
                    for (var i = 0; i < $scope.contragent.notes.length; i++) {
                        if ($scope.contragent.notes[i].id == noteId) {
                            $scope.contragent.notes.splice(i, 1);
                            break;
                        }
                    }

                    $scope.Saving = false;
                    $mdDialog.hide();
                }).error(function () {
                    $state.go('error');
                    $scope.Saving = false;
                    $mdDialog.hide();
                })
            }, function () {

            });
        }

        $scope.saveEventClick = function () {
            if ($scope.eventForm.$invalid) {
                return;
            }

            $scope.currentEvent.startDate = $scope.currentEvent.startDate.normalize();
            $scope.currentEvent.endDate = $scope.currentEvent.endDate.normalize();
            if ($scope.currentEvent.remindDate != undefined) {
                $scope.currentEvent.remindDate = $scope.currentEvent.remindDate.normalize();
            }

            $scope.Saving = true;
            calendarService.saveEvent($scope.currentEvent).success(function (response) {
                if (!$scope.contragent.notes.ContainsId(response.id)) {
                    $scope.contragent.notes.push(response);
                } else {
                    for (var i = 0; i < $scope.client.notes.length; i++) {
                        if ($scope.contragent.notes[i].id == response.id) {
                            $scope.contragent.notes.splice(i, 1);
                            $scope.contragent.notes.push(response);
                            break;
                        }
                    }
                }

                $scope.Saving = false;
                $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                $scope.Saving = false;
                $mdDialog.hide();
            })

        }

        $scope.showEditUserDialog = function (user) {
            $scope.userEditMode = true;
            $scope.user = user;
            $scope.addingUserMethod = usersService.EditUser;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/users/user_dialog_tmpl.html',
                clickOutsideToClose: true,
            });
        }

        $scope.showResetPassConfirmDialog = function (userId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('RESET_PASS_CONFIRM_TITLE'))
                .textContent($translate.instant('RESET_PASS_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('YES'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                usersService.resetPasswordForUser(userId).success(function (response) {
                    message = response.success ? $translate.instant('RESET_PASS_SUCCESS') : response.errorMessage;
                    $mdDialog.show($mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title($translate.instant('RESET_PASS_CONFIRM_TITLE'))
                        .textContent(message)
                        .ariaLabel('Message')
                        .ok('OK'));

                }).error(function (response) {
                    $state.go('error');
                });
            }, function () {

            });
        };

        $scope.saveUserClick = function () {
            if ($scope.user.name.length == 0) {
                return;
            }

            $scope.Saving = true;
            usersService.EditUser($scope.user).success(function (response) {
                if (response.success != true) {
                    $mdDialog.show(
                        $mdDialog.alert()
                            //.parent(angular.element(document.querySelector('#popupContainer')))
                            .clickOutsideToClose(true)
                            .title($translate.instant('ADDING_USER_ERROR'))
                            .textContent(response.errorMessage)
                            .ariaLabel('Error dialog')
                            .ok('OK')

                    );
                    $mdDialog.hide();
                    return;
                }

                /*
                $scope.reloadUsers();
                $scope.contragent.contragentUsers
                $scope.user = usersService.getUser();
                */

                $mdDialog.hide();
                $scope.Saving = false;

            }).error(function () {
                $state.go('error');
                $mdDialog.hide();
            });
        }

        $scope.showDeleteUserDialog = function (userId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('DELETE_USER'))
                .textContent($translate.instant('DELETE_USER_CONFIRM_TEXT'))
                .ok($translate.instant('YES'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function () {
                usersService.deleteUser(userId).success(function (response) {
                    var users = $scope.contragent.contragentUsers;

                    for (i in users) {
                        if (users[i].id == userId) {
                            users.splice(i, 1);
                            break;
                        }
                    }
                }).error(function () {
                    $state.go('error');
                });
            }, function () {

            });
        }
        //data loading

        $scope.getFormatedDate = function (date) {
            return moment(date).format('YYYY-MM-DD');
        };

        usersService.GetResponsibleUsersList().success(function (response) {
            $scope.responsibleUsers = response;

        }).error(function () {
            $state.go('error');
        });

        usersService.load().success(function (response) {

            usersService.setAdmins(response['Admin']);
            usersService.setAdvancedUsers(response['Advanced user']);
            usersService.setNormalUsers(response['Normal user']);

            $scope.admins = usersService.admins;
            $scope.advancedUsers = usersService.advancedUsers;
            $scope.normalUsers = usersService.normalUsers;

            for (i in $scope.admins) {
                $scope.users.push($scope.admins[i]);
            }

            for (i in $scope.advancedUsers) {
                $scope.users.push($scope.advancedUsers[i]);
            }

            for (i in $scope.normalUsers) {
                $scope.users.push($scope.normalUsers[i]);
            }

        }).error(function () {
            $state.go('error');
        });
    });