angular.module('EuroJobsCrm.controllers').controller('LanguageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService,
        employeesService, fileService, usersService, calendarService, Notification) {

        if ($cookies.get('user_lng') == null) {
            $scope.currentLang = 'ru';
            $translate.use('ru');
        }
        else {
            $scope.currentLang = $cookies.get('user_lng');
        }

        $scope.$location = $location;

        calendarService.getLatestEvents().success(function (response) {
            $scope.count = response.length;
        }).error(function () {
            $scope.count = 0;
        });


        setInterval(function () {
            calendarService.getLatestEvents().success(function (response) {
                $scope.count = response.length;

                var start = new Date();
                var end = new Date().addTime(0, 5);

                for (var i = 0; i < response.length; i++) {
                    var remindDate = new Date(response[0].remindDate).addHours(-1);
                    if (start <= remindDate && remindDate <= end) {
                        Notification({
                            message: response[i].noteText,
                            title: response[i].subject
                        });
                    }
                }
            }).error(function () {
                $scope.count = 0;
            });

            $scope.$apply();
        }, 2 * 60 * 1000)


        $scope.changeLanguage = function (lang) {

            utc = {
                UsrId: $cookies.get('id'),
                prefLng: lang
            }

            usersService.ChangeUserLanguage(utc).success(function (response) {
                $translate.use(lang);
                $cookies.put('user_lng', lang);
                $scope.currentLang = lang;
            }).error(function () {
                $state.go('error');
            });
        }

        $translate.use($cookies.get('user_lng'));


        this.notificationsEnabled = true;
        this.toggleNotifications = function () {
            this.notificationsEnabled = !this.notificationsEnabled;
        };

        $scope.goToClients = function () {
            $state.go('clients');
        }


    })