import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { PlaceFilterModel } from '../models/place-filter.model';

@Injectable({
    providedIn: 'root'
})
export class SidenavService {

    private filterAnnouncedSource = new Subject<PlaceFilterModel>();

    filterAnnounced$ = this.filterAnnouncedSource.asObservable();

    announceFiltering(filterModel: PlaceFilterModel) {
        this.filterAnnouncedSource.next(filterModel);
    }
}
