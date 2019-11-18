import { Component, OnInit } from '@angular/core';
import { HallService } from 'src/app/services/hall.service';
import { HallModel } from 'src/app/models/hall.model';
import { ActivatedRoute } from '@angular/router';
import { HallJoinModel } from 'src/app/models/hall-join.model';

@Component({
  selector: 'app-hall-management',
  templateUrl: './hall-management.component.html',
  styleUrls: ['./hall-management.component.scss']
})
export class HallManagementComponent implements OnInit {

  hallId: string;
  hall: HallModel;
  halls: HallModel[];
  hallJoins: HallJoinModel[] = [];
  constructor(private activatedRoute: ActivatedRoute, private hallService: HallService) { }

  ngOnInit() {
    this.hallId = this.activatedRoute.snapshot.paramMap.get('id');
    console.log(this.hallId);
    this.hallService.getHall(this.hallId).subscribe(hall => {
      console.log(hall);
      this.hall = hall;
      this.hallService.getHalls(this.hall.placeId).subscribe(halls => {
        this.halls = halls.filter(h => h.id !== this.hall.id);
        for (const hall of this.halls) {
          const join = this.hall.joins.filter(j => j.hallId === hall.id)[0];
          if (join) {
            join.isPossible = true;
            join.hallName = hall.name;
            this.hallJoins.push(join);
          } else {
            this.hallJoins.push({ hallId: hall.id, isPossible: false , hallName: hall.name });
          }
        }
      });
    });
  }

  submit() {
    this.hallService.updateHallJoins(this.hallId, this.hallJoins).subscribe();
  }
}
