module Fridge.Services {
    'use strict';

    import Food = Fridge.Models.Food;

    export interface IFoodResource extends ng.resource.IResourceClass<Food> {
        update(food: Food): Food;
    }

    Fridge.App.factory(Constants.Services.foodResource, [
        Constants.Services.$resource,
        ($resource: ng.resource.IResourceService) : IFoodResource => {
            var updateAction: ng.resource.IActionDescriptor = {
                method: 'PUT', isArray: false
            };

            return <IFoodResource> $resource('/api/Food/:Id', { Id: '@Id' }, {
                update: updateAction
            });
        }
    ]);
}