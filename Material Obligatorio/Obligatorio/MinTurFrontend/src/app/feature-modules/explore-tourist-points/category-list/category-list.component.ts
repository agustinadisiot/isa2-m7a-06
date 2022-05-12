import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CategoryService } from 'src/app/core/http-services/category/category.service';
import { CategoryBasicInfoModel } from 'src/app/shared/models/out/category-basic-info-model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: []
})
export class CategoryListComponent implements OnInit {
  public categories: CategoryBasicInfoModel[] = [];
  @Output() categoryChosenEvent = new EventEmitter<number>();

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void{
    this.retrieveComponentData();
  }

  public retrieveComponentData(): void{
    this.categoryService.allCategories().subscribe(categories => this.loadCategories(categories), (error: string) => this.showError(error));
  }

  public categoryFiltering(categoryId: number): void{
    this.categoryChosenEvent.emit(categoryId);
  }

  private loadCategories(categoriesResponse: CategoryBasicInfoModel[]): void{
    this.categories = categoriesResponse;
  }

  private showError(message: string): void{
    console.log(message);
  }

}
