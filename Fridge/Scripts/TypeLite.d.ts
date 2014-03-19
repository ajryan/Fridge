
declare module Fridge.Models {
interface IFridgeScope {
  Foods: Fridge.Models.Food[];
  EditFood: Fridge.Models.Food;
  EditMode: string;
}
interface Food {
  Id: number;
  Brand: string;
  Name: string;
  Kind: string;
  PortionUnits: string;
  PortionSize: number;
}
}


// TODO: Extend TypeLITE to emit methods and accept extends lists

declare module Fridge.Models {
	interface Food extends ng.resource.IResource<Food> {
	}

	interface IFridgeScope extends ng.IScope {
		SubmitEdit();
		SelectEditFood(food: Food);
		CreateFood();
		DeleteFood(food: Food);
	}

	interface Food {
		Selected: boolean;
	}
}

