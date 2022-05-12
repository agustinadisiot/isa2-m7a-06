import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ImageSanitizerService } from 'src/app/core/http-services/image-sanitizer/image-sanitizer.service';
import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { ResortRoutes } from 'src/app/core/routes';
import { ResortSearchModel } from 'src/app/shared/models/in/resort-search-model';
import { ResortSearchResultModel } from 'src/app/shared/models/out/resort-search-result-model';

@Component({
  selector: 'app-explore-resort-list',
  templateUrl: './explore-resort-list.component.html',
  styleUrls: []
})
export class ExploreResortListComponent implements OnInit {
  public resorts: ResortSearchResultModel[] = [];
  @Input() resortSearchModel: ResortSearchModel;

  constructor(private resortService: ResortService, private imageSanitizer: ImageSanitizerService,
              private router: Router) { }

  ngOnInit(): void {
    this.retrieveComponentData();
  }

  public navigateToSelectedResort(resortId: number): void{
    this.router.navigate([ResortRoutes.RESORTS, resortId], {
      queryParams: {
        adultsAmount: this.resortSearchModel.acommodationDetails.adultsAmount,
        kidsAmount: this.resortSearchModel.acommodationDetails.kidsAmount,
        babiesAmount: this.resortSearchModel.acommodationDetails.babiesAmount,
        retiredAmount: this.resortSearchModel.acommodationDetails.retiredAmount,
        checkIn: this.resortSearchModel.acommodationDetails.checkIn,
        checkOut: this.resortSearchModel.acommodationDetails.checkOut,
        totalPrice: this.resorts.find(r => r.id === resortId).totalPrice
      }
    });
  }

  private retrieveComponentData(): void{
    this.resortService.allResortsClientSearch(this.resortSearchModel)
      .subscribe(resorts => this.loadResorts(resorts), (error: HttpErrorResponse) => this.showError(error));
  }

  private loadResorts(resorts: ResortSearchResultModel[]): void{
    resorts.forEach(resort => {
      this.imageSanitizer.sanitizeImagesData(resort.images);
    });

    this.resorts = resorts;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

}
