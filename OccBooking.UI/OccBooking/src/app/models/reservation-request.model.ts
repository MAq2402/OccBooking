import { AdditionalOptionModel } from './additional-option.model';
import { MenuOrderModel } from '../place-details/make-reservation/make-reservation.component';

export class ReservationRequestModel {
    clientFirstName: string;
    clientLastName: string;
    clientPhoneNumber: string;
    clientEmail: string;
    date: Date;
    options: AdditionalOptionModel[];
    occasionType: string;
    menuOrders: MenuOrderModel[];
}