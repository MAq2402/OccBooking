import { AdditionalOptionModel } from './additional-option.model';

export class ReservationRequestModel {
    clientFirstName: string;
    clientLastName: string;
    clientPhoneNumber: string;
    clientEmail: string;
    date: Date;
    options: AdditionalOptionModel[];
    occasionType: string;
}