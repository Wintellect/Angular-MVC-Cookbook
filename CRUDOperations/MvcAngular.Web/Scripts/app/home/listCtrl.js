
angular
    .module('myApp.ctrl.list', [])
    .controller('listCtrl', ['$scope', '$http', '$location', function ($scope, $http, $location) {

        $scope.people = [];
        $scope.viewPerson = function (id) {
            $location.path("/detail/" + id);
        };

        $http({
            method: 'GET',
            url: '/api/people'
        }).success(function (data, status, headers, config) {
            $scope.people = data;
        });

    }]);
