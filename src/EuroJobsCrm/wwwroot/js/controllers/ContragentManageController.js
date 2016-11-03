angular.module('EuroJobsCrm.controllers').controller('ContragentManageController', function ($scope, $location, $translate, $http, $state, contragentsService, $mdDialog, $routeParams) {
 
    for (i in contragentsService.contragents) {
        if (contragentsService.contragents[i].id == $state.params.id) {
            $scope.contragent = contragentsService.contragents[i];
        }
    }

    
});