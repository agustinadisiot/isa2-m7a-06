import { TouristPointEndpoints } from '../endpoints';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TouristPointSearchModel } from 'src/app/shared/models/in/tourist-point-search-model';
import { TouristPointBasicInfoModel } from 'src/app/shared/models/out/tourist-point-basic-info-model';
import { TouristPointIntentModel } from 'src/app/shared/models/out/tourist-point-intent-model';

@Injectable({
  providedIn: 'root'
})
export class TouristPointService {
  constructor(private http: HttpClient) { }

  public allTouristPoints(searchModel?: TouristPointSearchModel): Observable<TouristPointBasicInfoModel[]> {
    let allTouristPointsUri = `${TouristPointEndpoints.GET_TOURIST_POINTS}`;

    if (searchModel != null){
      if (searchModel.regionId != null && searchModel.categoriesId == null){
        allTouristPointsUri += '?regionId=' + searchModel.regionId;
      }
      else if (searchModel.categoriesId != null && searchModel.regionId == null){
        allTouristPointsUri += '?categoriesId=' + searchModel.categoriesId;
      }
      else if (searchModel.regionId != null && searchModel.categoriesId != null){
        allTouristPointsUri += '?regionId=' + searchModel.regionId + '&categoriesId=' + searchModel.categoriesId;
      }
    }
    return this.http.get<TouristPointBasicInfoModel[]>(allTouristPointsUri);
  }

  public createTourisPoint(newTouristPoint: TouristPointIntentModel): Observable<TouristPointBasicInfoModel[]> {
    return this.http.post<TouristPointBasicInfoModel[]>(TouristPointEndpoints.GET_TOURIST_POINTS, newTouristPoint);
  }
}
