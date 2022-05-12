import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdateResortAvailabilityComponent } from './update-resort-availability.component';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [UpdateResortAvailabilityComponent]
})
export class UpdateResortAvailabilityModule { }
