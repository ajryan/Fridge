var Fridge;
(function (Fridge) {
    (function (Controllers) {
        var FridgeCtrl = (function () {
            function FridgeCtrl($scope, foodResource) {
                $scope.Foods = foodResource.query();
                $scope.EditMode = "Create";
                $scope.EditFood = null;

                $scope.SelectEditFood = function (food) {
                    if ($scope.EditFood)
                        $scope.EditFood.Selected = false;
                    food.Selected = true;
                    $scope.EditFood = food;
                    $scope.EditMode = "Edit";
                };

                $scope.SubmitEdit = function () {
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

                $scope.CreateFood = function () {
                    if ($scope.EditFood)
                        $scope.EditFood.Selected = null;
                    $scope.EditFood = null;
                    $scope.EditMode = "Create";
                };

                $scope.DeleteFood = function (food) {
                    foodResource.delete(food, function () {
                        var deletedIndex = $scope.Foods.indexOf(food);
                        $scope.Foods.splice(deletedIndex, 1);
                    });
                };
            }
            FridgeCtrl.$inject = [Fridge.Constants.Services.$scope, Fridge.Constants.Services.foodResource];
            return FridgeCtrl;
        })();
        Controllers.FridgeCtrl = FridgeCtrl;
    })(Fridge.Controllers || (Fridge.Controllers = {}));
    var Controllers = Fridge.Controllers;
})(Fridge || (Fridge = {}));
//# sourceMappingURL=fridge.js.map
