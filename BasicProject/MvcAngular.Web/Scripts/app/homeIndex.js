
angular
	.module('myApp', [])
	.controller('PageCtrl', ['$scope', function ($scope) {

		$scope.name = 'World';
		
		var getCurrentDate = function () {
			var today = new Date();
			var dd = today.getDate();
			var mm = today.getMonth() + 1; //January is 0!

			var yyyy = today.getFullYear();
			if (dd < 10) {
				dd = '0' + dd;
			} if (mm < 10) {
				mm = '0' + mm;
			}
			today = mm + '/' + dd + '/' + yyyy;
			return today;
		};
		
		$scope.date = getCurrentDate();
	}]);
