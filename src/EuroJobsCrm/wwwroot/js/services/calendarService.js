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
                url: 'api/Calendar/Events',
                method: "POST",
                data: {
                    id:eventId
                }

            });
        }

        events.saveEvent = function (event) {
            return $http({
                url: 'api/Calendar/Events/Save',
                method: "POST",
                data: event
            });
        }

        events.deleteEvent = function (eventId) {
            return $http({
                url: 'api/Calendar/Events/Delete',
                method: "POST",
                data: {
                    id:eventId
                }

            });
        }

        return events;
    }]);