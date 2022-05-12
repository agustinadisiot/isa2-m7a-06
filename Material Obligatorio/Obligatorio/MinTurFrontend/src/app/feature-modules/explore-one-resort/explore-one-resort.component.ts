import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageSanitizerService } from 'src/app/core/http-services/image-sanitizer/image-sanitizer.service';
import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { AccommodationIntentModel } from 'src/app/shared/models/in/accommodation-intent-model';
import { ResortDetailsModel } from 'src/app/shared/models/out/resort-details-model';
import { ReviewDetailsModel } from 'src/app/shared/models/out/review-details-model';

@Component({
  selector: 'app-explore-one-resort',
  templateUrl: './explore-one-resort.component.html',
  styleUrls: []
})
export class ExploreOneResortComponent implements OnInit {
  public resort: ResortDetailsModel;
  public reviews: ReviewDetailsModel[] = [];
  public accommodationDetails: AccommodationIntentModel;
  public totalPrice: number;
  public resortId: number;
  public accommodationDetailsProvided = true;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private resortService: ResortService,
              private imageSanitizer: ImageSanitizerService) { }

  ngOnInit(): void{
    this.loadParams();
    this.retrieveComponentData();
  }

  private retrieveComponentData(): void{
    this.resortService.oneResort(this.resortId).subscribe(resort => this.loadResort(resort),
     (error: HttpErrorResponse) => this.showError(error));
  }

  private loadResort(resort: ResortDetailsModel): void{
    this.imageSanitizer.sanitizeImagesData(resort.images);
    this.resort = resort;
    this.reviews = resort.reviews;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  private loadParams(): void{
    let checkInParam: Date;
    let checkOutParam: Date;
    let adultsAmountParam: number;
    let kidsAmountParam: number;
    let babiesAmountParam: number;
    let retiredAmountParam: number;

    checkInParam = new Date(this.getSpecificQueryParamOrFail('checkIn'));
    checkOutParam = new Date(this.getSpecificQueryParamOrFail('checkOut'));
    adultsAmountParam = +this.getSpecificQueryParamOrFail('adultsAmount');
    kidsAmountParam = +this.getSpecificQueryParamOrFail('kidsAmount');
    babiesAmountParam = +this.getSpecificQueryParamOrFail('babiesAmount');
    retiredAmountParam = +this.getSpecificQueryParamOrFail('retiredAmount');
    this.totalPrice = +this.getSpecificQueryParamOrFail('totalPrice');
    this.resortId = +this.getSpecificRouteParamOrFail('resortId');

    this.accommodationDetails = {
      checkIn: checkInParam,
      checkOut: checkOutParam,
      adultsAmount: adultsAmountParam,
      kidsAmount: kidsAmountParam,
      babiesAmount: babiesAmountParam,
      retiredAmount: retiredAmountParam
    };
  }

  private getSpecificQueryParamOrFail(paramName: string): string{
    let retrievedParam: string;

    this.activatedRoute.queryParamMap.subscribe(param => {
      if (param.has(paramName)){
        retrievedParam = param.get(paramName);
      }else{
        retrievedParam = null;
        this.disableReservation();
      }
    });

    return retrievedParam;
  }

  private getSpecificRouteParamOrFail(paramName: string): string{
    let retrievedParam: string;

    this.activatedRoute.paramMap.subscribe(param => {
      if (param.has(paramName)){
        retrievedParam = param.get(paramName);
      }else{
        retrievedParam = null;
        this.navigateTo404();
      }
    });

    return retrievedParam;
  }

  private disableReservation(): void{
    this.accommodationDetailsProvided = false;
  }

  private navigateTo404(): void{
    this.router.navigate(['/404'], {replaceUrl: true});
  }

}
