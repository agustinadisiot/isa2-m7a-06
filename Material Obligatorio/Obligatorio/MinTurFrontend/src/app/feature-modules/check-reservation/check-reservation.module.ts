import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckReservationComponent } from './check-reservation.component';
import { CheckReservationInputsComponent } from './check-reservation-inputs/check-reservation-inputs.component';



@NgModule({
  declarations: [CheckReservationComponent, CheckReservationInputsComponent],
  imports: [
    CommonModule,
    UtilitiesModule
  ]
})
export class CheckReservationModule { }
