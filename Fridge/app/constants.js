var Fridge;
(function (Fridge) {
    'use strict';

    var Constants = (function () {
        function Constants() {
        }
        Constants.AppName = 'FridgeApp';

        Constants.Services = {
            $scope: '$scope',
            $resource: '$resource',
            foodResource: 'employeeResource'
        };
        return Constants;
    })();
    Fridge.Constants = Constants;
})(Fridge || (Fridge = {}));
//# sourceMappingURL=constants.js.map
