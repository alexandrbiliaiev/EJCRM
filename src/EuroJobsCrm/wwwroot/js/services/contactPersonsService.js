angular.module('EuroJobsCrm.services')
    .factory('contactpersonsService', ['$http', function ($http) {

        var contactPersons = {};

        contactPersons.load = function () {
            return $http.get('api/ContactPersons');
        };

        contactPersons.saveContactPerson = function (contactperson) {
            return $http({
                url: 'api/ContactPersons/Save',
                method: "POST",
                data: contactperson
            });
        }

        contactPersons.deleteContactPerson = function (id) {
            param = {
                contactPersonId: id
            };
            return $http({
                url: 'api/ContactPersons/Delete',
                method: "POST",
                data: id
            });
        }

        contactPersons.getContactPerson = function (id) {
            for (i in this.contactpersons) {
                if (this.contactpersons[i].id == id) {
                    return this.contactpersons[i];
                }
            }

            return new {
                id : 0,
                name:'',
                licenseNumber: '',
                status: 'a'
            }
        }

        return contactPersons;

    }]);