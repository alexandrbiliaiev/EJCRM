angular.module('EuroJobsCrm.controllers').controller('UsersController', function ($scope, $location, $http, $state, $translate, $mdDialog, usersService) {
    $scope.contragents = [];
    $scope.user = usersService.getUser();

    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.showAddNormalUserDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/users/user_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
        .then(function (answer) {

        }, function () {

        });
    }

    $scope.saveNormalUserClick = function () {
        if ($scope.userForm.$invalid) {
            return;
        }

        usersService.saveUser($scope.user).success(function (response) {
            usersService.users.push(response);
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
        $scope.users = usersService.users;
        return;
    }

    usersService.load().success(function (response) {
        usersService.users = response;
        $scope.users = usersService.users;
    }).error(function () {
        $state.go('error');
    });
});