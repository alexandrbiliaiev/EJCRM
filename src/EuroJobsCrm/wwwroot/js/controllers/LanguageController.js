angular.module('EuroJobsCrm.controllers').controller('LanguageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService,
        employeesService, fileService, usersService) {

        $scope.currentLang = $cookies.get('user_lng');

        $scope.$location = $location;


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