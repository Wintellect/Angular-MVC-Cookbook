
angular
    .module('myApp.ctrl.detail', [])
    .controller('detailCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $scope.person = {
            title: '',
            firstName: '',
            middleName: '',
            lastName: '',
            suffix: ''
        };

        $scope.returnToList = function () {
            $location.path("/");
        };
        
        $http({
            method: 'GET',
            url: '/api/people/' + $routeParams.id
        }).success(function (data, status, headers, config) {
            $scope.person = data;
        });

    }]);
