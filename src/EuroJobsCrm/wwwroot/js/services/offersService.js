angular.module('EuroJobsCrm.services')
    .factory('offersService', ['$http', function ($http) {

        var offers = {};

        offers.load = function () {
            return $http.get('api/Offers');
        };

        offers.saveOffer= function (offer) {
            return $http({
                url: 'api/Offers/Save',
                method: "POST",
                data: offer
            });
        }

        offers.deleteOffer = function (offId) {

            return $http({
                url: 'api/Offers/Delete',
                method: "POST",
                data: offId
            });
        }

        return offers;

    }]);