import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/core/http-services/category/category.service';
import { CategoryBasicInfoModel } from 'src/app/shared/models/out/category-basic-info-model';

@Component({
  selector: 'app-tourist-points',
  templateUrl: './explore-tourist-points.component.html',
  styleUrls: []
})
export class ExploreTouristPointsComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public chosenTouristPointId: number;
  private chosenCategories: CategoryBasicInfoModel[] = [];
  private chosenCategoriesId: number[] = [];
  private categoriesIdParam: string;
  private currentRegion: RegionBasicInfoModel;

  constructor(private activatedRoute: ActivatedRoute, private regionService: RegionService, private router: Router,
              private categoryService: CategoryService) { }

  ngOnInit(): void{
    this.validateRegionIdParam();
    this.validateCategoriesIdParam();
    this.populateExplanationParams();
  }

  public updateFilteringCategoriesId(categoryId: number): void{
    const existingCategoryIdIndex = this.chosenCategoriesId.indexOf(categoryId);

    if (existingCategoryIdIndex !== -1){
      this.chosenCategoriesId.splice(existingCategoryIdIndex, 1);
    }else{
      this.chosenCategoriesId.push(categoryId);
    }

    this.generateCategoriesIdParam();
    this.updateCategoriesIdInRoute();
  }

  private generateCategoriesIdParam(): void{
    this.categoriesIdParam = '';

    for (const categoryId of this.chosenCategoriesId) {
      this.categoriesIdParam += categoryId;

      if (this.chosenCategoriesId.indexOf(categoryId) !== this.chosenCategoriesId.length - 1){
        this.categoriesIdParam += '-';
      }
    }
  }

  private updateCategoriesIdInRoute(): void{
    let categoriesIdParamForRoute: string = null;

    if (this.categoriesIdParam !== ''){
      categoriesIdParamForRoute = this.categoriesIdParam;
    }

    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: {
        categoriesId: categoriesIdParamForRoute
      },
      queryParamsHandling: 'merge',
      skipLocationChange: true
    });
  }

  public updateChosenTouristPointId(touristPointId?: number): void{
    this.chosenTouristPointId = touristPointId;
  }

  private validateRegionIdParam(): void{
    let regionId: number;

    this.activatedRoute.queryParamMap.subscribe(p => regionId = +p.get('regionId'));
    this.regionService.oneRegion(regionId).subscribe(region => this.loadCurrentRegion(region));
  }

  private loadCurrentRegion(region: RegionBasicInfoModel): void{
    this.currentRegion = region;
  }

  private validateCategoriesIdParam(): void{
    let categoriesId: string[];
    this.chosenCategories = [];

    this.activatedRoute.queryParamMap.subscribe(p => {
      if (p.has('categoriesId')){
      categoriesId = p.get('categoriesId').split('-');

      categoriesId.forEach(categoryId => {
        this.categoryService.oneCategory(+categoryId).subscribe(category => this.loadChosenCategories(category));
      });
      }
    });
  }

  private loadChosenCategories(category: CategoryBasicInfoModel): void{
    this.chosenCategories.push(category);
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Elige un Punto Turistico';
    this.explanationDescription = 'Selecciona uno de los siguientes puntos turisticos y los detalles sobre su estadia para ver los hospedajes disponibles.';
  }

}
