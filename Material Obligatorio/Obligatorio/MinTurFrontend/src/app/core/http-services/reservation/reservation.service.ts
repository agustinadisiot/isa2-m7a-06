import { ReservationEndpoints } from './../endpoints';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReservationCheckStateModel } from 'src/app/shared/models/in/reservation-check-state-model';
import { format } from 'util';
import { ReservationIntentModel } from 'src/app/shared/models/in/reservation-intent-model';
import { ReservationConfirmationModel } from 'src/app/shared/models/out/reservation-confirmation-model';
import { ReservationDetailsModel } from 'src/app/shared/models/out/reservation-details-model';
import { ReservationStateIntentModel } from 'src/app/shared/models/in/reservation-state-intent-model';
import { ReservationInidividualReportModel } from 'src/app/shared/models/out/reservation-individual-report-model';
import { ReservationReportInputModel } from 'src/app/shared/models/in/reservation-report-input-model';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {
  constructor(private http: HttpClient) {}

  public allReservations(): Observable<ReservationDetailsModel[]>{
    return this.http.get<ReservationDetailsModel[]>(format(ReservationEndpoints.GET_RESERVATIONS));
  }

  public makeReservation(reservationIntent: ReservationIntentModel): Observable<ReservationConfirmationModel>{
    return this.http.post<ReservationConfirmationModel>(ReservationEndpoints.CREATE_RESERVATION, reservationIntent);
  }

  public getReservationState(reservationId: string): Observable<ReservationCheckStateModel>{
    return this.http.get<ReservationCheckStateModel>(format(ReservationEndpoints.GET_RESERVATION_STATE, reservationId));
  }

  public updateReservationState(reservationId: string,
                                reservationIntent: ReservationStateIntentModel): Observable<ReservationCheckStateModel>{
    return this.http.put<ReservationCheckStateModel>(
        format(ReservationEndpoints.UPDATE_RESERVATION_STATE, reservationId),
              reservationIntent
        );
  }

  public getReservationsReport(input: ReservationReportInputModel): Observable<ReservationInidividualReportModel[]>{
    let reservationReportUri: string = ReservationEndpoints.GENERATE_REPORT;

    reservationReportUri += '?initialDate=' + this.parseDateToJSON(input.initialDate);
    reservationReportUri += '&finalDate=' + this.parseDateToJSON(input.finalDate);
    reservationReportUri += '&touristPointId=' + input.touristPointId;

    return this.http.get<ReservationInidividualReportModel[]>(reservationReportUri);
  }

  private parseDateToJSON(date: Date): string{
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    return year + '-' + month + '-' + day;
  }
}
