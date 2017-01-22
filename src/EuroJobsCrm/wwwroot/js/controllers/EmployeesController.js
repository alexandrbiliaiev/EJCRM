angular.module('EuroJobsCrm.controllers').controller('EmployeesController', function($scope, $location, $http, $state, $translate, $mdDialog, $cookies, employeesService) {
    $scope.employees = [];

    $scope.userRole = $cookies.get('user_role');
    $scope.ctgId = $cookies.get('ctg_id');

    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin'
        || $scope.userRole == 'Advanced User';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin'
        || $scope.userRole == 'Advanced User' || $scope.userRole == 'Normal user'
        || $scope.userRole == 'CONTRAGENT';
    $scope.deleteClaim = $scope.addClaim;

    $scope.isActive = false;
    $scope.Saving = false;

    $scope.birthdate = null;

    $scope.setDefaultEmployee = function() {
        return {
            id: 0,
            contragentId: -1,
            firstName: null,
            middleName: null,
            lastName: null,
            birthDate: null,
            description: null,
            status: null,
            cltId: null,
            offId: null
        }
    }

    $scope.employee = $scope.setDefaultEmployee();

    $scope.moment = moment;

    $scope.editEmployee = function(employeeId) {
        $state.go('employee', {
            id: employeeId
        });
    }

    $scope.saveEmployeeClick = function() {
        if ($scope.employeeForm.$invalid) {
            return;
        }
        $scope.Saving = true;
        employee = {
            id: $scope.employee.id,
            contragentId: $scope.ctgId,
            firstName: $scope.employee.firstName,
            middleName: $scope.employee.middleName,
            lastName: $scope.employee.lastName,
            birthDate: $scope.birthdate,
            description: $scope.employee.description,
            status: $scope.employee.status,
            cltId: $scope.employee.cltId,
            offId: $scope.employee.offId
        }

        employeesService.saveEmployee(employee).success(function(response) {
            $scope.employees.push(response);
            $scope.Saving = false;
            $mdDialog.hide();
        }).error(function() {
            $state.go('error');
            $scope.Saving = false;
            $mdDialog.hide();
        });
    }

    $scope.close = function() {
        $mdDialog.hide();
    }

    $scope.showAddEmployeeDialog = function(ev) {
        employee = $scope.setDefaultEmployee();
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/employees/employee_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
            .then(function(answer) {

            }, function() {

            });
    }

    $scope.showDeleteEmployeeConfirmDialog = function(employeeId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('EMP_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('EMP_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function() {
            employeesService.deleteEmployee(employeeId).success(function(response) {
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

            }).error(function(response) {
                $state.go('error');
            });
        }, function() {

        });
    };

    if (employeesService.employees != undefined) {
        $scope.employees = employeesService.employees;
        return;
    }

    if ($scope.ctgId == '-1') {
        employeesService.load().success(function(response) {
            employeesService.employees = response;
            $scope.employees = employeesService.employees;
            $scope.isActive = true;
        }).error(function() {
            $scope.isActive = true;
            $state.go('error');
        });
    }
    else {
        employeesService.loadByCtg($scope.ctgId).success(function(response) {
            employeesService.employees = response;
            $scope.employees = employeesService.employees;
            $scope.isActive = true;
        }).error(function() {
            $scope.isActive = true;
            $state.go('error');
        });
    }


});