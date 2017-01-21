angular.module('EuroJobsCrm.controllers').controller('CalendarController', function ($scope, $location, $translate, $http, $state, $mdDialog,
                                                                                     $routeParams, $cookies, calendarService, usersService,
                                                                                     contragentsService, clientsService, employeesService) {
    $scope.startDate = new Date();

    $scope.dayConfig = {
        startDate: $scope.startDate,
        viewType: "Week",
        onEventClick: function (args) {
            $scope.Loading = true;
            calendarService.getEvent(args.e.id()).success(function (response) {
                response.start = new DayPilot.Date(response.startDate);
                response.end = new DayPilot.Date(response.endDate);
                response.text = response.subject;

                if ($scope.clients == undefined || $scope.clients.length <= 1){
                    $scope.clients = [{id: response.clientId, name: response.clientName}]
                }

                if ($scope.users != undefined || $scope.users.length <= 1){
                    $scope.users = [{id: response.targetUser, name: response.targetUserName}];
                }

                $scope.currentEvent = response;
                $scope.Loading = false;
            }).error(function () {
                $scope.Loading = false;
                $state.go('error');
            })

            $scope.showEventDialog();
        },
        onEventMove: function(args) {
            var params = {
                id: args.e.id(),
                newStart: args.newStart.toString(),
                newEnd: args.newEnd.toString()
            };
            console.log(args);
        },
        onTimeRangeSelected: function(args) {
            $scope.currentEvent = {
                start: args.start,
                end: args.end,
                id: DayPilot.guid(),
                text: ""
            };
            $scope.showEventDialog();
        },
    };

    $scope.monthConfig = {
        startDate: moment($scope.startDate).format('YYYY-MM-DD'),
    };

    $scope.events = new Array();

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

    $scope.showEventDialog = function () {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/calendar/event_tmpl.html',
            clickOutsideToClose: true,
        })
        .then(function (answer) {

        }, function () {

        });
    }

    $scope.close = function (){
        $mdDialog.hide();
    }

    $scope.loadUsers = function () {
        if ($scope.users != undefined){
            return;
        }

        $scope.isLoading = true;
        usersService.load().success(function (response) {
            var users = response['Accounting'].concat(response['Admin'])
                .concat(response['Advanced user']).concat(response['Normal user']);

            $scope.users = users;
            $scope.isLoading = false;
        }).error(function () {
            $scope.isLoading = false;
            $state.go('error');
        })

    }

    $scope.contragents = [{id:16, name:"test"}];
    $scope.loadContragents = function () {
        if ($scope.contragents != undefined && $scope.contragents.length > 1){
            return;
        }

        $scope.isLoading = true;
        contragentsService.loadLite().success(function (response) {
            $scope.contragents = response;
            $scope.isLoading = false;
        }).error(function () {
            $scope.isLoading = false;
            $state.go('error');
        })
    }

    $scope.loadClients = function () {
        if ($scope.clients != undefined && $scope.clients.length > 1){
            return;
        }

        $scope.isLoading = true;
        clientsService.loadLite().success(function (response) {
            $scope.clients = response;
            $scope.isLoading = false;
        }).error(function () {
            $scope.isLoading = false;
            $state.go('error');
        })
    }

    $scope.loadEmployees = function () {
        if ($scope.employees != undefined && $scope.employees.length > 1){
            return;
        }

        $scope.isLoading = true;
        employeesService.loadLite().success(function (response) {
            $scope.employees = response;
            $scope.isLoading = false;
        }).error(function () {
            $scope.isLoading = false;
            $state.go('error');
        })
    }

    calendarService.load().success(function (response) {

        for (var i in response) {
            response[i].start = new DayPilot.Date(response[i].startDate);
            response[i].end = new DayPilot.Date(response[i].endDate);
            response[i].text = response[i].subject;

            $scope.events.push(response[i]);
        }

    }).error(function () {
        $state.go('error');
    })
    
    $scope.changeViewType('day');

});