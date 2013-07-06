
angular
    .module('myApp.ctrl.detail', [])
    .controller('detailCtrl', [
        '$scope',
        '$routeParams',
        '$route',
        'peopleService',
        function($scope, $routeParams, $route, peopleService) {

            $scope.person = {
                title: '',
                firstName: '',
                middleName: '',
                lastName: '',
                suffix: ''
            };

            $scope.isDeleteRequested = !!$route.current.isDeleteRequested;

            $scope.deletePerson = function () {
                peopleService
                    .deletePerson($routeParams.id)
                    .success(function (data, status, headers, config) {
                        $scope.navigationManager.goToListPage();
                    })
                    .error(function (data, status, headers, config) {
                        $scope.errorMessage = (data || { message: "Delete operation failed." }).message + (' [HTTP-' + status + ']');
                    });
            };

            $scope.returnToList = function () {
                $scope.navigationManager.goToListPage();
            };

            peopleService
                .readPerson($routeParams.id)
                .success(function(data, status, headers, config) {
                    $scope.person = data;
                });

        }]);
