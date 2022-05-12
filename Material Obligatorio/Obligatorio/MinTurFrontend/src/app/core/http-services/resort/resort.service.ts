import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResortSearchModel } from 'src/app/shared/models/in/resort-search-model';
import { ResortSearchResultModel } from 'src/app/shared/models/out/resort-search-result-model';
import { ResortDetailsModel } from 'src/app/shared/models/out/resort-details-model';
import { ResortEndpoints } from '../endpoints';
import { format } from 'util';
import { ResortPartialUpdateModel } from 'src/app/shared/models/in/resort-partial-update-model';
import { ResortIntentModel } from 'src/app/shared/models/in/resort-intent-model';

@Injectable({
  providedIn: 'root'
})
export class ResortService {
  constructor(private http: HttpClient) { }

  public allResortsClientSearch(searchModel: ResortSearchModel): Observable<ResortSearchResultModel[]>{
    let allResortsUri = format(ResortEndpoints.GET_RESORTS, true);

    if (searchModel.touristPointId != null){
      allResortsUri += '&touristPointId=' + searchModel.touristPointId;
    }
    if (searchModel.available != null){
      allResortsUri += '&available=' + searchModel.available;
    }
    const accommodationDetails = searchModel.acommodationDetails;

    allResortsUri += '&checkIn=' + this.parseDateToJSON(accommodationDetails.checkIn);
    allResortsUri += '&checkOut=' + this.parseDateToJSON(accommodationDetails.checkOut);
    allResortsUri += '&adultsAmount=' + accommodationDetails.adultsAmount;
    allResortsUri += '&kidsAmount=' + accommodationDetails.kidsAmount;
    allResortsUri += '&babiesAmount=' + accommodationDetails.babiesAmount;
    allResortsUri += '&retiredAmount=' + accommodationDetails.retiredAmount;

    return this.http.get<ResortSearchResultModel[]>(allResortsUri);
  }

  public allResortsNotClientSearch(searchModel: ResortSearchModel): Observable<ResortDetailsModel[]>{
    let allResortsUri = format(ResortEndpoints.GET_RESORTS, false);

    if (searchModel.touristPointId != null){
      allResortsUri += '&touristPointId=' + searchModel.touristPointId;
    }
    if (searchModel.available != null){
      allResortsUri += '&available=' + searchModel.available;
    }
    return this.http.get<ResortDetailsModel[]>(allResortsUri);
  }

  public oneResort(resortId: number): Observable<ResortDetailsModel>{
    return this.http.get<ResortDetailsModel>(format(ResortEndpoints.GET_ONE_RESORT, resortId));
  }

  public updateResortAvailability(resortId: number, resortUpdateModel: ResortPartialUpdateModel): Observable<ResortDetailsModel>{
    return this.http.patch<ResortDetailsModel>(format(ResortEndpoints.UPDATE_RESORT_AVAILABILITY, resortId), resortUpdateModel);
  }

  public createOneResort(resortIntenModel: ResortIntentModel): Observable<ResortDetailsModel>{
    return this.http.post<ResortDetailsModel>(ResortEndpoints.CREATE_RESORT, resortIntenModel);
  }

  public deleteOneResort(resortId: number): Observable<void>{
    return this.http.delete<void>(format(ResortEndpoints.DELETE_ONE_RESORT, resortId));
  }

  private parseDateToJSON(date: Date): string{
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    return year + '-' + month + '-' + day;
  }
}
