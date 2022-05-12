import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ReviewService } from 'src/app/core/http-services/review/review.service';
import { ResortRoutes } from 'src/app/core/routes';
import { ReviewIntentModel } from 'src/app/shared/models/in/review-intent-model';
import { ReviewDetailsModel } from 'src/app/shared/models/out/review-details-model';

@Component({
  selector: 'app-submit-review',
  templateUrl: './submit-review.component.html',
  styleUrls: []
})
export class SubmitReviewComponent implements OnInit {
  public reservationUniqueCode: string;
  public reviewText: string;
  public stars = 0;
  public displayError: boolean;
  public errorMessage: string;
  public justSubmitedReview = false;
  public submitedReview: ReviewDetailsModel;
  private reviewIntent: ReviewIntentModel;

  constructor(private reviewService: ReviewService, private router: Router) { }

  ngOnInit(): void{
  }

  public setReservationUniqueCode(reservationUniqueCode: string): void{
    this.reservationUniqueCode = reservationUniqueCode;
  }

  public setReviewText(reviewText: string): void{
    this.reviewText = reviewText;
  }

  public setStars(stars: number): void{
    this.stars = stars;
  }

  public closeError(): void{
    this.displayError = false;
  }

  public submitReview(): void{
    this.validateInputs();

    if (!this.displayError){
      this.constructReviewIntent();
      this.reviewService.submitReview(this.reviewIntent).subscribe(reviewDetails => this.loadSubmitedReview(reviewDetails),
        (error: HttpErrorResponse) => this.showError(error));
    }
  }

  public visitResortsPage(): void{
    const resortId = this.submitedReview.resortId;

    this.router.navigate([ResortRoutes.RESORTS, resortId]);
  }

  private constructReviewIntent(): void{
    this.reviewIntent = {
      reservationId: this.reservationUniqueCode,
      text: this.reviewText,
      stars: this.stars
    };
  }

  private validateInputs(): void{
    this.displayError = false;

    this.validateReservationUniqueCode();
    this.validateReviewText();
    this.validateStars();
  }

  private validateReservationUniqueCode(): void{
    const regex = new RegExp('^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$');

    if (this.reservationUniqueCode == null || !regex.test(this.reservationUniqueCode)){
      this.displayError = true;
      this.errorMessage = 'El formato del codigo unico de la reserva es invalido';
    }
  }

  private validateReviewText(): void{
    if (this.reviewText == null){
      this.displayError = true;
      this.errorMessage = 'Debe proveer una reseña sobre el hospedaje';
    }
  }

  private validateStars(): void{
    if (this.stars === 0){
      this.displayError = true;
      this.errorMessage = 'Debe calificar al hospedaje con una puntuacion de 1 a 5';
    }
  }

  private loadSubmitedReview(reviewDetails: ReviewDetailsModel): void{
    this.justSubmitedReview = true;
    this.submitedReview = reviewDetails;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }
}
