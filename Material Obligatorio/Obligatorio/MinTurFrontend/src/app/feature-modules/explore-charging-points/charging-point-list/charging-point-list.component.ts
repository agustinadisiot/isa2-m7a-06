import { HttpErrorResponse } from '@angular/common/http';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ChargingPointService } from 'src/app/core/http-services/charging-point/charging-point.service';
import { ChargingPointBasicInfoModel } from 'src/app/shared/models/out/charging-point-basic-info-model';

@Component({
  selector: 'app-charging-point-list',
  templateUrl: './charging-point-list.component.html',
  styleUrls: []
})
export class ChargingPointListComponent implements OnInit {
  public chargingPoints: ChargingPointBasicInfoModel[] = [];
  public chosenChargingPointId?: number;
  @Output() chosenChargingPointIdChange = new EventEmitter<number>();
  constructor(private activatedRoute: ActivatedRoute, private chargingPointService: ChargingPointService,
               private router: Router) { }

  ngOnInit(): void{
    this.retrieveComponentData();
    this.subscribeToChangesInRoute();
  }

  public retrieveComponentData(): void{
    //this.loadParams();

    this.chargingPointService.allChargingPoints()
      .subscribe(chargingPoints => this.loadChargingPoint(chargingPoints), (error: HttpErrorResponse) => {
        this.showError(error)
      });
  }

  public chooseChargingPoint(chargingPointId?: number): void{
    if (chargingPointId === this.chosenChargingPointId){
      this.chosenChargingPointId = null;
    }else{
      this.chosenChargingPointId = chargingPointId;
    }

    this.chosenChargingPointIdChange.emit(this.chosenChargingPointId);
  }

  public isChargingPointCardChosen(chargingPointId: number): boolean{
    return this.chosenChargingPointId === chargingPointId;
  }

  private subscribeToChangesInRoute(): void{
    this.router.events.subscribe(val => {
      if (val instanceof NavigationEnd){
        this.retrieveComponentData();
        this.checkIfChosenChargingPointIsStillAvailable();
      }
    });
  }

  private checkIfChosenChargingPointIsStillAvailable(): void{
    let chargingPointIsStillAvailable = false;

    this.chargingPoints.forEach(chargingPoint => {
      if (chargingPoint.id === this.chosenChargingPointId){
        chargingPointIsStillAvailable = true;
      }
    });

    if (!chargingPointIsStillAvailable){
      this.chooseChargingPoint(null);
    }
  }

  // private loadParams(): void{
  //   let regionIdParam: number;
  //
  //   this.activatedRoute.queryParamMap.subscribe(param => {
  //     if (param.has('regionId')){
  //       regionIdParam = +param.get('regionId');
  //     }else{
  //       this.router.navigate([''], {replaceUrl: true});
  //     }
  //   });
  //
  // }

  private loadChargingPoint(chargingPoints: ChargingPointBasicInfoModel[]): void{
    this.chargingPoints = chargingPoints;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }
}
