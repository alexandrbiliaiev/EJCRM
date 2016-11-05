angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, $translate, $mdDialog, contragentsService) {
    $scope.contragents = [];
    $scope.index = 0;


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
        console.log($scope);
        $mdDialog.hide();
    }

    $scope.showAddContragentDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/contragent_add_tmpl.html',
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
    }

    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
        $scope.contragents = contragentsService.contragents;
    }).error(function () {
        $state.go('error');
    });
});