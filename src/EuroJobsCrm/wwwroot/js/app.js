var app = angular.module('EuroJobsCrm', ['ngMaterial', 'ngMessages', 'ngRoute', 'pascalprecht.translate', 'angular-jsvat', 'ui.router', 'EuroJobsCrm.services', 'EuroJobsCrm.controllers'])

    .config(function ($translateProvider, $routeProvider, $stateProvider, $mdThemingProvider, $httpProvider) {
        $translateProvider.useStaticFilesLoader({
            prefix: '/languages/',
            suffix: '.json'
        });

        //$translateProvider.preferredLanguage('pl');
        $translateProvider.forceAsyncReload(true);
        $mdThemingProvider.theme('dark-blue').backgroundPalette('blue').dark();

        $stateProvider
            .state('error', {
                url: '/server_error',
                templateUrl: 'templates/error_503.html',
            });
    });

angular.module('EuroJobsCrm.services', []);
angular.module('EuroJobsCrm.controllers', []);
