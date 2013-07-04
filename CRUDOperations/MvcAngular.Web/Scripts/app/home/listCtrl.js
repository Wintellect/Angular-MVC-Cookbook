
angular
    .module('myApp.ctrl.list', [])
    .controller('listCtrl', [
        '$scope',
        '$location',
        'peopleService',
        function($scope, $location, peopleService) {

            $scope.people = [];
            $scope.viewPerson = function(id) {
                $location.path("/detail/" + id);
            };
            $scope.editPerson = function(id) {
                $location.path("/edit/" + id);
            };
            $scope.deletePerson = function(id) {
                $location.path("/delete/" + id);
            };
            $scope.createPerson = function() {
                $location.path("/create");
            };

            peopleService
                .getPeople()
                .success(function(data, status, headers, config) {
                    $scope.people = data.rows;
                });

            $scope.navigationManager.setListPage();

        }]);
