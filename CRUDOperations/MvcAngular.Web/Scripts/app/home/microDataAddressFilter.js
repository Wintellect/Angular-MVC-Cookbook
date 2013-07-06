
// Note: this is an anti-pattern.  The sanitize functionality of AngularJS
// will remove the microdata tags which limits the usefulness of this filter.
angular
    .module('myApp.filters.microDataAddress', [])
    .filter('microdataaddress', [
        function() {
            return function (addr) {
                
                // From http://schema.org/PostalAddress
                
                var output = '',
                    br = '<br />';
                if (addr.lineOne) {
                    output += [
                        '<span itemprop="streetAddress">',
                        addr.lineOne,
                        '</span>'
                    ].join('');
                }
                if (addr.lineTwo) {
                    output += [
                        (output ? br : ''),
                        '<span itemprop="streetAddress">',
                        addr.lineTwo,
                        '</span>'
                    ].join('');
                }
                output += [
                    (output ? br : ''),
                    '<span itemprop="addressLocality">',
                    addr.city,
                    '</span>, ',
                    '<span itemprop="addressRegion">',
                    addr.stateProvince,
                    '</span> ',
                    '<span itemprop="postalCode">',
                    addr.postalCode,
                    '</span>'
                ].join('');
                
                output += [
                    br,
                    '<span itemprop="addressCountry">',
                    addr.country,
                    '</span>'
                ].join('');
                
                output = [
                    '<span itemprop="address" itemscope itemtype="http://schema.org/PostalAddress">',
                    output,
                    '</span>'
                ].join('');

                return output;
            };
        }]);
