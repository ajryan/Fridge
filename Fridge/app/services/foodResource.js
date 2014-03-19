var Fridge;
(function (Fridge) {
    (function (Services) {
        'use strict';

        Fridge.App.factory(Fridge.Constants.Services.foodResource, [
            Fridge.Constants.Services.$resource,
            function ($resource) {
                var updateAction = {
                    method: 'PUT', isArray: false
                };

                return $resource('/api/Food/:Id', { Id: '@Id' }, {
                    update: updateAction
                });
            }
        ]);
    })(Fridge.Services || (Fridge.Services = {}));
    var Services = Fridge.Services;
})(Fridge || (Fridge = {}));
//# sourceMappingURL=foodResource.js.map
