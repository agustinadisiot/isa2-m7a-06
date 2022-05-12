import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ReservationStateIntentModel } from 'src/app/shared/models/in/reservation-state-intent-model';
import { ReservationDetailsModel } from 'src/app/shared/models/out/reservation-details-model';
import { PossibleReservationStates } from 'src/app/shared/enums/possible-reservation-states';

@Component({
  selector: 'app-update-reservation-state',
  templateUrl: './update-reservation-state.component.html',
  styleUrls: []
})
export class UpdateReservationStateComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public reservationStates = PossibleReservationStates;
  public errorMessages: string[] = [];
  public displayError = false;
  public justUpdatedReservationState = false;
  public state: string;
  public reservations: ReservationDetailsModel[];
  private reservationId: string;
  private description: string;
  private reservationState: string;
  private reservationStateIntentModel: ReservationStateIntentModel;


  constructor(private reservationService: ReservationService) { }

  ngOnInit(): void {
    this.getReservations();
  }

  private getReservations(): void {
    this.reservationService.allReservations()
      .subscribe(reservationsDetailsModels => this.loadReservations(reservationsDetailsModels),
                  error => this.showError(error)
    );
    this.populateExplanationParams();
  }

  private loadReservations(reservationsDetailsModels: ReservationDetailsModel[]): void {
    this.reservations = reservationsDetailsModels;
  }

  public updateReservationState(): void{
    this.validateInputs();
    if (!this.displayError){
      this.reservationStateIntentModel = {
        state: this.reservationState,
        description: this.description,
      };
      this.reservationService.updateReservationState(this.reservationId, this.reservationStateIntentModel)
        .subscribe(reservationDetailsModel => {
          this.justUpdatedReservationState = true;
        },
        (error: HttpErrorResponse) => this.showError(error));
    }else{
      this.justUpdatedReservationState = false;
    }
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];

    this.validateReservationId();
    this.validateDescription();
    this.validateReservationState();
  }

  private validateReservationId(): void{
    if (!this.reservationId){
      this.displayError = true;
      this.errorMessages.push('Debes seleccionar una reserva');
    }
  }

  private validateDescription(): void{
    if (!this.description?.trim()){
      this.displayError = true;
      this.errorMessages.push('Debes agregar una descripción');
    }
  }

  private validateReservationState(): void{
    if (!this.reservationState){
      this.displayError = true;
      this.errorMessages.push('Debes seleccionar una nuevo estado');
    }
  }


  public selectReservation(reservationId: string): void {
    this.reservationId = reservationId;
  }

  public setDescription(description: string): void{
    this.description = description;
  }

  public selectState(newState: string): void {
    this.reservationState = newState;
  }

  public closeError(): void{
    this.displayError = false;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Actualiza estado de una reserva';
    this.explanationDescription = 'Selecciona una reserva y actualiza su estado';
  }

}
