angular.module('EuroJobsCrm.controllers').controller('CalendarController', function ($scope, $location, $translate, $http, $state, $mdDialog,
    $routeParams, $cookies, calendarService, usersService,
    contragentsService, clientsService, employeesService) {

    function convertToDayPilotEvent(response) {
        response.start = new DayPilot.Date(response.startDate);
        response.end = new DayPilot.Date(response.endDate);
        response.text = response.subject;
        response.startDate = new Date(response.startDate).addHours(-1);
        response.endDate = new Date(response.endDate).addHours(-1);
        response.remindDate = new Date(response.remindDate).addHours(-1);
    }



    $scope.startDate = new Date();

    $scope.onEventClick = function (args) {
        $scope.Loading = true;
        calendarService.getEvent(args.e.id()).success(function (response) {
            convertToDayPilotEvent(response);

            if (response.clientId != null &&
                ($scope.clients == undefined || !$scope.clients.ContainsId(response.clientId))) {

                $scope.clients = [{ id: response.clientId, name: response.clientName }]
            }

            if (response.targetUser != null &&
                ($scope.users == undefined || !$scope.users.ContainsId(response.targetUser))) {

                $scope.users = [{ id: response.targetUser, userName: response.targetUserName }];
            }

            if (response.contragentId != null &&
                ($scope.contragents == undefined || !$scope.contragents.ContainsId(response.contragentId))) {

                $scope.contragents = [{ id: response.contragentId, name: response.contragentName }]
            }

            if (response.employeeId != null &&
                ($scope.employees == undefined || !$scope.employees.ContainsId(response.employeeId))) {

                $scope.employees = [{ id: response.employeeId, firstName: response.employeeName }]
            }

            $scope.currentEvent = response;
            $scope.Loading = false;
        }).error(function () {
            $scope.Loading = false;
            $state.go('error');
        })

        $scope.showEventDialog();
    }

    $scope.onTimeRangeSelected = function (args) {
        $scope.currentEvent = {
            start: args.start.value,
            end: args.end,
            id: DayPilot.guid(),
            text: "",
            startDate: new Date(args.start.value).addHours(-1),
            endDate: new Date(args.end).addHours(-1),
            remindDate: new Date(args.start.value).addDays(-1).addHours(-1)
        };
        $scope.showEventDialog();
    }

    $scope.onEventMove = function (args) {
        var event = $scope.events.getById(args.e.id());
        if (event == undefined) {
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
            onTimeRangeSelected: $scope.onTimeRangeSelected,
        };

    $scope.monthConfig = {
        startDate: moment($scope.startDate).format('YYYY-MM-DD'),
        onEventClick: $scope.onEventClick,
        onEventMove: $scope.onEventMove,
        onTimeRangeSelected: $scope.onTimeRangeSelected,
    };

    $scope.events = new Array();

    $scope.changeViewType = function (type) {
        $scope.viewType = type;

        if (type == 'day') {
            $scope.dayViewEnabled = true;
            $scope.weekViewEnabled = $scope.monthViewEnabled = false;
            $scope.dayConfig.viewType = 'Day';
        }
        if (type == 'week') {
            $scope.weekViewEnabled = true;
            $scope.dayViewEnabled = $scope.monthViewEnabled = false;
            $scope.dayConfig.viewType = 'Week';
        }
        if (type == 'month') {
            $scope.monthViewEnabled = true;
            $scope.weekViewEnabled = $scope.dayViewEnabled = false;
        }
    }

    $scope.changeStartDate = function () {
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

    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.saveEventClick = function () {
        if ($scope.eventForm.$invalid) {
            return;
        }

        $scope.currentEvent.startDate = $scope.currentEvent.startDate.normalize();
        $scope.currentEvent.endDate = $scope.currentEvent.endDate.normalize();
        if ($scope.currentEvent.remindDate != undefined) {
            $scope.currentEvent.remindDate = $scope.currentEvent.remindDate.normalize();
        }

        $scope.Saving = true;
        calendarService.saveEvent($scope.currentEvent).success(function (response) {
            convertToDayPilotEvent(response);

            if (!$scope.events.ContainsId(response.id)) {
                $scope.events.push(response);
            } else {
                for (var i = 0; i < $scope.events.length; i++) {
                    if ($scope.events[i].id == response.id) {
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

            for (var i = 0; i < $scope.events.length; i++) {
                if ($scope.events[i].id == $scope.currentEvent.id) {
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
        $scope.currentEvent.startDate = $scope.currentEvent.startDate;
        var pilotDate = new Date($scope.currentEvent.start);
        $scope.currentEvent.startDate.addTime(pilotDate.getHours(), pilotDate.getMinutes());
    }

    $scope.eventEndDateChange = function () {
        $scope.currentEvent.endDate = $scope.currentEvent.endDate;
        var pilotDate = new Date($scope.currentEvent.end);
        $scope.currentEvent.endDate.addTime(pilotDate.getHours(), pilotDate.getMinutes());
    }

    $scope.eventRemindDateChange = function () {
        $scope.currentEvent.remindDate = $scope.currentEvent.remindDate.normalize();
    }

    $scope.loadUsers = function () {
        if ($scope.users != undefined && $scope.users.length > 2) {
            return;
        }

        $scope.isLoading = true;
        usersService.load().success(function (response) {
            users = [];
            angular.extend(users, response['Admin']);
            angular.extend(users, response['Accounting']);
            angular.extend(users, response['Advanced user']);
            angular.extend(users, response['Normal user']);

            $scope.users = users;
            $scope.isLoading = false;
        }).error(function () {
            $scope.isLoading = false;
            $state.go('error');
        })

    }

    $scope.loadContragents = function () {
        if ($scope.contragents != undefined && $scope.contragents.length > 2) {
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
        if ($scope.clients != undefined && $scope.clients.length > 2) {
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
        if ($scope.employees != undefined && $scope.employees.length > 2) {
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