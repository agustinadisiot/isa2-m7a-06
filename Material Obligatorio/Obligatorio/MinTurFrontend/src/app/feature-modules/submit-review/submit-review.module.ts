import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubmitReviewComponent } from './submit-review.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { ReviewService } from 'src/app/core/http-services/review/review.service';
import { SubmitReviewExplanationComponent } from './submit-review-explanation/submit-review-explanation.component';
import { SubmitReviewInputComponent } from './submit-review-input/submit-review-input.component';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    SubmitReviewComponent,
    SubmitReviewExplanationComponent,
    SubmitReviewInputComponent
  ],
  exports: [
    SubmitReviewComponent
  ],
  providers: [
    ReviewService
  ]
})
export class SubmitReviewModule { }
