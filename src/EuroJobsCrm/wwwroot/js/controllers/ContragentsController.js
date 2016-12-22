angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, $translate, $mdDialog, contragentsService, usersService) {
    $scope.contragents = [];
    $scope.index = 0;
    $scope.contragents = new Array();
    $scope.users = new Array();

    $scope.contragent = {
        status: 'a',
        name: '',
        icenseNumber: ''
    }

    $scope.editContragent = function (ctgId) {
        $state.go('contragent', {
            id: ctgId
        });
    }


    $scope.saveContragentClick = function () {
        if ($scope.contragentForm.$invalid) {
            return;
        }

        contragent = {
            name: $scope.contragent.name,
            licenseNumber: $scope.contragent.licenseNumber,
            status: $scope.contragent.status
        };

        contragentsService.saveContragent(contragent).success(function (response) {
            contragentsService.contragents.push(response);
            $scope.contragent.status = 'a';
            $scope.contragent.name = $scope.contragent.licenseNumber = '';
            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }


    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.showAddContragentDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/contragents/contragent_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

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

    if (contragentsService.contragents != undefined) {
        $scope.contragents = contragentsService.contragents;
        return;
    };

    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
        $scope.contragents = contragentsService.contragents;
    }).error(function () {
        $state.go('error');
    });

    usersService.load().success(function (response) {

        usersService.setAdmins(response['Admin']);
        usersService.setAdvancedUsers(response['Advanced user']);
        usersService.setNormalUsers(response['Normal user']);

        $scope.admins = usersService.admins;
        $scope.advancedUsers = usersService.advancedUsers;
        $scope.normalUsers = usersService.normalUsers;

        for (i in $scope.admins) {
            $scope.users.push($scope.admins[i]);
        }

        for (i in $scope.advancedUsers) {
            $scope.users.push($scope.advancedUsers[i]);
        }

        for (i in $scope.normalUsers) {
            $scope.users.push($scope.normalUsers[i]);
        }

    }).error(function () {
        $state.go('error');
    });

    $scope.userRole = $cookies.get('user_role');
    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin'  || $scope.userRole == 'Advanced user';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super admin'  || $scope.userRole == 'Advanced user' || $scope.userRole == 'Normal user';
    
});