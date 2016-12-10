angular.module('EuroJobsCrm.services')
    .factory('usersService', ['$http', function ($http) {

        var users = {};

        users.load = function () {
            return $http.get('/api/Users/GetInternalUsers');
        };

        users.AddNormalUser = function (user) {
            return $http({
                url: 'api/Users/AddNormalUser',
                method: "POST",
                data: user
            });
        }

        users.AddAdvancedUser = function (user) {
            return $http({
                url: 'api/Users/AddAdvancedUser',
                method: "POST",
                data: user
            });
        }

        users.AddAccountingUser = function (user) {
            return $http({
                url: 'api/Users/AddAccountingUser',
                method: "POST",
                data: user
            });
        }

        users.AddAdminUser = function (user) {
            return $http({
                url: 'api/Users/AddAdmin',
                method: "POST",
                data: user
            });
        }

        users.deleteUser = function (userId) {
            return $http({
                url: 'api/Clients/Delete',
                method: "POST",
                data: userId
            });
        }

        users.getUser = function (id) {
            for (i in this.users) {
                if (this.users[i].id == id) {
                    return this.users[i];
                }
            }

            return {
                id: 0,
                email: "",
                password: "",
            }
        }

        return users;

    }]);