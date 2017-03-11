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

Date.prototype.addHours = function(hours) {
    this.setTime(this.getTime() + hours * 60 * 60 * 1000);
    return this;
}

var app = angular.module('EuroJobsCrm', ['ngMaterial', 'ngMessages', 'ngFileUpload', 'ngRoute', 'ngCookies', 'pascalprecht.translate', 'angular-jsvat', 'ui.router', 'EuroJobsCrm.services', 'EuroJobsCrm.controllers', 'ng-mfb', 'ngMaterialDatePicker', 'daypilot', 'ui-notification'])

    .config(function ($translateProvider, $routeProvider, $stateProvider, $mdThemingProvider, $httpProvider, $mdDateLocaleProvider, NotificationProvider) {
        $translateProvider.useStaticFilesLoader({
            prefix: '/languages/',
            suffix: '.json'
        });

        $mdDateLocaleProvider.formatDate = function (date) {
            return moment(date).format('YYYY-MM-DD');
        };

        $translateProvider.useSanitizeValueStrategy('escapeParameters');
        //$translateProvider.preferredLanguage('pl');
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
            }).state('employee', {
                url: "/emp_edit/:id",
                templateUrl: 'templates/employees/employee_details.html',
            }).state('employees', {
                url: "/emp_all",
                templateUrl: 'templates/employees/employees_list.html',
            }).state('offers', {
                url: "/off_all",
                templateUrl: 'templates/offers/offers_list.html',
            }).state('offer', {
                url: "/off_edit/:id",
                templateUrl: 'templates/offers/offer_details.html',
            }).state('users', {
                url: "/usr_all",
                templateUrl: 'templates/users/users_list.html',
            }).state('calendar', {
                url: "/cal",
                templateUrl: 'templates/calendar/calendar.html',
            });


        NotificationProvider.setOptions({
            delay: 10000,
            startTop: 20,
            startRight: 10,
            verticalSpacing: 20,
            horizontalSpacing: 20,
            positionX: 'right',
            positionY: 'bottom'
        });
    });



app.run(function ($rootScope) {
    $rootScope.$on('$stateChangeSuccess', function (event, to, toParams, from, fromParams) {
        //save the previous state in a rootScope variable so that it's accessible from everywhere
        $rootScope.previousState = from;
        $rootScope.previousStateParams = fromParams;
    });
});

angular.module('EuroJobsCrm.filters', []);
angular.module('EuroJobsCrm.services', []);
angular.module('EuroJobsCrm.controllers', []);



