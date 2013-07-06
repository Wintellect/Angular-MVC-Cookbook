
angular
    .module('myApp.filters.address', [])
    .filter('address', [
        function() {
            return function(addr) {
                var output = '',
                    br = '<br />';
                if (addr.lineOne) {
                    output += addr.lineOne;
                }
                if (addr.lineTwo) {
                    output += (output ? br : '') + addr.lineTwo;
                }
                output += [
                    (output ? br : ''),
                    addr.city,
                    ', ',
                    addr.stateProvince,
                    ' ',
                    addr.postalCode
                ].join('');
                output += br + addr.country;
                return output;
            };
        }]);
