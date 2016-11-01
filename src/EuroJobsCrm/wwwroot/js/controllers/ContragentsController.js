angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, contragentsService, $mdDialog) {
    $scope.contragents = [];
    $scope.index = 0;
    $scope.status = '';
    $scope.name = '';
    $scope.licensenumber = '';


    contragentsService.load().success(function (response) {
        $scope.contragents = response;

    }).error(function () {
        $state.go('error');
    });

    $scope.addNewContragent = function () {

            contragent = {
                name: $scope.name,
                licenseNumber: $scope.licensenumber,
                status: $scope.status
            };

            return $http({
                url: 'api/Contragents/Add',
                method: "POST",
                data: contragent
            });
           
        }

        $scope.addNewContragentClick = function () {

            $scope.addNewContragent().success(function (response) {
                $scope.contragents.push(response);
                 $mdDialog.hide();
            }).error(function () {
                $state.go('error');
                 $mdDialog.hide();
            });
        }


    $scope.showAdvanced = function (ev) {
        $mdDialog.show({
            controller: this,
            templateUrl: '/templates/contragent_add_tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
            .then(function (answer) {

            }, function () {

            });
    };


    function DialogController($scope, $mdDialog) {
        
    }
});

