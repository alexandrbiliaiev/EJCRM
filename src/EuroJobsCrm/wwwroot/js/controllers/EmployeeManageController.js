angular.module('EuroJobsCrm.controllers').controller('EmployeeManageController', function ($scope, $rootScope, $location, $translate, $http, $state, countriesService, employeesService,
    $mdDialog, $routeParams, Upload, $timeout) {

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

    $scope.saveDocumentClick = function () {
        if ($scope.documentForm.$invalid) {
            return;
        }

        employeesService.saveDocument($scope.document).success(function (response) {
            if ($scope.document.id== 0 || $scope.document.id == undefined) {
                $scope.employee.documents.push(response);
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

    employeeId = $state.params.id;

    employeesService.getEmployeeFromDb(employeeId).success(function (response) {
        $scope.employee = response;
        $scope.employeeBirthday = moment(response.birthDate).format('DD-MM-YYYY');
    }).error(function () {
        $state.go('error');
    });

    
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
                $scope.document.files.push({url : response.data, name : file.name});
                file.result = response.data;
            });
        }, function (response) {
            if (response.status > 0)
                $scope.errorMsg = response.status + ': ' + response.data;
        }, function (evt) {
            // Math.min is to fix IE which reports 200% sometimes
            $scope.sendProgress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));

        });
    }


});