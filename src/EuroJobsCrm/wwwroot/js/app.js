var app = angular.module('EuroJobsCrm', ['ngMaterial', 'ngMessages', 'ngRoute', 'pascalprecht.translate', 'angular-jsvat', 'ui.router', 'EuroJobsCrm.services', 'EuroJobsCrm.controllers', 'ng-mfb'])

    .config(function ($translateProvider, $routeProvider, $stateProvider, $mdThemingProvider, $httpProvider, $mdDateLocaleProvider) {
        $translateProvider.useStaticFilesLoader({
            prefix: '/languages/',
            suffix: '.json'
        });

        $mdDateLocaleProvider.formatDate = function (date) {
            return moment(date).format('YYYY-MM-DD');
        };

        $translateProvider.useSanitizeValueStrategy('escapeParameters');
        $translateProvider.preferredLanguage('pl');
        $translateProvider.forceAsyncReload(true);
        $mdThemingProvider.theme('dark-blue').backgroundPalette('blue').dark();

        $stateProvider
            .state('error', {
                url: '/server_error',
                templateUrl: 'templates/error_503.html',
            }).state('contragents', {
                url: "/ctg_all",
                templateUrl: 'templates/contragents/contragents_list.html',
            }).state('clients', {
                url: "/clt_all",
                templateUrl: 'templates/clients/clients_list.html',
            }).state('contragent', {
                url: "/ctg_edit/:id",
                templateUrl: 'templates/contragents/contragent_details.html',
            }).state('client', {
                url: "/clt_edit/:id",
                templateUrl: 'templates/clients/client_details.html',
            });
    });

angular.module('EuroJobsCrm.services', []);

angular.module('EuroJobsCrm.controllers', []);
