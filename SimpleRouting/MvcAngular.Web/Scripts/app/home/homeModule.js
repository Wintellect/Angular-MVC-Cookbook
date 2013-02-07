
angular
    .module('myApp', [
        'myApp.ctrl.home',
        'myApp.ctrl.contact',
        'myApp.ctrl.about'
    ])
    .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        
        // Specify the three simple routes ('/', '/About', and '/Contact')
        $routeProvider.when('/', {
            templateUrl: '/Home/Home',
            controller: 'homeCtrl',
        });
        $routeProvider.when('/About', {
            templateUrl: '/Home/About',
            controller: 'aboutCtrl',
        });
        $routeProvider.when('/Contact', {
            templateUrl: '/Home/Contact',
            controller: 'contactCtrl'
        });
        $routeProvider.otherwise({
            redirectTo: '/'
        });

        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');

    }]);
