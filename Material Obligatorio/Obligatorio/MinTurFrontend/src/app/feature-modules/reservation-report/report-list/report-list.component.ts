import { Component, Input, OnInit } from '@angular/core';
import { ReservationService } from 'src/app/core/http-services/reservation/reservation.service';
import { ReservationInidividualReportModel } from 'src/app/shared/models/out/reservation-individual-report-model';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: []
})
export class ReportListComponent implements OnInit {
  @Input() individualReports: ReservationInidividualReportModel[] = [];

  constructor() { }

  ngOnInit(): void{
  }

}
