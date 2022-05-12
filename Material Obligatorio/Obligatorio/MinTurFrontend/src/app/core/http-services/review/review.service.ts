import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReviewIntentModel } from 'src/app/shared/models/in/review-intent-model';
import { ReviewDetailsModel } from 'src/app/shared/models/out/review-details-model';
import { ReviewEndpoints } from '../endpoints';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  constructor(private http: HttpClient) { }

  public submitReview(reviewIntent: ReviewIntentModel): Observable<ReviewDetailsModel>{
    return this.http.post<ReviewDetailsModel>(ReviewEndpoints.CREATE_REVIEW, reviewIntent);
  }
}
