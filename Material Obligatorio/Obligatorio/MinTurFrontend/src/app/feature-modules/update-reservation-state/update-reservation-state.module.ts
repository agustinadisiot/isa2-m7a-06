import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateReservationStateComponent } from './update-reservation-state.component';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [UpdateReservationStateComponent]
})
export class UpdateReservationStateModule { }
