
angular
    .module('myApp', [
        'myApp.ctrl.list',
        'myApp.ctrl.detail',
        'myApp.ctrl.edit',
        'myApp.service.people'
    ])
    .config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Home/List',
            controller: 'listCtrl'
        });
        $routeProvider.when('/detail/:id', {
            templateUrl: '/Home/Detail',
            controller: 'detailCtrl'
        });
        $routeProvider.when('/edit/:id', {
            templateUrl: '/Home/Edit',
            controller: 'editCtrl'
        });
        $routeProvider.when('/create', {
            templateUrl: '/Home/Edit',
            controller: 'editCtrl'
        });
        $routeProvider.when('/delete/:id', {
            templateUrl: '/Home/Detail',
            controller: 'detailCtrl',
            isDeleteRequested: true
        });
        $routeProvider.otherwise({
            redirectTo: '/'
        });

        // Specify HTML5 mode (using the History APIs) or HashBang syntax.
        $locationProvider.html5Mode(false).hashPrefix('!');

    }]);
