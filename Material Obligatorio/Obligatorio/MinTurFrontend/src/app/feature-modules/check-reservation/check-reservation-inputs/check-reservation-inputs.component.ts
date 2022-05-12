import { ReservationCheckStateModel } from 'src/app/shared/models/in/reservation-check-state-model';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';

@Component({
  selector: 'app-check-reservation-inputs',
  templateUrl: './check-reservation-inputs.component.html',
  styleUrls: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CheckReservationInputsComponent implements OnInit {
  public justCheckedReservation = false;
  public reservationUniqueCode: string;
  public displayError: boolean;
  public errorMessage: string;
  public reservation: ReservationCheckStateModel;

  constructor(private reservationService: ReservationService) { }

  ngOnInit(): void {
  }

  public closeError(): void{
    this.displayError = false;
  }

  public setReservationUniqueCode(reservationUniqueCode: string): void{
    this.reservationUniqueCode = reservationUniqueCode;
  }

  public checkReservation(): void{
    this.validateInputs();

    if (!this.displayError){
      this.reservationService.getReservationState(this.reservationUniqueCode)
        .subscribe(reservationBasicInfo => this.loadReservationState(reservationBasicInfo),
          (error: HttpErrorResponse) => this.showError(error));
    }
  }

  private loadReservationState(reservationResponse: ReservationCheckStateModel): void{
    this.justCheckedReservation = true;
    this.reservation = reservationResponse;
  }

  private validateInputs(): void{
    this.displayError = false;

    this.validateReservationUniqueCode();
  }

  private validateReservationUniqueCode(): void{
    const regex = new RegExp('^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$');

    if (this.reservationUniqueCode == null || !regex.test(this.reservationUniqueCode)){
      this.displayError = true;
      this.errorMessage = 'El formato del codigo unico de la reserva es invalido';
    }
  }

  private showError(error: HttpErrorResponse): void {
    console.log(error);
  }

}
