angular.module('EuroJobsCrm.services', [])
    .factory('contragentsService', ['$http', function ($http) {

        var contragents = {};

        contragents.load = function () {
            return $http.get('/api/Contragents');
        };


        return contragents;

    }]);