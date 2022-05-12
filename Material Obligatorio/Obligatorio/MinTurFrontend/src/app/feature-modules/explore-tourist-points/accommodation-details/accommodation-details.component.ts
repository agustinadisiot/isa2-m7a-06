import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResortRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-accommodation-details',
  templateUrl: './accommodation-details.component.html',
  styleUrls: []
})
export class AccommodationDetailsComponent implements OnInit {
  public minDate = new Date();
  @Input() chosenTouristPointId: number = null;
  private checkIn: Date;
  private checkOut: Date;
  private adultsAmount = 0;
  private kidsAmount = 0;
  private babiesAmount = 0;
  private retiredAmount = 0;
  public accommodationValidationErrorMessage: string;
  public showAccommodationDetailsError = false;

  constructor(private router: Router) { }

  ngOnInit(): void{
  }

  public choseAccommodationCheckIn(date: Date): void{
    this.checkIn = date;
  }

  public choseAccommodationCheckOut(date: Date): void{
    this.checkOut = date;
  }

  public chooseAdultsAmount(amount: number): void{
    this.adultsAmount = amount;
  }

  public chooseKidsAmount(amount: number): void{
    this.kidsAmount = amount;
  }

  public chooseBabiesAmount(amount: number): void{
    this.babiesAmount = amount;
  }

  public chooseRetiredAmount(amount: number): void{
    this.retiredAmount = amount;
  }

  public searchResorts(): void{
    if (this.accommodationDetailsAreValid()){
      this.showAccommodationDetailsError = false;

      this.router.navigate([`/${ResortRoutes.RESORTS}`], {
        queryParams: {
          touristPointId: this.chosenTouristPointId,
          adultsAmount: this.adultsAmount,
          kidsAmount: this.kidsAmount,
          babiesAmount: this.babiesAmount,
          retiredAmount: this.retiredAmount,
          checkIn: this.checkIn,
          checkOut: this.checkOut
        },
        replaceUrl: true
      });
    }
    else{
      this.showAccommodationDetailsError = true;
    }
  }

  public closeAccommodationDetailsError(): void{
    this.showAccommodationDetailsError = false;
  }

  private accommodationDetailsAreValid(): boolean{
    let validAccommodation = true;

    if (this.chosenTouristPointId == null){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'Debe elegir un punto turistico';
    }
    if (this.adultsAmount === 0 && this.retiredAmount === 0){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'Debe haber al menos un adulto o jubilado';
    }
    if (this.adultsAmount < 0 || this.kidsAmount < 0 || this.babiesAmount < 0 || this.retiredAmount < 0){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'Todos los huespedes deben ser numeros positivos';
    }
    if (this.checkIn == null || this.checkOut == null){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'Debe eligir las fechas de su estadia';
    }
    if (this.checkIn < this.minDate){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'No se puede resevar para un dia anterior al dia de hoy';
    }
    if (this.checkOut <= this.checkIn){
      validAccommodation = false;
      this.accommodationValidationErrorMessage = 'La fecha de Check Out debe ser obligatoriamente mayor a la de Check In';
    }

    return validAccommodation;
  }

}
