angular.module('EuroJobsCrm.controllers').controller('OffersController', function ($scope, $location, $http, $state, $translate, $mdDialog, offersService, contragentsService, $cookies) {
    $scope.offers = [];

    $scope.moment = moment;

    $scope.userRole = $cookies.get('user_role');
    $scope.ctgId = $cookies.get('ctg_id');
    $scope.deleteClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin';
    $scope.editClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user';
    $scope.addClaim = $scope.userRole == 'Admin' || $scope.userRole == 'Super Admin' || $scope.userRole == 'Advanced user' || $scope.userRole == 'Normal user';
    $scope.detailClaim = $scope.addClaim;
    $scope.Saving = false;
    $scope.isActive = false;
    $scope.expandOffers = true;
    $scope.currentLang;

    $scope.awaitingCandidates = [];

    $scope.goToOffers = function() {
        $state.go('offers');
    }

    $scope.countCandidates = function () {
        for (i in $scope.offer.employmentRequests) {

        }
    }

    $scope.editOffer = function (offerId) {
        $state.go('offer', {
            id: offerId
        });
    }

    $scope.sendNotifaction = false;
    $scope.saveofferClick = function () {
        if ($scope.offerForm.$invalid) {
            return;
        }
        var offer = $scope.offer;
        offersService.saveoffer(offer).success(function (response) {
            offersService.offers.push(response);
            $scope.offer = $scope.getDefaultoffer();
            $mdDialog.hide();
        }).error(function () {
            $state.go('error');
            $mdDialog.hide();
        });
    }

    $scope.close = function () {
        $mdDialog.hide();
    }

    $scope.showAddofferDialog = function (ev) {
        offer = $scope.getDefaultoffer();
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            templateUrl: '/templates/offers/offer_dialog_tmpl.html',
            targetEvent: ev,
            clickOutsideToClose: true,
        })
            .then(function (answer) {

            }, function () {
            });
    }

    $scope.showDeleteofferConfirmDialog = function (offerId) {
        var confirm = $mdDialog.confirm()
            .title($translate.instant('CLI_DELETE_CONFIRM_TITLE'))
            .textContent($translate.instant('CLT_DELETE_CONFIRM_TEXT'))
            .ariaLabel('label')
            .ok($translate.instant('DELETE_OK'))
            .cancel($translate.instant('DELETE_CANCEL'));

        $mdDialog.show(confirm).then(function () {
            offersService.deleteoffer(offerId).success(function (response) {
                if (response != true) {
                    return;
                }

                offers = offersService.offers;
                for (i in offers) {
                    if (offers[i].id != offerId) {
                        continue;
                    }

                    offers.splice(i, 1);
                    return;
                }

            }).error(function (response) {
                $state.go('error');
            });
        }, function () {

        });
    };

    if (offersService.offers != undefined) {
        $scope.offers = offersService.offers;
        return;
    }

    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
        $scope.currentLang = $translate.proposedLanguage() || $translate.use();
    };

    offersService.load().success(function (response) {
        offersService.offers = response;
        $scope.offers = offersService.offers;
        $scope.isActive = true;
    }).error(function () {
        $scope.isActive = true;
        $state.go('error');
    });

});