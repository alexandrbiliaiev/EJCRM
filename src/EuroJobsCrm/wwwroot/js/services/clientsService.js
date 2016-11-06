angular.module('EuroJobsCrm.services')
    .factory('clientsService', ['$http', function ($http) {

        var clients = {};

        clients.load = function () {
            return $http.get('/api/Clients');
        };

        clients.saveClient = function (client) {
            return $http({
                url: 'api/Clients/Save',
                method: "POST",
                data: client
            });
        }

        clients.deleteClient = function (clientId) {
            return $http({
                url: 'api/Clients/Delete',
                method: "POST",
                data: clientId
            });
        }

        clients.getClient = function (id) {
            for (i in this.clients) {
                if (this.clients[i].id == id) {
                    return this.clients[i];
                }
            }

            //todo
            return new {
                id : 0,
                name:'',
                licenseNumber: '',
                status: 'a'
            }
        }

        return clients;

    }]);