import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';
import { ReservationReportInputModel } from 'src/app/shared/models/in/reservation-report-input-model';
import { ReservationInidividualReportModel } from 'src/app/shared/models/out/reservation-individual-report-model';

@Component({
  selector: 'app-reservation-report',
  templateUrl: './reservation-report.component.html',
  styleUrls: []
})
export class ReservationReportComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public individualReports: ReservationInidividualReportModel[] = [];
  constructor(private reservationService: ReservationService) { }

  ngOnInit(): void{
    this.populateExplanationParams();
  }

  public generateReport(reservationReportInput: ReservationReportInputModel): void{
    this.individualReports = [];
    this.reservationService.getReservationsReport(reservationReportInput)
      .subscribe(individualReports => this.loadReports(individualReports), (error: HttpErrorResponse) => this.showError(error));
  }

  private loadReports(individualReports: ReservationInidividualReportModel[]): void{
    this.individualReports = individualReports;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Reporte (A)';
    this.explanationDescription = 'Elije un rango de fechas y un punto turistico para visualizar el '
      + 'reporte generado sobre la cantidad de reservas validas en cada hospedaje';
  }
}
