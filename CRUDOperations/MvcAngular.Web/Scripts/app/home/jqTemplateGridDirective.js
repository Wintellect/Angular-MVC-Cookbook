
angular.module('myApp.directives.jqTemplateGrid', [])
    .directive('jqTemplateGrid', [
        '$controller',
        '$compile',
        function ($controller, $compile) {
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
                        caption: "jQuery Grid using AngularJS Directive",
                        loadComplete: function (data) {

                            var count = data.rows.length,
                                trList = grid.find("tr.jqgrow"),
                                tr, idx, rowScope, ctrl, lnk;
                            
                            for (idx = 0; idx < count; idx += 1) {

                                tr = $(trList[idx]);
                                lnk = $compile(tr.contents());
                                
                                rowScope = scope.$new();
                                rowScope.data = data.rows[idx];
                                ctrl = $controller('gridRowCtrl', {
                                    $scope: rowScope
                                });
                                tr.data('$ngControllerController', ctrl);
                                lnk(rowScope);
                                
                                // TODO: call $destroy() on rowScope when finished to address memory leak.
                            }
                        }
                    });
                }
            };
        }
    ])
    .directive('jqTemplateGridColumn', [
        '$templateCache',
        function($templateCache) {
            return {
                restrict: 'E',
                require: '^jqTemplateGrid',
                link: function(scope, element, attrs, jqGridCtrl) {
                    var column = {}, template, propVal;
                    for (var prop in attrs) {
                        if (attrs.hasOwnProperty(prop) && !prop.match(/^\$/)) {
                            propVal = attrs[prop];
                            switch (prop) {
                                case 'width':
                                    column.width = propVal - 0;
                                    break;
                                case 'sortable':
                                    column.sortable = !(propVal === 'false');
                                    break;
                                case 'template':
                                    template = $templateCache.get(propVal);
                                    column.formatter = function (cellvalue, options, rowObject) {
                                        return template;
                                    };
                                    break;
                                default:
                                    column[prop] = propVal;
                                    break;
                            }
                        }
                    }
                    column.title = false;
                    jqGridCtrl.addColumn(column);
                }
            };
        }
    ]);
