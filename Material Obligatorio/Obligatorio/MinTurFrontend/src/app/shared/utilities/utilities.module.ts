import { NgModule } from '@angular/core';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatCarouselModule } from '@ngmodule/material-carousel';
import { NumberInputSpinnerComponent } from './number-input-spinner/number-input-spinner.component';
import { StarRatingComponent } from './star-rating/star-rating.component';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { PageExplanationComponent } from './page-explanation/page-explanation.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    NumberInputSpinnerComponent,
    StarRatingComponent,
    PageExplanationComponent
  ],
  exports: [
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatCheckboxModule,
    MatInputModule,
    MatCarouselModule,
    NumberInputSpinnerComponent,
    StarRatingComponent,
    MatSelectModule,
    MatRadioModule,
    PageExplanationComponent
  ],
})
export class UtilitiesModule { }
