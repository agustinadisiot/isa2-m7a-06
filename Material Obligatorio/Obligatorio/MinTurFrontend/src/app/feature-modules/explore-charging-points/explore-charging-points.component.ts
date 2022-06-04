import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ChargingPointService } from 'src/app/core/http-services/charging-point/charging-point.service';

@Component({
  selector: 'app-charging-points',
  templateUrl: './explore-charging-points.component.html',
  styleUrls: []
})
export class ExploreChargingPointsComponent implements OnInit {
  constructor(private router: Router, ) {

  }
  public explanationTitle: string;
  public explanationDescription: string;
  public chosenChargingPointId: number;

  ngOnInit(): void{
    this.populateExplanationParams();
  }

  public updateChosenChargingPointId(chargingPointId?: number): void{
    this.chosenChargingPointId = chargingPointId;
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Estos son todos los Puntos de carga';
    this.explanationDescription = 'Si conoce alguno más, por favor agrégelo. Si alguno esta desactualizado, por favor elimínelo.';
  }

}
