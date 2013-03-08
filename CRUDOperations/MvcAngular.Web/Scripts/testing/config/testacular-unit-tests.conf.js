basePath = '../../';

files = [
    JASMINE,
    JASMINE_ADAPTER,
    'angular/angular.js',
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
