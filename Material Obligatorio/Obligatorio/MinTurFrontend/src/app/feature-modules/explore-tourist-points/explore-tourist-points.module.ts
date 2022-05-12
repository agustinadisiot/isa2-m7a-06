import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExploreTouristPointsComponent } from './explore-tourist-points.component';
import { TouristPointListComponent } from './tourist-point-list/tourist-point-list.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { CategoryService } from 'src/app/core/http-services/category/category.service';
import { TouristPointService } from 'src/app/core/http-services/tourist-point/tourist-point.service';
import { AccommodationDetailsComponent } from './accommodation-details/accommodation-details.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { ImageSanitizerService } from 'src/app/core/http-services/image-sanitizer/image-sanitizer.service';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ExploreTouristPointsComponent,
    AccommodationDetailsComponent,
    TouristPointListComponent,
    CategoryListComponent,
  ],
  exports: [
    ExploreTouristPointsComponent
  ],
  providers: [
    RegionService,
    CategoryService,
    TouristPointService,
    ImageSanitizerService
  ]
})
export class ExploreTouristPointsModule { }
