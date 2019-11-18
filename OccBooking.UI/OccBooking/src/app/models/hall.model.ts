import { HallJoinModel } from './hall-join.model';

export class HallModel {
    id: string;
    name: string;
    capacity: number;
    placeId: string;
    joins: HallJoinModel[];
}
