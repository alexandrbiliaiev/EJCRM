angular.module('EuroJobsCrm.controllers').controller('OfferManageController', function($scope, $location, $translate, $http, $state,
    offersService, $mdDialog, $routeParams, $cookies, employeesService) {
    $scope.expandDetails = true;
    $scope.expandCandidates = true;
    $scope.expandEmployees = true;

    $scope.userRole = $cookies.get('user_role');
    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.acceptClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.rejectClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced User' || $scope.userRole == 'Normal User';


    $scope.moment = moment;

    $scope.awaitingCandidates = [];

    $scope.goBack = function() {
        $state.go('offers');
    }

    $scope.close = function() {
        console.log($scope);
        $mdDialog.hide();
    }

    $scope.getFormatedDate = function(date) {
        return moment(date).format('YYYY-MM-DD');
    };

    $scope.showAddCandidateDialog = function() {
        $scope.manageFreeEmployees();
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/offers/candidates_dialog_tmpl.html',

            clickOutsideToClose: true,
        })
            .then(function(answer) {

            }, function() {

            });
    };

    $scope.addCandidateToOffer = function(emp) {
        candidateRequest = {
            offerId: $scope.offer.id,
            clientId: null,
            employeeId: emp.id,
            clientId: $scope.offer.clientId,
            status: 0
        };

        offersService.saveCandidateRequest(candidateRequest).success(function(response) {
            $scope.offer.candidates.push(emp);

            $mdDialog.hide();
        }).error(function() {
            $state.go('error');
            $mdDialog.hide();
        });


    }

    $scope.countCandidates = function() {
        for (i in $scope.offer.employmentRequests) {

        }
    }

    $scope.deleteCandidateFromOffer = function(emp) {
        candidateRequest = {
            offerId: $scope.offer.id,
            employeeId: emp.id,
            status: 0
        };

        var confirm = $mdDialog.confirm()
            .title($translate.instant('CLI_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('CLT_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function() {
            offersService.deleteCandidateRequest(candidateRequest).success(function(response) {
                if (response != true) {
                    return;
                }

                for (i in $scope.offer.candidates) {
                    if ($scope.offer.candidates[i].id != emp.id) {
                        continue;
                    }
                    $scope.freeEmployees.push($scope.offer.candidates[i]);
                    $scope.offer.candidates.splice(i, 1);
                    return;
                }



            }).error(function(response) {
                $state.go('error');
            });
        }, function() {

        });


    }


    $scope.acceptCandidateToOffer = function(emp) {

        candidateRequest = {
            offerId: $scope.offer.id,
            clientId: $scope.offer.clientId,
            employeeId: emp.id,
            status: 1
        };

        offersService.pimpCandidateToEmployee(candidateRequest).success(function(response) {
            $scope.offer.employees.push(emp);

            for (i in $scope.offer.candidates) {
                if ($scope.offer.candidates[i].id != emp.id) {
                    continue;
                }
                $scope.offer.candidates.splice(i, 1);
                return;
            }

        }).error(function() {
            $state.go('error');
        });
    };

    $scope.rejectCandidateFromOffer = function(emp) {

        candidateRequest = {
            offerId: $scope.offer.id,
            employeeId: emp.id,
            status: 2
        };

        offersService.pimpCandidateToEmployee(candidateRequest).success(function(response) {

            for (i in $scope.offer.candidates) {
                if ($scope.offer.candidates[i].id != emp.id) {
                    continue;
                }
                $scope.offer.candidates.splice(i, 1);
                return;
            }

        }).error(function() {
            $state.go('error');
        });
    };

    offersService.getOffer($state.params.id).success(function(response) {
        $scope.offer = response;
    }).error(function() {
        $state.go('error');
    });

    employeesService.load().success(function(response) {
        employeesService.employees = response;
        $scope.freeEmployees = employeesService.employees;
        $scope.manageFreeEmployees();
    }).error(function() {
        $state.go('error');
    });


    $scope.manageFreeEmployees = function() {

        for (i in $scope.offer.candidates) {
            for (j in $scope.freeEmployees) {
                if ($scope.offer.candidates[i].id == $scope.freeEmployees[j].id) {
                    $scope.freeEmployees.splice(j, 1);
                }
            }
        }

        for (i in $scope.offer.employees) {
            for (j in $scope.freeEmployees) {
                if ($scope.offer.employees[i].id == $scope.freeEmployees[j].id) {
                    $scope.freeEmployees.splice(j, 1);
                }

            }
        }
    }

});