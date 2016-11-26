angular.module('EuroJobsCrm.services')
    .factory('offersService', ['$http', function ($http) {

        var offers = {};

        offers.getOffer = function (offId) {

            return $http({
                url: 'api/Offers/Get',
                method: "POST",
                data: offId
            });
        };

        offers.saveCandidateRequest = function (candidateRequest) {
            return $http({
                url: '/api/Offers/MakeRequest',
                method: "POST",
                data: candidateRequest
            });
        };

        offers.pimpCandidateToEmployee = function (candidateRequest) {
            return $http({
                url: '/api/Offers/ChangeRequestStatus',
                method: "POST",
                data: candidateRequest
            });
        };



        offers.load = function () {
            return $http.get('api/Offers');
        };

        offers.saveOffer = function (offer) {
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