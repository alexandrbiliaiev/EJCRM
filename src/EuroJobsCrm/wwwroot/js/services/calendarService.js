angular.module('EuroJobsCrm.services')
    .factory('calendarService', ['$http', function ($http) {
        var events = {};

        events.load = function () {
            return $http({
                url: 'api/Calendar/Events',
                method: "GET",
            });
        }

        events.getEvent = function (eventId) {
            return $http({
                url: 'api/Calendar/Event',
                method: "POST",
                data: {
                    id:eventId
                }

            });
        }

        return events;
    }]);