import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: []
})
export class StarRatingComponent implements OnInit {
  @Output() starsAmountChange = new EventEmitter<number>();
  @Input() availableInput = false;
  @Input() stars: number;
  public starsArray: number[] = [];
  public starsToChoose = new Array<boolean>(5);
  public amountOfChosenStars = 0;

  constructor() { }

  ngOnInit(): void{
    if (!this.availableInput){
      this.starsArray = Array(this.stars).fill(this.stars);
    } else{
      this.resetStarsToChoose();
    }
  }

  public selectAmountOfStars(amountOfStars: number): void{
    let starsToChooseIndex = amountOfStars - 1;

    this.resetStarsToChoose();
    while (starsToChooseIndex >= 0){
      this.starsToChoose[starsToChooseIndex] = true;
      starsToChooseIndex--;
    }

    this.amountOfChosenStars = amountOfStars;
    this.starsAmountChange.emit(this.amountOfChosenStars);
  }

  private resetStarsToChoose(): void{
    this.starsToChoose = new Array(5).fill(false);
  }
}
