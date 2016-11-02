angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, contragentsService, $mdDialog) {
    $scope.contragents = [];
    $scope.index = 0;
    $scope.status = 'a';
    $scope.name = '';
    $scope.licenseNumber = '';

    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
        $scope.contragents = contragentsService.contragents;

    }).error(function () {
        $state.go('error');
    });

    $scope.editContragent = function (id) {
        $state.go('contragent', {
            id: id
        });
    }

    $scope.addNewContragent = function () {
        contragent = {
            name: $scope.name,
            licenseNumber: $scope.licenseNumber,
            status: $scope.status
        };
        return $http({
            url: 'api/Contragents/Save',
            method: "POST",
            data: contragent
        });
    }

    $scope.addNewContragentClick = function () {
        if ($scope.contragentForm.$invalid){
            return;
        }

        $scope.addNewContragent().success(function (response) {
            contragentsService.contragents.push(response);
            $scope.status = 'a';
            $scope.name = $scope.licenseNumber = '';
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

    $scope.showDialog = function (ev) {
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
});