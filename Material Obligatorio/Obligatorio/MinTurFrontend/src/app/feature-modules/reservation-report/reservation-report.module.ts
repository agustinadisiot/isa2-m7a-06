import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationReportComponent } from './reservation-report.component';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { ReportInputComponent } from './report-input/report-input.component';
import { ReportListComponent } from './report-list/report-list.component';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ReservationReportComponent,
    ReportInputComponent,
    ReportListComponent
  ],
  exports: [
    ReservationReportComponent
  ],
  providers: [
    ReservationService
  ]
})
export class ReservationReportModule { }
