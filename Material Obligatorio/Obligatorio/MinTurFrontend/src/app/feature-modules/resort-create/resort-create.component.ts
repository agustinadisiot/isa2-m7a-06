import { ResortIntentModel } from 'src/app/shared/models/in/resort-intent-model';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { TouristPointService } from 'src/app/core/http-services/tourist-point/tourist-point.service';
import { TouristPointBasicInfoModel } from 'src/app/shared/models/out/tourist-point-basic-info-model';
import { Router } from '@angular/router';
import { ResortRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-resort-create',
  templateUrl: './resort-create.component.html',
  styleUrls: []
})
export class ResortCreateComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public displayError: boolean;
  public errorMessages: string[] = [];
  public imageName: string;
  public touristPoints: TouristPointBasicInfoModel[] = [];
  public justCreatedResort = false;
  public resortIntentModel: ResortIntentModel;

  public images: string[] = [];
  public touristPointId: number;
  public name: string;
  public description: string;
  public address: string;
  public phoneNummber: string;
  public reservationMessage: string;
  public pricePerNight: number;
  public available: boolean;
  public stars: number;

  constructor(private resortService: ResortService, private touristPointService: TouristPointService, private router: Router) { }

  ngOnInit(): void {
    this.getTouristPoints();
    this.populateExplanationParams();
  }

  private getTouristPoints(): void {
    this.touristPointService.allTouristPoints({}).subscribe(touristPoints => {
        this.loadTouristPoints(touristPoints);
      },
      error => this.showError(error)
    );
  }

  private loadTouristPoints(touristPoints: TouristPointBasicInfoModel[]): void {
    this.touristPoints = touristPoints;
  }

  public loadFiles(files: File[]): void {
    this.images = [];
    for (const file of files) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onloadend = (): void => {
        const image = typeof(reader.result) === 'string' ? reader.result.replace(/^data:.+;base64,/, '') : undefined;
        if (image){
          this.images.push(image);
        }
      };
    }
    this.imageName =  'Imágen/es cargada';
  }

  public selectTouristPoint(touristPointId: number): void {
    this.touristPointId = touristPointId;
  }

  public setAvailable(value: boolean): void{
    this.available = value;
  }

  public setName(name: string): void{
    this.name = name;
  }

  public setAddress(address: string): void{
    this.address = address;
  }

  public setDescription(description: string): void{
    this.description = description;
  }

  public setPhoneNumber(phoneNummber: string): void{
    this.phoneNummber = phoneNummber;
  }

  public setReservationMessage(reservationMessage: string): void{
    this.reservationMessage = reservationMessage;
  }

  public setPricePerNight(pricePerNight: string): void{
    this.pricePerNight = parseInt(pricePerNight, 10);
  }

  public setAvailability(available: boolean): void{
    this.available = available;
  }

  public setStars(stars: number): void{
    this.stars = stars;
  }

  public createResort(): void{
    this.validateInputs();

    if (!this.displayError){
      this.resortIntentModel = {
        touristPointId: this.touristPointId,
        name: this.name,
        stars: this.stars,
        address: this.address,
        phoneNumber: this.phoneNummber,
        reservationMessage: this.reservationMessage,
        description: this.description,
        imagesData: this.images,
        pricePerNight: this.pricePerNight,
        available: this.available,
      };
      this.resortService.createOneResort(this.resortIntentModel).subscribe(
        resortBasicInfo => {
          this.justCreatedResort = true;
        },
        error => this.showError(error)
      );
    }else{
      this.justCreatedResort = false;
    }
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];
    this.validateName();
    this.validateDescription();
    this.validateAddress();
    this.validatePhoneNumber();
    this.validateReservationMessage();
    this.validatePricePerNight();
    this.validateAvailable();
    this.validateStars();
    this.validateTouristPoint();
    this.validateImage();
  }

  private validateName(): void {
    if (!this.name?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un nombre');
    }
  }

  private validateDescription(): void {
    if (!this.description?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una descripción');
    }
  }

  private validateImage(): void {
    if (this.images?.length === 0){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar al menos imágen');
    }
  }

  private validateTouristPoint(): void {
    if (!this.touristPointId){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un punto turístico');
    }
  }

  private validateStars(): void {
    if (!this.stars){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una puntuación');
    }
  }

  private validateAddress(): void {
    if (!this.address?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una dirección');
    }
  }

  private validatePhoneNumber(): void {
    if (isNaN(parseInt(this.phoneNummber?.trim(),10))){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un número de teléfono y que este sea correcto');
    }
  }

  private validateReservationMessage(): void {
    if (!this.reservationMessage?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un mensaje de reserva');
    }
  }

  private validatePricePerNight(): void {
    if (!this.pricePerNight || this.pricePerNight <= 0){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un precio por noche');
    }
  }

  private validateAvailable(): void {
    if (this.available === undefined){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar si está disponible');
    }
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public closeError(): void{
    this.displayError = false;
  }

  public navigateToListResorts(): void{
    this.router.navigate([ResortRoutes.RESORT_LIST], {replaceUrl: true});
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Crear un hospedaje';
    this.explanationDescription = 'Aquí puedes crear hospedajes!';
  }

}
