import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TouristPointService } from 'src/app/core/http-services/tourist-point/tourist-point.service';
import { ReservationReportInputModel } from 'src/app/shared/models/in/reservation-report-input-model';
import { TouristPointBasicInfoModel } from 'src/app/shared/models/out/tourist-point-basic-info-model';

@Component({
  selector: 'app-report-input',
  templateUrl: './report-input.component.html',
  styleUrls: []
})
export class ReportInputComponent implements OnInit {
  public touristPoints: TouristPointBasicInfoModel[] = [];
  public reservationReportInput: ReservationReportInputModel;
  private initialDate: Date;
  private finalDate: Date;
  private touristPointId: number;
  public displayError: boolean;
  public errorMessage: string;
  @Output() reportInputChosen = new EventEmitter<ReservationReportInputModel>();

  constructor(private touristPointService: TouristPointService) { }

  ngOnInit(): void{
    this.retrieveResources();
  }

  private retrieveResources(): void{
    this.touristPointService.allTouristPoints().subscribe(touristPoints => this.loadTouristPoints(touristPoints),
      (error: HttpErrorResponse) => this.showError(error));
  }

  private loadTouristPoints(touristPoints: TouristPointBasicInfoModel[]): void{
    this.touristPoints = touristPoints;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public chooseInitialDate(initialDate: Date): void{
    this.initialDate = initialDate;
  }

  public chooseFinalDate(finalDate: Date): void{
    this.finalDate = finalDate;
  }

  public selectTouristPoint(touristPointId: number): void{
    this.touristPointId = touristPointId;
  }

  public generateReport(): void{
    this.displayError = false;
    this.validateParams();

    if (!this.displayError){
      this.reservationReportInput = {
        finalDate: this.finalDate,
        initialDate: this.initialDate,
        touristPointId: this.touristPointId
      };

      this.reportInputChosen.emit(this.reservationReportInput);
    }
  }

  public closeError(): void{
    this.displayError = false;
  }

  private validateParams(): void{
    this.validateDates();
    this.validateTouristPoint();
  }

  private validateDates(): void{
    if (this.initialDate == null || this.finalDate == null){
      this.displayError = true;
      this.errorMessage = 'Debe eligir las fechas de inicio y fin para generar el reporte';
    }
    if (this.finalDate <= this.initialDate){
      this.displayError = true;
      this.errorMessage = 'La fecha final debe ser obligatoriamente mayor a la de inicial';
    }
  }

  private validateTouristPoint(): void{
    const selectedTouristPoint = this.touristPoints.find(t => t.id === this.touristPointId);

    if (selectedTouristPoint == null){
      this.displayError = true;
      this.errorMessage = 'Debes elegir un punto turistico';
    }
  }
}
