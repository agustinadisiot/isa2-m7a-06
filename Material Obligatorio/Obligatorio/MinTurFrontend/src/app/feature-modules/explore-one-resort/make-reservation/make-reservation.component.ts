import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';
import { isEmailValid } from 'src/app/shared/helpers/validators';
import { AccommodationIntentModel } from 'src/app/shared/models/in/accommodation-intent-model';
import { ReservationIntentModel } from 'src/app/shared/models/in/reservation-intent-model';
import { ReservationConfirmationModel } from 'src/app/shared/models/out/reservation-confirmation-model';

@Component({
  selector: 'app-make-reservation',
  templateUrl: './make-reservation.component.html',
  styleUrls: []
})
export class MakeReservationComponent implements OnInit {
  @Input() accommodationDetails: AccommodationIntentModel;
  @Input() totalPrice: number;
  @Input() resortId: number;
  public errorMessages: string[] = [];
  public displayError = false;
  public justMadeReservation = false;
  public name: string;
  public surname: string;
  public email: string;
  public reservationConfirmation: ReservationConfirmationModel;
  private reservationIntent: ReservationIntentModel;

  constructor(private reservationService: ReservationService) { }

  ngOnInit(): void{
  }

  public setName(name: string): void{
    this.name = name;
  }

  public setSurname(surname: string): void{
    this.surname = surname;
  }

  public setEmail(email: string): void{
    this.email = email;
  }

  public makeReservation(): void{
    this.validateFields();
    if (!this.displayError){
      this.constructReservationIntent();

      this.reservationService.makeReservation(this.reservationIntent).subscribe(confirmation => this.loadConfirmation(confirmation),
        (error: HttpErrorResponse) => this.showError(error));
    }
  }

  public closeError(): void{
    this.displayError = false;
    this.errorMessages = [];
  }

  public parseDateIntoMDY(date: Date): string{
    const month = date.getMonth() + 1;
    const day = date.getDay();
    const year = date.getFullYear();

    return month + '/' + day + '/' + year;
  }

  private loadConfirmation(confirmation: ReservationConfirmationModel): void{
    this.reservationConfirmation = confirmation;
    this.justMadeReservation = true;
  }

  private constructReservationIntent(): void{
    this.reservationIntent = {
      resortId: this.resortId,
      name: this.name,
      surname: this.surname,
      email: this.email,
      checkIn: this.accommodationDetails.checkIn,
      checkOut: this.accommodationDetails.checkOut,
      adultsAmount: this.accommodationDetails.adultsAmount,
      kidsAmount: this.accommodationDetails.kidsAmount,
      babiesAmount: this.accommodationDetails.babiesAmount,
      retiredAmount: this.accommodationDetails.retiredAmount
    };
  }

  private validateFields(): void{
    this.closeError();
    this.validateName();
    this.validateSurname();
    this.validateEmail();
  }

  private validateName(): void{
    const regex = new RegExp('^[a-zA-ZñÑáéíóúü ]+$');

    if (this.name == null || !regex.test(this.name)){
      this.displayError = true;
      this.errorMessages.push('El nombre ingresado es invalido - solo caracteres alfabeticos');
    }
  }

  private validateSurname(): void{
    const regex = new RegExp('^[a-zA-ZñÑáéíóúü ]+$');

    if (this.surname == null || !regex.test(this.surname)){
      this.displayError = true;
      this.errorMessages.push('El apellido ingresado es invalido - solo caracteres alfabeticos');
    }
  }

  private validateEmail(): void{
    if (this.email == null || !isEmailValid(this.email)){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un email y que este sea válido');
    }
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }
}
