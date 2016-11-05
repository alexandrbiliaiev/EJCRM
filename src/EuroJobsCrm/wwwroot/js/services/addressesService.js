angular.module('EuroJobsCrm.services')
    .factory('addressesService', ['$http', function ($http) {

        var addresses = {};

        addresses.saveAddress= function (address) {
            return $http({
                url: 'api/Addresses/Save',
                method: "POST",
                data: address
            });
        }

        addresses.deleteAddress = function (adrId) {

            return $http({
                url: 'api/Addresses/Delete',
                method: "POST",
                data: adrId
            });
        }

        return addresses;

    }]);