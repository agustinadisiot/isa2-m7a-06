import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-number-input-spinner',
  templateUrl: './number-input-spinner.component.html',
  styleUrls: ['./number-input-spinner.component.css']
})
export class NumberInputSpinnerComponent implements OnInit {
  @Input() label = '';
  @Output() updatedValueEvent = new EventEmitter<number>();
  public spinnerValue = 0;

  constructor() { }

  ngOnInit(): void{
  }

  public incrementValue(): void{
    this.spinnerValue++;
    this.updatedValueEvent.emit(this.spinnerValue);
  }

  public decrementValue(): void{
    if (this.spinnerValue > 0){
      this.spinnerValue--;
      this.updatedValueEvent.emit(this.spinnerValue);
    }
  }
}
