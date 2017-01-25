angular.module('EuroJobsCrm.controllers').controller('UsersController', function ($scope, $location, $http, $state, $translate, $mdDialog, $cookies, usersService) {
    $scope.contragents = [];
    $scope.user = usersService.getUser();
    $scope.addingUserMethod = undefined;
    $scope.accountingUsers = new Array();
    $scope.admins = new Array();
    $scope.advancedUsers = new Array();
    $scope.normalUsers = new Array();
    $scope.Saving = false;
    $scope.isActive = false;

    console.log($cookies.get('user_role'));

    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.showAddNormalUserDialog = function () {
        $scope.addingUserMethod = usersService.AddNormalUser;
        $scope.showAddingUserDialog();
    }

    $scope.showAddAdvancedUserDialog = function () {
        $scope.addingUserMethod = usersService.AddAdvancedUser;
        $scope.showAddingUserDialog();
    }

    $scope.showAddAccountingUserDialog = function () {
        $scope.addingUserMethod = usersService.AddAccountingUser;
        $scope.showAddingUserDialog();
    }

    $scope.showAdminUserDialog = function () {
        $scope.addingUserMethod = usersService.AddAdminUser;
        $scope.showAddingUserDialog();
    }

    $scope.showAddingUserDialog = function (){
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/users/user_dialog_tmpl.html',
            clickOutsideToClose: true,
        })
        .then(function (answer) {

        }, function () {

        });
    }

    $scope.saveUserClick = function () {
        if ($scope.userForm.$invalid) {
            return;
        }

        $scope.Saving = true;
        $scope.addingUserMethod($scope.user).success(function (response) {
            if (response.success != true) {
                $mdDialog.show(
                    $mdDialog.alert()
                        //.parent(angular.element(document.querySelector('#popupContainer')))
                        .clickOutsideToClose(true)
                        .title($translate.instant('ADDING_USER_ERROR'))
                        .textContent(response.errorMessage)
                        .ariaLabel('Error dialog')
                        .ok('OK')

                );
                $mdDialog.hide();
                return;
            }

            if ($scope.addingUserMethod == usersService.AddNormalUser) {
                //usersService.normalUsers.push(response);
                 $scope.reloadUsers();
            }

            if ($scope.addingUserMethod == usersService.AddAdvancedUser) {
                //usersService.advancedUsers.push(response);
                $scope.reloadUsers();
            }

            if ($scope.addingUserMethod == usersService.AddAdminUser) {
               // usersService.admins.push(response);
               $scope.reloadUsers();
            }

            if ($scope.addingUserMethod == usersService.AddAccountingUser) {
               // usersService.accountingUsers.push(response);
               $scope.reloadUsers();
            }

            $scope.user = usersService.getUser();
            $mdDialog.hide();
            $scope.Saving = false;

        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.showResetPassConfirmDialog = function (userId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('RESET_PASS_CONFIRM_TITLE'))
            .textContent($translate.instant('RESET_PASS_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('YES'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            usersService.resetPasswordForUser(userId).success(function (response) {
                message = response.success ? $translate.instant('RESET_PASS_SUCCESS') : response.errorMessage;
                $mdDialog.show($mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title($translate.instant('RESET_PASS_CONFIRM_TITLE'))
                        .textContent(message)
                        .ariaLabel('Message')
                        .ok('OK'));

            }).error(function (response) {
                $state.go('error');
            });
        }, function () {

        });
    };

    if (usersService.users != undefined) {
        $scope.accountingUsers = usersService.accountingUsers;
        $scope.admins = usersService.admins;
        $scope.advancedUsers = usersService.advancedUsers;
        $scope.normalUsers = usersService.normalUsers;

        return;
    }

    usersService.load().success(function (response) {

        usersService.setAdmins(response['Admin']);
        usersService.setAccountingUsers(response['Accounting']);
        usersService.setAdvancedUsers(response['Advanced user']);
        usersService.setNormalUsers(response['Normal user']);
        

        $scope.accountingUsers = usersService.accountingUsers;
        $scope.admins = usersService.admins;
        $scope.advancedUsers = usersService.advancedUsers;
        $scope.normalUsers = usersService.normalUsers;

        $scope.isActive = true;
      
    }).error(function () {
        $state.go('error');
    });

    $scope.reloadUsers = function()
    {
        usersService.load().success(function (response) {

        usersService.setAdmins(response['Admin']);
        usersService.setAccountingUsers(response['Accounting']);
        usersService.setAdvancedUsers(response['Advanced user']);
        usersService.setNormalUsers(response['Normal user']);
        

        $scope.accountingUsers = usersService.accountingUsers;
        $scope.admins = usersService.admins;
        $scope.advancedUsers = usersService.advancedUsers;
        $scope.normalUsers = usersService.normalUsers;
      
    }).error(function () {
        $state.go('error');
    });
    }
});