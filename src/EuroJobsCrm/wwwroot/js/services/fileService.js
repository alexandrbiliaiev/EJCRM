angular.module('EuroJobsCrm.services')
    .factory('fileService', ['$http', function ($http) {

        var addresses = {};

        addresses.saveFile = function (data) {
            return $http({
                method: 'POST',
                url: 'api/Files/Upload',
                data: data, // pass in data as strings
                headers: {
                    'Content-Type': undefined
                }
            });
        }

        addresses.deleteFile = function (fileId) {

            return $http({
                url: 'api/Files/Delete',
                method: "POST",
                data: fileId
            });
        }

        return addresses;

    }]);