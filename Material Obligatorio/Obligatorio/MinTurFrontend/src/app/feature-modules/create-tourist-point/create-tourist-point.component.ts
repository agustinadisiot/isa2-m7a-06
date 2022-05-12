import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/core/http-services/category/category.service';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { TouristPointService } from 'src/app/core/http-services/tourist-point/tourist-point.service';
import { CategoryBasicInfoModel } from 'src/app/shared/models/out/category-basic-info-model';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';
import { TouristPointIntentModel } from 'src/app/shared/models/out/tourist-point-intent-model';

@Component({
  selector: 'app-create-tourist-point',
  templateUrl: './create-tourist-point.component.html',
  styleUrls: []
})
export class CreateTouristPointComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public justCreatedTouristPoint = false;
  public name: string;
  public description: string;
  public image: string;
  public regionId: number;
  public categoriesIds: number[] = [];
  public displayError: boolean;
  public errorMessages: string[] = [];
  public imageName: string;
  public categories: CategoryBasicInfoModel[] = [];
  public regions: RegionBasicInfoModel[] = [];
  private touristPointIntentModel: TouristPointIntentModel;

  constructor(private touristPointService: TouristPointService, private categoryService: CategoryService,
              private regionService: RegionService) { }

  ngOnInit(): void {
    this.getCategories();
    this.getRegions();
    this.populateExplanationParams();
  }

  private getCategories(): void {
    this.categoryService.allCategories().subscribe(categories => {
        this.loadCategories(categories);
      },
      error => this.showError(error)
    );
  }

  private getRegions(): void {
    this.regionService.allRegions().subscribe(regions => {
        this.loadRegions(regions);
      },
      error => this.showError(error)
    );
  }

  private loadCategories(categories: CategoryBasicInfoModel[]): void {
    this.categories = categories;
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

  public selectCategory(categoryId: number): void {
    const indexOfCategory = this.categoriesIds.indexOf(categoryId);
    if (indexOfCategory === -1){
      this.categoriesIds.push(categoryId);
    }else{
      this.categoriesIds.splice(indexOfCategory, 1);
    }
  }

  public selectRegion(regionId: number): void {
    this.regionId = regionId;
  }

  public loadFile(file: File): void {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onloadend = (): void => {
      this.image = typeof(reader.result) === 'string' ? reader.result.replace(/^data:.+;base64,/, '') : undefined;
    };
    this.imageName =  file?.name;
  }

  public createTouristPoint(): void{
    this.validateInputs();

    if (!this.displayError){
      this.touristPointIntentModel = {
        name: this.name,
        description: this.description,
        image: this.image,
        regionId: this.regionId,
        categoriesId: this.categoriesIds
      };
      this.touristPointService.createTourisPoint(this.touristPointIntentModel).subscribe(
        touristPointBasicInfoModel => {
          this.justCreatedTouristPoint = true;
        },
        error => this.showError(error)
      );
    }else{
      this.justCreatedTouristPoint = false;
    }
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];
    this.validateName();
    this.validateDescription();
    this.validateImage();
    this.validateRegion();
    this.validateCategories();
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
    if (!this.image?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una imágen');
    }
  }

  private validateRegion(): void {
    if (!this.regionId){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una región');
    }
  }

  private validateCategories(): void {
    if (this.categoriesIds.length === 0){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar al menos una categoría');
    }
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public closeError(): void{
    this.displayError = false;
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Crear un punto turístico';
    this.explanationDescription = 'Aquí puedes crear puntos turísticos!';
  }

}
