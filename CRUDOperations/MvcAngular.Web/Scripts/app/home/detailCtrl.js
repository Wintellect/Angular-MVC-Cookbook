
angular
    .module('myApp.ctrl.detail', [])
    .controller('detailCtrl', [
        '$scope',
        '$routeParams',
        '$route',
        '$location',
        'peopleService',
        function($scope, $routeParams, $route, $location, peopleService) {

            $scope.person = {
                title: '',
                firstName: '',
                middleName: '',
                lastName: '',
                suffix: ''
            };

            $scope.isDeleteRequested = !!$route.current.isDeleteRequested;

            $scope.returnToList = function() {
                $location.path("/");
            };

            $scope.deletePerson = function () {
                peopleService
                    .deletePerson($routeParams.id)
                    .success(function(data, status, headers, config) {
                        $location.path("/");
                    })
                    .error(function (data, status, headers, config) {
                        $scope.errorMessage = (data || { message: "Delete operation failed." }).message + (' [HTTP-' + status + ']');
                    });
            };

            peopleService
                .readPerson($routeParams.id)
                .success(function(data, status, headers, config) {
                    $scope.person = data;
                });

        }]);
