angular.module('EuroJobsCrm.controllers').controller('ContragentsController', function ($scope, $location, $http, $state, contragentsService, $mdDialog) {
    $scope.contragents = [];
    $scope.index = 0;
    $scope.status = '';
    $scope.name = '';
    $scope.licensenumber = '';


    contragentsService.load().success(function (response) {
        contragentsService.contragents = response;
        $scope.contragents = contragentsService.contragents;

    }).error(function () {
        $state.go('error');
    });

    $scope.editContragent = function(id){
        $state.go('contragent', {id: id});
    }

    $scope.showDialog = function (ev) {
        $mdDialog.show({
            controller: function ($scope) {
                $scope.status = '';
                $scope.name = '';
                $scope.licensenumber = '';

                $scope.addNewContragent = function () {
                    contragent = {
                        name: $scope.name,
                        licenseNumber: $scope.licensenumber,
                        status: $scope.status
                    };
                    return $http({
                        url: 'api/Contragents/Save',
                        method: "POST",
                        data: contragent
                    });
                }

                $scope.addNewContragentClick = function () {
                    $scope.addNewContragent().success(function (response) {
                        contragentsService.contragents.push(response);
                        $mdDialog.hide();
                    }).error(function () {
                        $state.go('error');
                        $mdDialog.hide();
                    });
                }
            },
            templateUrl: '/templates/contragent_add_tmpl.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            locals: {
                contrangents: $scope.contragents,
                $state: $state
            }
        })
            .then(function (answer) {

            }, function () {

            });
    };
});

