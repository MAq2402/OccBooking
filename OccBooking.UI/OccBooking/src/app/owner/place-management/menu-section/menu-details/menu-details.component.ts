import { Component, OnInit, Inject } from '@angular/core';
import { MenuModel } from 'src/app/models/menu.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-menu-details',
  templateUrl: './menu-details.component.html',
  styleUrls: ['./menu-details.component.scss']
})
export class MenuDetailsComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<MenuDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public menu: MenuModel[]) { }

  ngOnInit() {
  }

  close(): void {
    this.dialogRef.close();
  }
}
