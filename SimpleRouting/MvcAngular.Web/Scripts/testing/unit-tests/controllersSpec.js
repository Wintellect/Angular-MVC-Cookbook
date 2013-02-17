/// <reference path="../../angular/angular.js" />
/// <reference path="../../angular/angular-mocks.js" />
/// <reference path="../../app/home/homeCtrl.js" />
/// <reference path="../../app/home/contactCtrl.js" />
/// <reference path="../../app/home/aboutCtrl.js" />
/// <reference path="../jasmine/jasmine.js" />
'use strict';

describe('Home Controller', function() {

    var scope, controller;
    
    beforeEach(function() {
        module('myApp.ctrl.home');
    });

    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        controller = $controller("homeCtrl", {
            $scope: scope
        });
    }));

    it('should expect name to be World', function () {
        expect(scope.name).toBe("World");
    });
});

describe('Contact Controller', function () {

    var scope, controller;

    beforeEach(function () {
        module('myApp.ctrl.contact');
    });

    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        controller = $controller("contactCtrl", {
            $scope: scope
        });
    }));

    it('should expect web address to be correct', function () {
        expect(scope.webSite).toBe("https://github.com/Wintellect/Angular-MVC-Cookbook");
    });
});

describe('About Controller', function () {

    var scope, controller, mockWindow, resizeFxn;
    var currentWidth = 505, currentHeight = 404;

    beforeEach(function() {
        module('myApp.ctrl.about');
    });
    
    beforeEach(inject(function ($controller, $rootScope) {
        scope = $rootScope.$new();
        spyOn(angular, "element").andCallFake(function () {
            mockWindow = jasmine.createSpy('windowElement');
            mockWindow.width = jasmine.createSpy('width').andCallFake(function() {
                return currentWidth;
            });
            mockWindow.height = jasmine.createSpy('height').andCallFake(function () {
                return currentHeight;
            });
            mockWindow.bind = jasmine.createSpy('bind').andCallFake(function (evt, fxn) {
                resizeFxn = fxn;                
            });
            mockWindow.unbind = jasmine.createSpy('unbind');
            return mockWindow;
        });
        controller = $controller("aboutCtrl", {
            $scope: scope
        });
    }));

    it('should initially have expected window width and height', function () {
        expect(scope.windowWidth).toBe(505);
        expect(scope.windowHeight).toBe(404);
    });

    it('should have the expected version number', function () {
        expect(scope.version).toBe("1.0.0");
    });

    it('should bind to the window resize event', function () {
        expect(mockWindow.bind).toHaveBeenCalledWith("resize", jasmine.any(Function));
    });

    it('should update window width and height upon resize event', function () {
        expect(resizeFxn).not.toBeUndefined();
        currentWidth = 606;
        currentHeight = 303;
        resizeFxn();
        expect(scope.windowWidth).toBe(606);
        expect(scope.windowHeight).toBe(303);
    });
});
