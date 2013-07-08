
angular.module('myApp.directives.jqGrid', [])
    .directive('jqGrid', [
        function () {
            var uniqueId = 0;
            return {
                restrict: 'E',
                controller: [
                    '$scope',
                    '$element',
                    '$attrs',
                    function($scope, $element, $attrs) {
                        $scope.columns = [];
                        return {
                            addColumn: function(column) {
                                $scope.columns.unshift(column);
                            }
                        };
                    }],
                link: function (scope, element, attrs) {

                    uniqueId += 1;
                    var pagerId = 'dataGridPager' + uniqueId;
                    var pager = $('<div></div>');
                    pager.attr('id', pagerId);
                    
                    var grid = $('<table></table>');
                    grid.attr('id', 'dataGrid' + uniqueId);

                    var container = $('<div></div>');
                    container.append(grid);
                    container.append(pager);

                    element.replaceWith(container);
                    
                    grid.jqGrid({
                        url: '/api/people',
                        datatype: 'json',
                        mtype: 'GET',
                        colModel: scope.columns,
                        pager: pagerId,
                        autowidth: true,
                        height: 'auto',
                        rowNum: 20,
                        sortname: "",
                        sortorder: "asc",
                        viewrecords: true,
                        gridview: true,
                        autoencode: true,
                        caption: "jQuery Grid using AngularJS Directive"
                    });
                }
            };
        }
    ])
    .directive('jqGridColumn', [
        function() {
            return {
                restrict: 'E',
                require: '^jqGrid',
                link: function(scope, element, attrs, jqGridCtrl) {
                    var column = {};
                    for (var prop in attrs) {
                        if (attrs.hasOwnProperty(prop) && !prop.match(/^\$/)) {
                            column[prop] = attrs[prop];
                        }
                    }
                    jqGridCtrl.addColumn(column);
                }
            };
        }
    ]);
