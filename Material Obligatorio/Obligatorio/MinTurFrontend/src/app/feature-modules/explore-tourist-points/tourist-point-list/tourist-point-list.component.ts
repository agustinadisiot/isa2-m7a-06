import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { ImageSanitizerService } from 'src/app/core/http-services/image-sanitizer/image-sanitizer.service';
import { TouristPointService } from 'src/app/core/http-services/tourist-point/tourist-point.service';
import { TouristPointSearchModel } from 'src/app/shared/models/in/tourist-point-search-model';
import { TouristPointBasicInfoModel } from 'src/app/shared/models/out/tourist-point-basic-info-model';

@Component({
  selector: 'app-charging-point-list',
  templateUrl: './tourist-point-list.component.html',
  styleUrls: []
})
export class TouristPointListComponent implements OnInit {
  public touristPoints: TouristPointBasicInfoModel[] = [];
  private touristPointSearchModel: TouristPointSearchModel;
  public chosenTouristPointId?: number;
  @Output() chosenTouristPointIdChange = new EventEmitter<number>();

  constructor(private activatedRoute: ActivatedRoute, private toruistPointService: TouristPointService,
              private imageSanitizer: ImageSanitizerService, private router: Router) { }

  ngOnInit(): void{
    this.retrieveComponentData();
    this.subscribeToChangesInRoute();
  }

  public retrieveComponentData(): void{
    this.loadParams();

    this.toruistPointService.allTouristPoints(this.touristPointSearchModel)
      .subscribe(touristPoints => this.loadTouristPoints(touristPoints), (error: HttpErrorResponse) => this.showError(error));
  }

  public chooseTouristPoint(touristPointId?: number): void{
    if (touristPointId === this.chosenTouristPointId){
      this.chosenTouristPointId = null;
    }else{
      this.chosenTouristPointId = touristPointId;
    }

    this.chosenTouristPointIdChange.emit(this.chosenTouristPointId);
  }

  public isTouristPointCardChosen(touristpointId: number): boolean{
    return this.chosenTouristPointId === touristpointId;
  }

  private subscribeToChangesInRoute(): void{
    this.router.events.subscribe(val => {
      if (val instanceof NavigationEnd){
        this.retrieveComponentData();
        this.checkIfChosenTouristPointIsStillAvailable();
      }
    });
  }

  private checkIfChosenTouristPointIsStillAvailable(): void{
    let touristPointIsStillAvailable = false;

    this.touristPoints.forEach(touristPoint => {
      if (touristPoint.id === this.chosenTouristPointId){
        touristPointIsStillAvailable = true;
      }
    });

    if (!touristPointIsStillAvailable){
      this.chooseTouristPoint(null);
    }
  }

  private loadParams(): void{
    let regionIdParam: number;
    let categoriesIdParam: string;

    this.activatedRoute.queryParamMap.subscribe(param => {
      if (param.has('regionId')){
        regionIdParam = +param.get('regionId');
      }else{
        this.router.navigate([''], {replaceUrl: true});
      }

      if (param.has('categoriesId')){
        categoriesIdParam = param.get('categoriesId');
      }else{
        categoriesIdParam = null;
      }
    });

    this.touristPointSearchModel = {
      regionId: regionIdParam,
      categoriesId: categoriesIdParam
    };
  }

  private loadTouristPoints(touristPoints: TouristPointBasicInfoModel[]): void{
    touristPoints.forEach(touristPoint => {
     this.imageSanitizer.sanitizeImageData(touristPoint.image);
    });
    this.touristPoints = touristPoints;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }
}
