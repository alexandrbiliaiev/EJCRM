angular.module('EuroJobsCrm.controllers').controller('ClientsController',
    function($scope, $location, $http, $state, $translate, $mdDialog, $cookies,
        clientsService) {
        $scope.clients = [];

        $scope.userRole = $cookies.get('user_role');
        $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
        $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User';
        $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User' || $scope.userRole == 'Normal User';

      
        $scope.isActive = false;

        if (clientsService.clients != undefined) {
            $scope.clients = clientsService.clients;
            $scope.isActive = true;
            return;
        }

        clientsService.load().success(function(response) {

            clientsService.clients = response;
            $scope.clients = clientsService.clients;
            $scope.isActive = true;
        }).error(function() {
            $state.go('error');
        });

        $scope.getDefaultClient = function() {
            return {
                id: 0,
                krs: "",
                name: "",
                nip: "",
                regon: "",
                type: 0,
                status: 1,
                branch: ""
            }
        }

        $scope.client = $scope.getDefaultClient();

        $scope.editClient = function(clientId) {
            $state.go('client', {
                id: clientId
            });
        }

        $scope.saveClientClick = function() {
            if ($scope.clientForm.$invalid) {
                return;
            }

            client = $scope.client;

            clientsService.saveClient(client).success(function(response) {
                clientsService.clients.push(response);
                $scope.client = $scope.getDefaultClient();
                $mdDialog.hide();
            }).error(function() {
                $state.go('error');
                $mdDialog.hide();
            });
        }


        $scope.close = function() {
            $mdDialog.hide();
        }

        $scope.showAddClientDialog = function(ev) {
            client = $scope.getDefaultClient();
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                templateUrl: '/templates/clients/client_dialog_tmpl.html',
                targetEvent: ev,
                clickOutsideToClose: true,
            })
                .then(function(answer) {

                }, function() {

                });
        }

        $scope.showDeleteClientConfirmDialog = function(clientId) {
            var confirm = $mdDialog.confirm()
                .title($translate.instant('CLI_DELETE_CONFIRM_TITLE'))
                .textContent($translate.instant('CLT_DELETE_CONFIRM_TEXT'))
                .ariaLabel('label')
                .ok($translate.instant('DELETE_OK'))
                .cancel($translate.instant('DELETE_CANCEL'));

            $mdDialog.show(confirm).then(function() {
                clientsService.deleteClient(clientId).success(function(response) {
                    if (response != true) {
                        return;
                    }

                    clients = clientsService.clients;
                    for (i in clients) {
                        if (clients[i].id != clientId) {
                            continue;
                        }

                        clients.splice(i, 1);
                        return;
                    }

                }).error(function(response) {
                    $state.go('error');
                });
            }, function() {

            });
        };



    });