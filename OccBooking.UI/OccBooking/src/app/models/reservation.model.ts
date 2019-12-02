import { ClientModel } from './client.model';

export class ReservationModel {
    id: string;
    date: Date;
    client: ClientModel;
    cost: number;
    status: string;
    occasion: string;
    amountOfPeople: number;
    placeId: string;
    placeName: string;
    isRejected: boolean;
    isAccepted: boolean;
    isAnswered: boolean;
}