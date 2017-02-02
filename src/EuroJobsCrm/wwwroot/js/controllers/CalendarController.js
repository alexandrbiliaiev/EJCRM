angular.module('EuroJobsCrm.controllers').controller('CalendarController', function ($scope, $location, $translate, $http, $state, $mdDialog,
                                                                                     $routeParams, $cookies, calendarService, usersService,
                                                                                     contragentsService, clientsService, employeesService) {

    Array.prototype.ContainsId = function (id) {
        for (var i in this){
            if (this[i].id == id){
                return true;
            }
        }
        return false;
    }

    Array.prototype.getById = function (id) {
        for (var i in this){
            if (this[i].id == id){
                return this[i];
            }
        }
        return undefined;
    }

    Date.prototype.addDays = function(days) {
        var date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }

    Date.prototype.addTime = function(h, m) {
        this.setTime(this.getTime() + (h*60*60*1000) + (m*60*1000));
        return this;
    }

    Date.prototype.normalize = function() {
        offset = this.getTimezoneOffset()*60*1000;
        this.setTime(this.getTime() - offset);
        return this;
    }

    function convertToDayPilotEvent(response) {
        response.start = new DayPilot.Date(response.startDate);
        response.end = new DayPilot.Date(response.endDate);
        response.text = response.subject;
        response.startDate = new Date(response.startDate);
        response.endDate = new Date(response.endDate);
        response.remindDate = new Date(response.remindDate);
    }

    $scope.startDate = new Date();

    $scope.onEventClick = function (args) {
        $scope.Loading = true;
        calendarService.getEvent(args.e.id()).success(function (response) {
            convertToDayPilotEvent(response);

            if (response.clientId != null &&
                ($scope.clients == undefined || !$scope.clients.ContainsId(response.clientId))){

                $scope.clients = [{id: response.clientId, name: response.clientName}]
            }

            if (response.targetUser != null &&
                ($scope.users == undefined || !$scope.users.ContainsId(response.targetUser))){

                $scope.users = [{id: response.targetUser, userName: response.targetUserName}];
            }

            if (response.contragentId != null &&
                ($scope.contragents == undefined || !$scope.contragents.ContainsId(response.contragentId))){

                $scope.contragents = [{id: response.contragentId, name: response.contragentName}]
            }

            if (response.employeeId != null &&
                ($scope.employees == undefined || !$scope.employees.ContainsId(response.employeeId))){

                $scope.employees = [{id: response.employeeId, firstName: response.employeeName}]
            }

            $scope.currentEvent = response;
            $scope.Loading = false;
        }).error(function () {
            $scope.Loading = false;
            $state.go('error');
        })

        $scope.showEventDialog();
    }

    $scope.onTimeRangeSelected = function(args) {
        $scope.currentEvent = {
            start: args.start.value,
            end: args.end,
            id: DayPilot.guid(),
            text: "",
            startDate: new Date(args.start.value),
            endDate: new Date(args.end),
        };
        $scope.showEventDialog();
    }

    $scope.onEventMove = function(args) {
        var event = $scope.events.getById(args.e.id());
        if (event == undefined){
            return;
        }

        $scope.currentEvent = event;
        $scope.currentEvent.start = args.newStart;
        $scope.currentEvent.end = args.newEnd;
        $scope.currentEvent.startDate = new Date(args.newStart.value);
        $scope.currentEvent.endDate = new Date(args.newEnd.value);

        $scope.showEventDialog();
    },

    $scope.dayConfig = {
        startDate: $scope.startDate,
        viewType: "Week",
        onEventClick: $scope.onEventClick,
        onEventMove: $scope.onEventMove,
        onTimeRangeSelected:$scope.onTimeRangeSelected,
    };

    $scope.monthConfig = {
        startDate: moment($scope.startDate).format('YYYY-MM-DD'),
        onEventClick: $scope.onEventClick,
        onEventMove: $scope.onEventMove,
        onTimeRangeSelected: $scope.onTimeRangeSelected,
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

    $scope.saveEventClick = function () {
        if ($scope.eventForm.$invalid){
            return;
        }

        $scope.Saving = true;
        calendarService.saveEvent($scope.currentEvent).success(function (response) {
            convertToDayPilotEvent(response);

            if (!$scope.events.ContainsId(response.id)){
                $scope.events.push(response);
            }else{
                for (var i = 0; i < $scope.events.length; i++){
                    if ($scope.events[i].id == response.id){
                        $scope.events.splice(i, 1);
                        $scope.events.push(response);
                        break;
                    }
                }
            }

            $scope.Saving = false;
            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $scope.Saving = false;
            $mdDialog.hide();
        })

    }

    $scope.deleteEventClick = function () {
        $scope.Saving = true;
        calendarService.deleteEvent($scope.currentEvent.id).success(function (response) {

            for (var i = 0; i < $scope.events.length; i++){
                if ($scope.events[i].id == $scope.currentEvent.id){
                    $scope.events.splice(i, 1);
                    break;
                }
            }

            $scope.Saving = false;
            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $scope.Saving = false;
            $mdDialog.hide();
        })
    }

    $scope.eventStartDateChange = function () {
        $scope.currentEvent.startDate = $scope.currentEvent.startDate.normalize();
        var pilotDate = new Date($scope.currentEvent.start);
        $scope.currentEvent.startDate.addTime(pilotDate.getHours() - 1, pilotDate.getMinutes());
    }

    $scope.eventEndDateChange = function () {
        $scope.currentEvent.endDate = $scope.currentEvent.endDate.normalize();
        var pilotDate = new Date($scope.currentEvent.end);
        $scope.currentEvent.endDate.addTime(pilotDate.getHours() -1, pilotDate.getMinutes());
    }

    $scope.eventRemindDateChange = function () {
        $scope.currentEvent.remindDate = $scope.currentEvent.remindDate.normalize();
    }

    $scope.loadUsers = function () {
        if ($scope.users != undefined && $scope.users.length > 2){
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

    $scope.loadContragents = function () {
        if ($scope.contragents != undefined && $scope.contragents.length > 2){
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
        if ($scope.clients != undefined && $scope.clients.length > 2){
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
        if ($scope.employees != undefined && $scope.employees.length > 2){
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

        for (i = 0; i < response.length; i++) {
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