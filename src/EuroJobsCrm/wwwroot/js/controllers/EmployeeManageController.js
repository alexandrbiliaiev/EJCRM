angular.module('EuroJobsCrm.controllers').controller('EmployeeManageController', function ($scope, $location, $translate, $http, $state, countriesService, employeesService,
    $mdDialog, $routeParams) {
    
    $scope.expandDetails = true;
    $scope.expandDocs = true;
    
    $scope.moment = moment;

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

    $scope.close = function () {
        $mdDialog.hide();
    }

    employeeId = $state.params.id;
   
    employeesService.getEmployeeFromDb(employeeId).success(function (response) {
        $scope.employee = response;
        $scope.employeeBirthday = moment(response.birthDate).format('DD-MM-YYYY');
    }).error(function () {
        $state.go('error');
    });
    
});