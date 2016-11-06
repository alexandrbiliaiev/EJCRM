var app = angular.module('EuroJobsCrm', ['ngMaterial', 'ngMessages', 'ngRoute', 'pascalprecht.translate', 'angular-jsvat', 'ui.router', 'EuroJobsCrm.services', 'EuroJobsCrm.controllers', 'ng-mfb'])

    .config(function ($translateProvider, $routeProvider, $stateProvider, $mdThemingProvider, $httpProvider) {
        $translateProvider.useStaticFilesLoader({
            prefix: '/languages/',
            suffix: '.json'
        });

        $translateProvider.preferredLanguage('pl');
        $translateProvider.forceAsyncReload(true);
        $mdThemingProvider.theme('dark-blue').backgroundPalette('blue').dark();

        $stateProvider
            .state('error', {
                url: '/server_error',
                templateUrl: 'templates/error_503.html',
            }).state('contragents', {
                url: "/ctg_all",
                templateUrl: 'templates/contragents_list.html',
            }).state('clients', {
                url: "/clt_all",
                templateUrl: 'templates/clients_list.html',
            }).state('contragent', {
                url: "/edit/:id",
                templateUrl: 'templates/contragent_details.html',
            });
    });

angular.module('EuroJobsCrm.services', []);

angular.module('EuroJobsCrm.controllers', []);
