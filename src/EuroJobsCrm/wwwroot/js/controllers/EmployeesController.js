angular.module('EuroJobsCrm.controllers').controller('EmployeesController', function ($scope, $location, $http, $state, $translate, $mdDialog, $cookies, employeesService) {
    $scope.employees = [];

    $scope.moment = moment;

  /*  $scope.getDefaultEmployee = function () {
        return  {
            id: 0,
            krs: "",
            name: "",
            nip: "",
            regon: ""
        }
    }

    $scope.employee = $scope.getDefaultEmployee();*/

    $scope.editEmployee = function (employeeId) {
        $state.go('employee', {
            id: employeeId
        });
    }

    $scope.saveEmployeeClick = function () {
        if ($scope.employeeForm.$invalid) {
            return;
        }

        employee = $scope.employee;

        employeesService.saveemployee(employee).success(function (response) {
            employeesService.employees.push(response);
            $scope.employee = $scope.getDefaultemployee();
            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }


    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.showAddEmployeeDialog = function (ev) {
        employee = $scope.getDefaultemployee();
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

    $scope.showDeleteEmployeeConfirmDialog = function (employeeId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('CLI_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('CLT_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            employeesService.deleteemployee(employeeId).success(function (response) {
                if (response != true) {
                    return;
                }

                employees = employeesService.employees;
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

    if (employeesService.employees != undefined) {
        $scope.employees = employeesService.employees;
        return;
    }

    employeesService.load().success(function (response) {
        employeesService.employees = response;
        $scope.employees = employeesService.employees;
    }).error(function () {
        $state.go('error');
    });

    $scope.userRole = $cookies.get('user_role');
    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin'  || $scope.userRole == 'Advanced user';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin'  || $scope.userRole == 'Advanced user' || $scope.userRole == 'Normal user';
 
   
});