angular.module('EuroJobsCrm.controllers').controller('CalendarController', function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies, calendarService) {
    
    $scope.startDate = new Date();


    $scope.dayConfig = {
        startDate: $scope.startDate,
        viewType: "Week"
    };

    $scope.monthConfig = {
        startDate: moment($scope.startDate).format('YYYY-MM-DD'),
    };

    $scope.changeViewType = function(type){
        $scope.viewType = type;

        if (type == 'day'){
            $scope.dayViewEnabled = true;
            $scope.weekViewEnabled = $scope.monthViewEnabled = false;
            $scope.dayConfig.viewType = 'Day';
        }
        if (type == 'week'){
            $scope.weekViewEnabled = true;
            $scope.dayViewEnabled = $scope.monthViewEnabled = false;
            $scope.dayConfig.viewType = 'Week';
        }
        if (type == 'month'){
            $scope.monthViewEnabled = true;
            $scope.weekViewEnabled = $scope.dayViewEnabled = false;     
        }
    }

    $scope.changeStartDate = function(){
         var result = new Date($scope.$$childHead.startDate);
         result.setDate(result.getDate() + 1);

         $scope.dayConfig.startDate = result;
         $scope.monthConfig.startDate = moment($scope.$$childHead.startDate).format('YYYY-MM-DD');

         $scope.$$childHead.dpDay.update();
         $scope.$$childHead.dpMonth.update();
         
    }

    $scope.changeViewType('day');
    
});