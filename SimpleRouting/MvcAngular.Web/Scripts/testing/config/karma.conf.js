// It can be generated using karma init:
// $ karma init my.conf.js
// 
// Which testing framework do you want to use ?
// Press tab to list possible options. Enter to move to the next question.
// > jasmine
// 
// Do you want to use Require.js ?
// This will add Require.js plugin.
// Press tab to list possible options. Enter to move to the next question.
// > no
// 
// Do you want to capture a browser automatically ?
// Press tab to list possible options. Enter empty string to move to the next question.
// > Chrome
// > Firefox
// >
// 
// What is the location of your source and test files ?
// You can use glob patterns, eg. "js/*.js" or "test/**/*Spec.js".
// Enter empty string to move to the next question.
// > *.js
// > test/**/*.js
// >
// 
// Should any of the files included by the previous patterns be excluded ?
// You can use glob patterns, eg. "**/*.swp".
// Enter empty string to move to the next question.
// >
// 
// Do you want Karma to watch all the files and run the tests on change ?
// Press tab to list possible options.
// > yes
//
// Config file generated at "/Users/vojta/Code/karma/my.conf.js".

// http://karma-runner.github.io
// Karma configuration

module.exports = function (config) {
	config.set({

		// base path, that will be used to resolve files and exclude
		basePath: '../../',


		// frameworks to use
		frameworks: ['jasmine'],


		// list of files / patterns to load in the browser
		files: [
			'angular/angular.js',
			'angular/angular-mocks.js',
			'app/**/*.js',
			'testing/unit-tests/**/*.js'
		],


		// list of files to exclude
		exclude: [

		],


		// test results reporter to use
		// possible values: 'dots', 'progress', 'junit', 'growl', 'coverage'
		reporters: ['progress', 'junit'],

		// Please note just about all additional reporters in Karma 
		// (other than progress) require an additional library to be installed (via NPM).
		// https://github.com/karma-runner/karma-junit-reporter 
		junitReporter: {
			outputFile: 'test_out/unit.xml',
			suite: 'unit'
		},

		// web server port
		port: 9876,


		// enable / disable colors in the output (reporters and logs)
		colors: true,


		// level of logging
		// possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
		logLevel: config.LOG_INFO,


		// enable / disable watching file and executing tests whenever any file changes
		autoWatch: true,


		// Start these browsers, currently available:
		// - Chrome
		// - ChromeCanary
		// - Firefox
		// - Opera (has to be installed with `npm install karma-opera-launcher`)
		// - Safari (only Mac; has to be installed with `npm install karma-safari-launcher`)
		// - PhantomJS
		// - IE (only Windows; has to be installed with `npm install karma-ie-launcher`)
		browsers: ['Chrome', 'Firefox'],


		// If browser does not capture in given timeout [ms], kill it
		captureTimeout: 60000,


		// Continuous Integration mode
		// if true, it capture browsers, run tests and exit
		singleRun: true
	});
};
