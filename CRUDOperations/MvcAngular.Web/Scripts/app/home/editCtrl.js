
angular
    .module('myApp.ctrl.edit', [])
    .controller('editCtrl', [
        '$scope',
        '$routeParams',
        '$location',
        'peopleService',
        function($scope, $routeParams, $location, peopleService) {

            $scope.person = {
                title: '',
                firstName: '',
                middleName: '',
                lastName: '',
                suffix: ''
            };

            $scope.returnToList = function() {
                $location.path("/");
            };

            $scope.save = function () {
                
                if ($scope.isNew) {
                    peopleService
                        .createPerson($scope.person)
                        .success(function(data, status, headers, config) {
                            $location.path("/");
                        })
                        .error(function(data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                } else {
                    peopleService
                        .updatePerson($scope.person)
                        .success(function (data, status, headers, config) {
                            $location.path("/");
                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Update operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
            };

            $scope.isNew = angular.isUndefined($routeParams.id);
            
            if (!$scope.isNew) {
                peopleService
                    .readPerson($routeParams.id)
                    .success(function (data, status, headers, config) {
                        $scope.person = data;
                    });
            }

        }]);
