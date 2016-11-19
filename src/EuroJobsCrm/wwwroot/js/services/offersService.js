angular.module('EuroJobsCrm.services')
    .factory('offersService', ['$http', function ($http) {

        var offers = {};

        offers.saveAddress= function (offer) {
            return $http({
                url: 'api/Offers/Save',
                method: "POST",
                data: offer
            });
        }

        offers.deleteAddress = function (offId) {

            return $http({
                url: 'api/Offers/Delete',
                method: "POST",
                data: offId
            });
        }

        return offers;

    }]);