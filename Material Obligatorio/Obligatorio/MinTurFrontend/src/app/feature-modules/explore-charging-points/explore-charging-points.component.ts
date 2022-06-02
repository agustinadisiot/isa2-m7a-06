import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-charging-points',
  templateUrl: './explore-charging-points.component.html',
  styleUrls: []
})
export class ExploreChargingPointsComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public chosenChargingPointId: number;

  private currentRegion: RegionBasicInfoModel;

  constructor(private activatedRoute: ActivatedRoute, private regionService: RegionService, private router: Router) { }

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
