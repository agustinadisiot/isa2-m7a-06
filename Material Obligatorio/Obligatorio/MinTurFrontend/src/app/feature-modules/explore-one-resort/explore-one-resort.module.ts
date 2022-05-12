import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExploreOneResortComponent } from './explore-one-resort.component';
import { ExploreOneResortDetailsComponent } from './explore-one-resort-details/explore-one-resort-details.component';
import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { MakeReservationComponent } from './make-reservation/make-reservation.component';
import { ReviewListComponent } from './review-list/review-list.component';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ExploreOneResortDetailsComponent,
    MakeReservationComponent,
    ReviewListComponent,
    ExploreOneResortComponent
  ],
  exports: [
    ExploreOneResortComponent
  ],
  providers: [
    ResortService,
    ReservationService
  ]
})
export class ExploreOneResortModule { }
