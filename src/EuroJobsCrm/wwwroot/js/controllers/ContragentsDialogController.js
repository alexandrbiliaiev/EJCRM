angular.module('EuroJobsCrm.controllers').controller('ContragentsDialogController', function ($scope, $location, $http, $state, contragentsService, $mdDialog) {
    $scope.contragents = [];
    $scope.index = 0;
    $scope.status = '';
    $scope.name = '';
    $scope.licensenumber = '';
});

