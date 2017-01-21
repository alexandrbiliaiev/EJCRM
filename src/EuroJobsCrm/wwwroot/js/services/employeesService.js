angular.module('EuroJobsCrm.services')
    .factory('employeesService', ['$http', function ($http) {

        var employees = {};
        var currentEmployee;

        employees.load = function () {
            return $http.get('api/Employees/GetAll');
        };

        employees.loadLite = function () {
            return $http.get('api/Employees/GetAll/Lite');
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

        employees.saveDocument = function (document){
            return $http({
                url: 'api/Employees/SaveDocument',
                method: "POST",
                data: document
            });
        }

         employees.deleteDocument = function(docId){
             return $http({
                url: 'api/Employees/DeleteDocument',
                method: "POST",
                data: docId
            });
        }

        employees.deleteDocumentFile = function(fileId){
             return $http({
                url: 'api/Employees/DeleteFile',
                method: "POST",
                data: fileId
            });
        }

        employees.getEmployeeFromDb = function (id) {
            param = {
                employeeId: id
            };
            return $http({
                url: 'api/Employees/Get',
                method: "POST",
                data: id
            });
        }


        employees.getEmployeeFromCache = function (id) {
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

        employees.setCurrentEmployee = function(employee){
                currentEmployee = employee;
        }

        employees.getCurrentEmployee = function(){
            return currentEmployee;
        }

        employees.getDefaultDocument = function () {
            return {
                id: 0,
                parentDocumentId: null,
                employeeId: 0,
                seria: '',
                number: '',
                issueDate: new Date(),
                validFrom: new Date(),
                validTo: new Date(),
                type: 0,
                visaType: '',
                remarks: '',
                files: new Array()
            };
        }

        return employees;

    }]);