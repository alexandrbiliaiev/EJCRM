angular.module('EuroJobsCrm.controllers').controller('OfferManageController', function ($scope, $location, $translate, $http, $state, 
offersService, $mdDialog, $routeParams, employeesService) {
    $scope.expandDetails = true;
    $scope.expandCandidates = true;
    $scope.expandEmployees = true;
    $scope.expandEmployees = true;

    $scope.goBack = function () {
        $state.go('offers');
    }

    $scope.close = function () {
        console.log($scope);
        $mdDialog.hide();
    }

    $scope.getFormatedDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    };


    offersService.getOffer($state.params.id).success(function (response) {
        $scope.offer = response;
    }).error(function () {
        $state.go('error');
    });

});