basePath = '../../';

files = [
    JASMINE,
    JASMINE_ADAPTER,
    'angular/angular.js',
    'angular/angular-bootstrap.js',
    'angular/angular-bootstrap-prettify.js',
    'angular/angular-cookies.js',
    'angular/angular-loader.js',
    'angular/angular-resource.js',
    'angular/angular-sanitize.js',
    'angular/angular-scenario.js',
    'angular/angular-mocks.js',
    'app/**/*.js',
    'testing/unit-tests/**/*.js'
];

autoWatch = true;
logLevel = LOG_INFO; //LOG_DEBUG;
browsers = ['Chrome', 'IE'];
singleRun = true;

junitReporter = {
    outputFile: 'test_out/unit.xml',
    suite: 'unit'
};
