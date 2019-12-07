import { AdditionalOptionModel } from './additional-option.model';
import { OccasionTypeMapModel } from './occasion-type-map';
import { OwnerModel } from './owner.model';

export class PlaceModel {
    id: string;
    name: string;
    hasRooms: boolean;
    minimalCostPerPerson?: number;
    description: string;
    street: string;
    city: string;
    zipCode: string;
    province: string;
    additionalOptions: AdditionalOptionModel[];
    occasionTypes: string[];
    occasionTypesMaps: OccasionTypeMapModel[];
    image: any;
    isConfigured: boolean;
    owner: OwnerModel;
}
