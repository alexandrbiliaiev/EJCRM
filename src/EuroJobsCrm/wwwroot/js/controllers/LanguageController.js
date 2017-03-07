angular.module('EuroJobsCrm.controllers').controller('LanguageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService,
        employeesService, fileService, usersService, calendarService) {

        $scope.currentLang = $cookies.get('user_lng');
        $scope.$location = $location;


        setInterval(function () {
            calendarService.getMyEvents().success(function (response) {
                $scope.count = response;
            }).error(function () {
                $scope.count = 0;
            });
            // результат метода підрахунку моїх івентів на сьогодні
            $scope.$apply();
        }, 20000 )



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