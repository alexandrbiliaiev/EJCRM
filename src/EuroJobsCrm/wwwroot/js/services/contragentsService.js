angular.module('EuroJobsCrm.services')
    .factory('contragentsService', ['$http', function ($http) {

        var contragents = {};

        contragents.load = function () {
            return $http.get('/api/Contragents');
        };

        contragents.loadLite = function () {
            return $http.get('/api/Contragents/Lite');
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
            param = {
                id: id
            };
            return $http({
                url: '/api/Contragents/GetContrgent',
                method: "POST",
                data: id
            });
        }

        contragents.AddContragentUser = function (user) {
            return $http({
                url: 'api/Users/AddContragentUser',
                method: "POST",
                data: user
            });
        }

        contragents.notifyAboutOffer = function (params) {
            return $http({
                url: 'api/Contragents/Notify',
                method: "POST",
                data: params
            });
        }



        return contragents;

    }]);