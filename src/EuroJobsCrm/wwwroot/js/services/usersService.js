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

        users.GetResponsibleUsersList = function()
        {
            return $http({
                url: 'api/Users/GetResponsibleUsersList',
                method: "GET"
            });
        }

        users.ChangeUserLanguage = function (utc) {
             return $http({
                url: 'api/Users/ChangeUserLanguage',
                method: "POST",
                data: utc
            });
        }

        users.AddAdvancedUser = function (user) {
            return $http({
                url: 'api/Users/AddAdvancedUser',
                method: "POST",
                data: user
            });
        }

        users.AddContragentUser = function (user, id) {
            return $http({
                url: 'api/Users/AddContragentUser',
                method: "POST",
                data: user, id
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

        users.EditUser = function (user) {
            return $http({
                url: 'api/Users/EditName',
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

        users.resetPasswordForUser = function (userId) {
            return $http({
                url: 'api/Users/ResetPassword',
                method: "POST",
                data: { id: userId }
            });
        }

        users.deleteUser = function (userId) {
            return $http({
                url: 'api/Users/Delete',
                method: "POST",
                data: { id: userId }
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

        users.setAdmins = function (admins) {
            users.admins = admins != undefined ? admins : new Array();
        }

        users.setAccountingUsers = function (accountingUsers) {
            users.accountingUsers = accountingUsers != undefined ? accountingUsers : new Array();
        }

        users.setAdvancedUsers = function (advUsers) {
            users.advancedUsers = advUsers != undefined ? advUsers : new Array();
        }

        users.setNormalUsers = function (normalUsers) {
            users.normalUsers = normalUsers != undefined ? normalUsers : new Array();
        }

        return users;

    }]);