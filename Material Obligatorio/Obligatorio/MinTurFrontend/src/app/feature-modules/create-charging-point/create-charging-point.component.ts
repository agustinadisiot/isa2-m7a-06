import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';
import { ChargingPointIntentModel } from 'src/app/shared/models/out/charging-point-intent-model';
import { ChargingPointService } from 'src/app/core/http-services/charging-point/charging-point.service';

@Component({
  selector: 'app-create-charging-point',
  templateUrl: './create-charging-point.component.html',
  styleUrls: []
})
export class CreateChargingPointComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public justCreatedChargingPoint = false;
  public name: string;
  public description: string;
  public address: string;
  public regionId: number;
  public displayError: boolean;
  public errorMessages: string[] = [];
  public regions: RegionBasicInfoModel[] = [];
  private chargingPointIntentModel: ChargingPointIntentModel;

  constructor(private chargingPointService: ChargingPointService,
              private regionService: RegionService) { }

  ngOnInit(): void {
    this.getRegions();
    this.populateExplanationParams();
  }

  private getRegions(): void {
    this.regionService.allRegions().subscribe(regions => {
        this.loadRegions(regions);
      },
      error => this.showError(error)
    );
  }

  private loadRegions(regions: RegionBasicInfoModel[]): void {
    this.regions = regions;
  }

  public setName(name: string): void{
    this.name = name;
  }

  public setDescription(description: string): void{
    this.description = description;
  }

  
  public setAddress(address: string): void{
    this.address = address;
  }

  public selectRegion(regionId: number): void {
    this.regionId = regionId;
  }

  public createChargingPoint(): void{
    this.validateInputs();

    if (!this.displayError){
      this.chargingPointIntentModel = {
        name: this.name,
        description: this.description,
        address: this.address,
        regionId: this.regionId,
      };
      this.chargingPointService.createChargingsPoint(this.chargingPointIntentModel).subscribe(
        chargingPointBasicInfoModel => {
          this.justCreatedChargingPoint = true;
        },
        error => this.showError(error)
      );
    }else{
      this.justCreatedChargingPoint = false;
    }
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];
    this.validateName();
    this.validateDescription();
    this.validateAddress();
    this.validateRegion();
  }

  private validateName(): void {
    const regex = new RegExp('@"^[a-zA-ZñÑáéíóúü0-9 ]+$"');
    if (regex.test(this.name)){
      this.displayError = true;
      this.errorMessages.push('El formato del nombre no es correcto');
    }
    if (!this.name?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un nombre');
    }
  }

  private validateDescription(): void {
    const regex = new RegExp('@"^[a-zA-ZñÑáéíóúü0-9 ]+$"');
    if (regex.test(this.description)){
      this.displayError = true;
      this.errorMessages.push('El formato de la descripción no es correcto');
    }
    if (!this.description?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una descripción');
    }
  }

  private validateAddress(): void {
    const regex = new RegExp('@"^[a-zA-ZñÑáéíóúü0-9 ]+$"');
    if (regex.test(this.address)){
      this.displayError = true;
      this.errorMessages.push('El formato de la dirección no es correcto');
    }
    if (!this.address?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una dirección');
    }
  }

  private validateRegion(): void {
    if (!this.regionId){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una región');
    }
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public closeError(): void{
    this.displayError = false;
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Crear un punto de carga';
    this.explanationDescription = 'Aquí puedes crear puntos de carga!';
  }

}
