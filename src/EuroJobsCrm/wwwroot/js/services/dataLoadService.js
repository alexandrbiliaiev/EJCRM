angular.module('EuroJobsCrm.services', [])
    .factory('dataLoadService', ['$http', function ($http) {

        var shopData = {};

        shopData.load = function () {
            return $http.get('/api/shop');
        };


        return shopData;

    }]);