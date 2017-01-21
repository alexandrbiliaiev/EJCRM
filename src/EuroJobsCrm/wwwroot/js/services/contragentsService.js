angular.module('EuroJobsCrm.services')
    .factory('contragentsService', ['$http', function ($http) {

        var contragents = {};

        contragents.load = function () {
            return $http.get('/api/Contragents');
        };

        contragents.saveContragent = function (contragent) {
            return $http({
                url: 'api/Contragents/Save',
                method: "POST",
                data: contragent
            });
        }

        contragents.addResponsiblePersonToContragent = function (param) {
            return $http({
                url: 'api/Contragents/addResponsiblePersonToContragent',
                method: "POST",
                data: param
            });
        }

        contragents.deleteContragent = function (ctgId) {
            param = {
                contragentId: ctgId
            };
            return $http({
                url: 'api/Contragents/Delete',
                method: "POST",
                data: ctgId
            });
        }

        contragents.getContragent = function (id) {
            for (i in this.contragents) {
                if (this.contragents[i].id == id) {
                    return this.contragents[i];
                }
            }

            return new {
                id: 0,
                name: '',
                licenseNumber: '',
                status: 'a'
            }
        }

        contragents.AddContragentUser = function (user) {
            return $http({
                url: 'api/Users/AddContragentUser',
                method: "POST",
                data: user
            });
        }



        return contragents;

    }]);