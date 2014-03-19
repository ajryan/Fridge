module Fridge.Controllers {
    import Food = Fridge.Models.Food;

    export class FridgeCtrl {

        static $inject = [Constants.Services.$scope, Constants.Services.foodResource];

        constructor($scope: Fridge.Models.IFridgeScope, foodResource: Fridge.Services.IFoodResource) {

            $scope.Foods = foodResource.query();
            $scope.EditMode = "Create";
            $scope.EditFood = null;

            $scope.SelectEditFood = (food: Food)=> {
                if ($scope.EditFood)
                    $scope.EditFood.Selected = false;
                food.Selected = true;
                $scope.EditFood = food;
                $scope.EditMode = "Edit";
            };

            $scope.SubmitEdit = () => {
                // TODO: no submit when not valid
                
                if ($scope.EditFood.Id > 0) {
                    var updatedFood = foodResource.update($scope.EditFood);
                    $scope.EditFood.Id = updatedFood.Id;
                } else {
                    var createdFood = foodResource.save($scope.EditFood);
                    $scope.Foods.push(createdFood);
                    $scope.EditFood = null;
                }
                $scope.CreateFood();
            };

            $scope.CreateFood = () => {
                if ($scope.EditFood)
                    $scope.EditFood.Selected = null;
                $scope.EditFood = null;
                $scope.EditMode = "Create";
            };

            $scope.DeleteFood = (food: Food) => {
                foodResource.delete(food, ()=> {
                    var deletedIndex = $scope.Foods.indexOf(food);
                    $scope.Foods.splice(deletedIndex, 1);
                });
            };
        }
    }
} 