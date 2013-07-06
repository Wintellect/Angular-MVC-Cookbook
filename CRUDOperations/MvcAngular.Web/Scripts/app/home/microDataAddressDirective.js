
angular
    .module('myApp.directives.microDataAddress', [])
    .directive('microDataAddress', [
        function () {
            return {
                restrict: 'E',
                template: '<span itemprop="address" itemscope itemtype="http://schema.org/PostalAddress"></span>',
                link: function (scope, element, attrs) {
                    
                    attrs.$observe('value', function (value) {
                        
                        var addr = scope.$eval(value),
                            parent = element.find('span').empty(),
                            added = false;
                        if (addr) {
                            
                            if (addr.lineOne) {
                                angular
                                    .element('<span itemprop="streetAddress"></span>')
                                    .text(addr.lineOne)
                                    .appendTo(parent);
                                added = true;
                            }
                            if (addr.lineTwo) {
                                if (added) {
                                    parent.append('<br />');
                                }
                                angular
                                    .element('<span itemprop="streetAddress"></span>')
                                    .text(addr.lineTwo)
                                    .appendTo(parent);
                                added = true;
                            }
                            
                            if (added) {
                                parent.append('<br />');
                            }
                            angular
                                .element('<span itemprop="addressLocality"></span>')
                                .text(addr.city)
                                .appendTo(parent);
                            parent.append(', ');
                            angular
                                .element('<span itemprop="addressRegion"></span>')
                                .text(addr.stateProvince)
                                .appendTo(parent);
                            parent.append(' ');
                            angular
                                .element('<span itemprop="postalCode"></span>')
                                .text(addr.postalCode)
                                .appendTo(parent);
                            
                            parent.append('<br />');
                            angular
                                .element('<span itemprop="addressCountry"></span>')
                                .text(addr.country)
                                .appendTo(parent);
                        }
                    });
                }
            };
        }
    ]);
