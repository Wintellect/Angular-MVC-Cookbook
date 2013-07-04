
angular
    .module('myApp.ctrl.grid', [])
    .controller('gridCtrl', [
        '$scope',
        '$location',
        function ($scope, $location) {

            $scope.createPerson = function() {
                $location.path("/create");
            };
            
            $scope.navigationManager.setListPage();

        }])
    .controller('gridRowCtrl', [
        '$scope',
        '$location',
        function ($scope, $location) {

            $scope.view = function () {
                $location.path("/detail/" + $scope.data.personId);
            };
            $scope.edit = function () {
                $location.path("/edit/" + $scope.data.personId);
            };
            $scope.delete = function () {
                $location.path("/delete/" + $scope.data.personId);
            };

        }]);
