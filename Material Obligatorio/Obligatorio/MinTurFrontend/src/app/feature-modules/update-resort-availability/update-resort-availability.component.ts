import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { ResortDetailsModel } from 'src/app/shared/models/out/resort-details-model';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ResortPartialUpdateModel } from 'src/app/shared/models/in/resort-partial-update-model';
import { Router } from '@angular/router';
import { ResortRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-update-resort-availability',
  templateUrl: './update-resort-availability.component.html',
  styleUrls: []
})
export class UpdateResortAvailabilityComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public justChangedAvailability = false;
  public errorMessages: string[] = [];
  public displayError: boolean;
  public resorts: ResortDetailsModel[] = [];
  public resortId: number;
  public newAvailability: boolean;
  private resortUpdateModel: ResortPartialUpdateModel;

  constructor(private resortService: ResortService, private router: Router) { }

  ngOnInit(): void {
    this.retrieveComponentData();
    this.populateExplanationParams();
  }

  public updateResortAvailability(): void{
    this.validateInputs();

    if (!this.displayError){
      this.resortUpdateModel = {
        newAvailability: this.newAvailability
      };
      this.resortService.updateResortAvailability(this.resortId, this.resortUpdateModel)
        .subscribe(resortDetailModel => {
          this.justChangedAvailability = true;
        },
          (error: HttpErrorResponse) => this.showError(error)
        );
    }else{
      this.justChangedAvailability = false;
    }
  }

  public navigateToChangedResort(): void{
    this.router.navigate([ResortRoutes.RESORTS, this.resortId], { replaceUrl: true });
  }

  private retrieveComponentData(): void{
    this.resortService.allResortsNotClientSearch({})
      .subscribe(resorts => this.loadResorts(resorts), (error: HttpErrorResponse) => this.showError(error));
  }

  private loadResorts(resorts: ResortDetailsModel[]): void{
    this.resorts = resorts;
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];

    this.validateResortId();
    this.validateNewAvailability();
  }

  public selectResort(resortId: number): void{
    this.resortId = resortId;
  }

  public selectAvailability(newAvailability: boolean): void {
    this.newAvailability = newAvailability;
  }

  private validateResortId(): void{
    if (!this.resortId){
      this.displayError = true;
      this.errorMessages.push('Debes seleccionar un hospedaje');
    }
  }

  private validateNewAvailability(): void{
    if (this.newAvailability === undefined){
      this.displayError = true;
      this.errorMessages.push('Debes especificar una nueva disponibilidad');
    }
  }

  public closeError(): void{
    this.displayError = false;
  }

  private showError(error: HttpErrorResponse): void {
    console.log(error);
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Cambiar disponibilidad de un hospedaje';
    this.explanationDescription = 'Selecciona de la lista de hospedajes cual quieres cambiar y especifica su nueva disponibilidad';
  }

}
