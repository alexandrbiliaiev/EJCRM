angular.module('EuroJobsCrm.services')
    .factory('employeesService', ['$http', function ($http) {

        var employees = {};

        employees.load = function () {
            return $http.get('api/Employees');
        };

        employees.saveEmployee = function (employee) {
            return $http({
                url: 'api/Employees/Save',
                method: "POST",
                data: employee
            });
        }

        employees.deleteEmployee = function (id) {
            param = {
                employeeId: id
            };
            return $http({
                url: 'api/Employees/Delete',
                method: "POST",
                data: id
            });
        }

        employees.getEmployee = function (id) {
            for (i in this.employees) {
                if (this.employees[i].id == id) {
                    return this.employees[i];
                }
            }

            return new {
                id : 0,
                name:'',
                licenseNumber: '',
                status: 'a'
            }
        }

        return employees;

    }]);