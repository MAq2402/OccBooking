import { AdditionalOptionModel } from './additional-option.model';

export class PlaceModel {
    id: string;
    name: string;
    hasRooms: boolean;
    costPerPerson: number;
    description: string;
    street: string;
    city: string;
    zipCode: string;
    province: string;
    additionalOptions: AdditionalOptionModel[];
}
