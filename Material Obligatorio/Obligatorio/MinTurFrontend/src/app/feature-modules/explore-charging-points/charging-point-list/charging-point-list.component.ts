import { HttpErrorResponse } from '@angular/common/http';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
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
  public errorLoadingChargingPoints = null;
  @Output() chosenChargingPointIdChange = new EventEmitter<number>();
  @Input() reloadChargingPoint = new EventEmitter();
  constructor(private activatedRoute: ActivatedRoute, private chargingPointService: ChargingPointService,
               private router: Router, public deleteDialog: MatDialog) { }

  ngOnInit(): void{
    this.retrieveComponentData();
    this.subscribeToChangesInRoute();
  }

  public retrieveComponentData(): void{
    this.loadChargingPoint();
  }

  public loadChargingPoint(): void{
    this.chargingPointService.allChargingPoints()
    .subscribe(chargingPoints => this.chargingPoints = chargingPoints, (error: HttpErrorResponse) => {
      this.showError(error)
      this.errorLoadingChargingPoints = "No hay puntos de carga por el momento."
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
  
  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public deleteChargingPoint()
  {
    this.chargingPointService.deleteChargingPoint(this.chosenChargingPointId)
    .subscribe( () => this.loadChargingPoint(), (error: HttpErrorResponse) => {
      alert(error.message)
    }); 
  }

  public chargingPointForm(): void{
    this.router.navigateByUrl("/explore/create-charging-point");
  }
}
