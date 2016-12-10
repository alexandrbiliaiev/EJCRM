angular.module('EuroJobsCrm.controllers').controller('UsersController', function ($scope, $location, $http, $state, $translate, $mdDialog, usersService) {
    $scope.contragents = [];
    $scope.user = usersService.getUser();
    $scope.addingUserMethod = undefined;

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
                usersService.normalUsers.push(response);
            }

            if ($scope.addingUserMethod == usersService.AddAdvancedUser) {
                usersService.advancedUsers.push(response);
            }

            if ($scope.addingUserMethod == usersService.AddAdminUser) {
                usersService.admins.push(response);
            }

            if ($scope.addingUserMethod == usersService.AddAccountingUser) {
                usersService.accountingUsers.push(response);
            }

            $scope.user = usersService.getUser();
            $mdDialog.hide();

        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.showDeleteCtgConfirmDialog = function (contragentId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('CTG_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('CTG_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            contragentsService.deleteContragent(contragentId).success(function (response) {
                if (response != true) {
                    return;
                }

                contragents = contragentsService.contragents;
                for (i in contragents) {
                    if (contragents[i].id != contragentId) {
                        continue;
                    }

                    contragents.splice(i, 1);
                    return;
                }

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
        $scope.accountingUsers = usersService.accountingUsers = response['Accounting'];
        $scope.admins = usersService.admins = response['Admin'];
        $scope.advancedUsers = usersService.advancedUsers = response['Advanced user'];
        $scope.normalUsers = usersService.normalUsers = response['Normal user'];
    }).error(function () {
        $state.go('error');
    });
});