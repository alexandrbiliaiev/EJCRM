angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, contragentsService) {
    $scope.contragents = [];
    $scope.index = 0;

    $scope.addNewContragentClick = function () {

        $scope.addNewContragent().success(function (response) {
             $scope.contragents.push(response);

        }).error(function () {
            $state.go('error');
        });
    }

    $scope.addNewContragent = function () {

        contragent = {
            name: 'Vectra ' + $scope.index++,
            licenseNumber: '02',
            status: 'a'
        };

        return $http({
            url: 'api/Contragents/Add',
            method: "POST",
            data: contragent
        });

    }


    contragentsService.load().success(function (response) {
        $scope.contragents = response;

    }).error(function () {
        $state.go('error');
    });

}); 