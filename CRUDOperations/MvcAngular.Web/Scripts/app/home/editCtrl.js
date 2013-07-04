
angular
    .module('myApp.ctrl.edit', [])
    .controller('editCtrl', [
        '$scope',
        '$routeParams',
        '$templateCache',
        'peopleService',
        function($scope, $routeParams, $templateCache, peopleService) {

            var editTemplates = {
                'list': '/Home/ContactInfoList',
                'postal': '/Home/EditAddress',
                'phone': '/Home/EditPhone',
                'email': '/Home/EditEmail'
            };

            $scope.person = {
                title: '',
                firstName: '',
                middleName: '',
                lastName: '',
                suffix: ''
            };

            $scope.returnToList = function() {
                $scope.navigationManager.goToListPage();
            };

            $scope.panelId = 'list';
            $scope.contactInfoPanelUrl = editTemplates[$scope.panelId];
            $scope.$watch('panelId', function(panelId) {
                $scope.contactInfoPanelUrl = editTemplates[panelId];
            });

            $scope.save = function () {
                
                if ($scope.isNew) {
                    peopleService
                        .createPerson($scope.person)
                        .success(function(data, status, headers, config) {
                            $scope.navigationManager.goToListPage();
                        })
                        .error(function(data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Create operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                } else {
                    peopleService
                        .updatePerson($scope.person)
                        .success(function (data, status, headers, config) {
                            $scope.navigationManager.goToListPage();
                        })
                        .error(function (data, status, headers, config) {
                            $scope.errorMessage = (data || { message: "Update operation failed." }).message + (' [HTTP-' + status + ']');
                        });
                }
            };

            $scope.isNew = angular.isUndefined($routeParams.id);
            
            if (!$scope.isNew) {
                peopleService
                    .readPerson($routeParams.id)
                    .success(function (data, status, headers, config) {
                        $scope.person = data;
                    });
            }

        }]);
