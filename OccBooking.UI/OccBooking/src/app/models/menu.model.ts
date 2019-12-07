import { MealModel } from './meal.model';

export class MenuModel {
    id: string;
    name: string;
    type: number;
    costPerPerson: number;
    meals: MealModel[];
}
