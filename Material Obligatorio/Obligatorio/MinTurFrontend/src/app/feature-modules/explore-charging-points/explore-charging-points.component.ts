import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-charging-points',
  templateUrl: './explore-charging-points.component.html',
  styleUrls: []
})
export class ExploreChargingPointsComponent implements OnInit {
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