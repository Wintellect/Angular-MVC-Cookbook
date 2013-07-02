
angular
    .module('myApp.ctrl.grid', [])
    .controller('gridCtrl', [
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

            //peopleService
            //    .getPeople()
            //    .success(function(data, status, headers, config) {
            //        $scope.people = data.rows;
            //    });

        }]);
