import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExploreResortsComponent } from './explore-resorts.component';
import { ImageSanitizerService } from 'src/app/core/http-services/image-sanitizer/image-sanitizer.service';
import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { ExploreResortListComponent } from './explore-resort-list/explore-resort-list.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ExploreResortListComponent,
    ExploreResortsComponent
  ],
  exports: [
    ExploreResortsComponent
  ],
  providers: [
    ImageSanitizerService,
    ResortService
  ]
})
export class ExploreResortsModule { }
