angular.module('EuroJobsCrm.controllers').controller('LanguageController',
    function ($scope, $location, $translate, $http, $state, $mdDialog, $routeParams, $cookies,
        clientsService, countriesService, contactpersonsService, addressesService, offersService,
        employeesService, fileService) {


        $scope.setLanguage = setLanguage;

        function setLanguage(lang) {
            $cookies.__APPLICATION_LANGUAGE = lang;
            $translate.use(lang);
        }

        function init() {
            var lang = $cookies.__APPLICATION_LANGUAGE || 'en';
            setLanguage(lang);
        }

        init();

    })