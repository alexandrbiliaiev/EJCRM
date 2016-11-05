angular.module('EuroJobsCrm.controllers').controller('ContragentManageController', function ($scope, $location, $translate, $http, $state, contragentsService, $mdDialog, $routeParams) {
    $scope.expandDetails = true;
    $scope.expandContactPersons = true;
    $scope.expandAddresses = true;
    $scope.expandEmployees = true;



    $scope.goBack = function () {
        $state.go('contragents');
    }

    $scope.saveContragentClick = function () {
        if ($scope.contragentForm.$invalid) {
            return;
        }

        contragent = {
            id: $scope.contragent.id,
            name: $scope.contragent.name,
            licenseNumber: $scope.contragent.licenseNumber,
            status: $scope.contragent.status
        };

        contragentsService.saveContragent(contragent).success(function (response) {
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

    $scope.showEditContragentDialog = function (ev) {
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

    
   
     if (contragentsService.contragents != undefined) {
         $scope.contragent = contragentsService.getContragent($state.params.id);
        return;
    }

    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
         $scope.contragent = contragentsService.getContragent($state.params.id);
    }).error(function () {
        $state.go('error');
    });

});