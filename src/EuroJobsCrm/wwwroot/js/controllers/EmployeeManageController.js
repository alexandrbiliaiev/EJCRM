angular.module('EuroJobsCrm.controllers').controller('EmployeeManageController', function ($scope, $rootScope, $location, $translate, $http, $state, countriesService, employeesService,
    $mdDialog, $routeParams, Upload, $timeout, $cookies) {

    $scope.userRole = $cookies.get('user_role');
    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User' || $scope.userRole == 'Normal User';

    $scope.expandDetails = true;
    $scope.expandDocs = true;

    $scope.moment = moment;
    $scope.sendProgress = 0;


    $scope.showEditEmployeeDialog = function () {
        $scope.birthdate = new Date($scope.employee.birthDate);
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/employees/employee_dialog_tmpl.html',

            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }

    $scope.showAddDocumentDialog = function () {
        $scope.document = employeesService.getDefaultDocument();
        $scope.document.employeeId = $scope.employee.id;
        $scope.docFile = undefined;
        $scope.sendProgress = 0;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/documents/document_dialog_tmpl.html',

            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }

    $scope.showEditDocumentDialog = function (document) {
        $scope.document = document;
        $scope.document.validFrom = new Date($scope.document.validFrom);
        $scope.document.validTo = new Date($scope.document.validTo);
        $scope.document.issueDate = new Date($scope.document.issueDate);


        $scope.docFile = undefined;
        $scope.sendProgress = 0;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/documents/document_dialog_tmpl.html',

            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    }

    $scope.showDeleteDocFileConfirmDialog = function (fileId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('DOC_FILE_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('DOC_FILE_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            employeesService.deleteDocumentFile(fileId).success(function (response) {
                if (response == false) {
                    return;
                }

                documents = $scope.employee.identityDocuments;
                for (i in documents) {
                    files = documents[i].files;
                    for (j in files) {
                        if (files[j].id != fileId) {
                            continue;
                        }

                        files.splice(j, 1);
                        return;
                    }
                }

            }).error(function (response) {
                $state.go('error');
            });
        }, function () {

        });
    }

    $scope.showDeleteDocConfirmDialog = function (docId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('DOC_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('DOC_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            employeesService.deleteDocument(docId).success(function (response) {
                if (response == false) {
                    return;
                }

                documents = $scope.employee.identityDocuments;
                for (i in documents) {

                    if (documents[i].id != docId) {
                        continue;
                    }

                    documents.splice(i, 1);
                    return;

                }

            }).error(function (response) {
                $state.go('error');
            });
        }, function () {

        });
    }

    $scope.saveEmployeeClick = function () {
        if ($scope.employeeForm.$invalid) {
            return;
        }

        employee = {
            id: $scope.employee.id,
            contragentId: $scope.employee.contragentId,
            firstName: $scope.employee.firstName,
            middleName: $scope.employee.middleName,
            lastName: $scope.employee.lastName,
            birthDate: $scope.birthdate,
            description: $scope.employee.description,
            status: $scope.employee.status,
            cltId: $scope.employee.cltId,
            offId: $scope.employee.offId
        }

        employeesService.saveEmployee(employee).success(function (response) {
            if ($scope.employee.id == 0 || $scope.employee.id == undefined) {
                $scope.contragent.employees.push(response);
            } else {
                for (i in employeesService.employees) {
                    if (employeesService.employees[i].id == response.id) {
                        for (j in employeesService.employees[i])
                            employeesService.employees[i][j] = response[j];
                    }
                }

            }



            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.saveDocumentClick = function () {
        if ($scope.documentForm.$invalid) {
            return;
        }

        employeesService.saveDocument($scope.document).success(function (response) {
            if ($scope.document.id == 0 || $scope.document.id == undefined) {
                $scope.employee.identityDocuments.push(response);
            } else {
                for (i in $scope.document)
                    $scope.document[i] = response[i];
            }

            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.goBack = function () {
        $state.go($rootScope.previousState, $rootScope.previousStateParams);
    }

    $scope.uploadFile = function (file) {
        $scope.sendProgress = 0;
        file.upload = Upload.upload({
            url: 'api/Files/Upload',
            data: {
                file: file
            },
        });

        file.upload.then(function (response) {
            $timeout(function () {
                $scope.document.files.push({ url: response.data, name: file.name });
                file.result = response.data;
                $scope.docFile = undefined;
                $scope.sendProgress = 0;
            });
        }, function (response) {
            if (response.status > 0)
                $scope.errorMsg = response.status + ': ' + response.data;
        }, function (evt) {
            // Math.min is to fix IE which reports 200% sometimes
            $scope.sendProgress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));

        });
    }

    employeeId = $state.params.id;

    employeesService.getEmployeeFromDb(employeeId).success(function (response) {
        $scope.employee = response;
        $scope.employeeBirthday = moment(response.birthDate).format('DD-MM-YYYY');
    }).error(function () {
        $state.go('error');
    });

});