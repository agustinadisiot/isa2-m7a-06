import { CategoryEndpoints } from './../endpoints';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryBasicInfoModel } from 'src/app/shared/models/out/category-basic-info-model';
import { format } from 'util';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  public allCategories(): Observable<CategoryBasicInfoModel[]> {
    return this.http.get<CategoryBasicInfoModel[]>(CategoryEndpoints.GET_CATEGORIES);
  }

  public oneCategory(categoryId: number): Observable<CategoryBasicInfoModel> {
    return this.http.get<CategoryBasicInfoModel>((format(CategoryEndpoints.GET_ONE_CATEGORY, categoryId)));
  }
}
