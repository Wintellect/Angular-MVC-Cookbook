
angular
    .module('myApp', [
        'ngSanitize',
        'myApp.ctrl.home',
        'myApp.ctrl.list',
        'myApp.ctrl.detail',
        'myApp.ctrl.edit',
        'myApp.service.people',
        'myApp.filters.address',
        'myApp.filters.microDataAddress',
        'myApp.directives.microDataAddress',
        'myApp.directives.jqGrid',
        'myApp.directives.jqTemplateGrid',
        'myApp.ctrl.grid'
    ])
    .config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: '/Home/List',
            controller: 'listCtrl'
        });
        $routeProvider.when('/grid-one', {
            templateUrl: '/Home/Grid1',
            controller: 'gridCtrl'
        });
        $routeProvider.when('/grid-two', {
            templateUrl: '/Home/Grid2',
            controller: 'gridCtrl'
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
        $locationProvider.html5Mode(true);
        //$locationProvider.html5Mode(false).hashPrefix('!');

    }]);
