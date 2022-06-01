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
    this.validateRegionIdParam();
    this.populateExplanationParams();
  }

  public updateChosenChargingPointId(chargingPointId?: number): void{
    this.chosenChargingPointId = chargingPointId;
  }

  private validateRegionIdParam(): void{
    let regionId: number;

    this.activatedRoute.queryParamMap.subscribe(p => regionId = +p.get('regionId'));
    this.regionService.oneRegion(regionId).subscribe(region => this.loadCurrentRegion(region));
  }

  private loadCurrentRegion(region: RegionBasicInfoModel): void{
    this.currentRegion = region;
  }


  private populateExplanationParams(): void{
    this.explanationTitle = 'Elige un Punto de carga';
    this.explanationDescription = 'Selecciona uno de los siguientes puntos turisticos y los detalles sobre su estadia para ver los hospedajes disponibles.';
  }


}
