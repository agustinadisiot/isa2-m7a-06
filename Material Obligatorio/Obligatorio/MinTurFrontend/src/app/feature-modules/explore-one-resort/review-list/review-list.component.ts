import { Component, Input, OnInit } from '@angular/core';
import { ReviewDetailsModel } from 'src/app/shared/models/out/review-details-model';

@Component({
  selector: 'app-review-list',
  templateUrl: './review-list.component.html',
  styleUrls: []
})
export class ReviewListComponent implements OnInit {
  @Input() reviews: ReviewDetailsModel[] = [];

  constructor() { }

  ngOnInit(): void{
  }

}
