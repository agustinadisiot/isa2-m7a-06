import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateTouristPointComponent } from './create-tourist-point.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [CreateTouristPointComponent]
})
export class CreateTouristPointModule { }
