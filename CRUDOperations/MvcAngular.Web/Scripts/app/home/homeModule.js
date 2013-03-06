
angular
    .module('myApp', [
        'myApp.ctrl.list',
        'myApp.ctrl.detail'
    ])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        
        // Specify the three simple routes ('/', '/About', and '/Contact')
        $routeProvider.when('/', {
            templateUrl: '/Home/List',
            controller: 'listCtrl',
        });
        $routeProvider.when('/detail/:id', {
            templateUrl: '/Home/Detail',
            controller: 'detailCtrl',
        });
        $routeProvider.otherwise({
            redirectTo: '/'
        });

        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');

    }]);
