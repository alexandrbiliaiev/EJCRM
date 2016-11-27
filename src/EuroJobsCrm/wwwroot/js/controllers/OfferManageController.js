angular.module('EuroJobsCrm.controllers').controller('OfferManageController', function ($scope, $location, $translate, $http, $state,
    offersService, $mdDialog, $routeParams, employeesService) {
    $scope.expandDetails = true;
    $scope.expandCandidates = true;
    $scope.expandEmployees = true;
    $scope.expandEmployees = true;

    $scope.moment = moment;

    $scope.goBack = function () {
        $state.go('offers');
    }

    $scope.close = function () {
        console.log($scope);
        $mdDialog.hide();
    }

    $scope.getFormatedDate = function (date) {
        return moment(date).format('YYYY-MM-DD');
    };

    $scope.showAddCandidateDialog = function () {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/offers/candidates_dialog_tmpl.html',

            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {

            });
    };

    $scope.addCandidateToOffer = function (emp) {
        candidateRequest = {
            offerId: $scope.offer.id,
            employeeId: emp.id,
            status: 0
        };

        offersService.saveCandidateRequest(candidateRequest).success(function (response) {
            $scope.offer.candidates.push(emp);

            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });

    }

    $scope.acceptCandidateToOffer = function (emp) {

        candidateRequest = {
            offerId: $scope.offer.id,
            employeeId: emp.id,
            status: 1
        };

        offersService.pimpCandidateToEmployee(candidateRequest).success(function (response) {
            $scope.offer.employees.push(emp);

            for (i in $scope.offer.candidates) {
                if ($scope.offer.candidates[i].id != emp.id) {
                    continue;
                }

                $scope.offer.candidates.splice(i, 1);
                return;
            }

        }).error(function () {
            $state.go('error');
        });
    };

    offersService.getOffer($state.params.id).success(function (response) {
        $scope.offer = response;
    }).error(function () {
        $state.go('error');
    });

    employeesService.load().success(function (response) {
        employeesService.employees = response;
        $scope.freeEmployees = employeesService.employees;
    }).error(function () {
        $state.go('error');
    });




});