import { ChargingPointEndpoints } from '../endpoints';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChargingPointBasicInfoModel} from "../../../shared/models/out/charging-point-basic-info-model";
import { ChargingPointIntentModel } from 'src/app/shared/models/out/charging-point-intent-model';

//import { ChargingPointIntentModel } from 'src/app/shared/models/out/tourist-point-intent-model';

@Injectable({
  providedIn: 'root'
})
export class ChargingPointService {
  constructor(private http: HttpClient) { }

  public allChargingPoints(): Observable<ChargingPointBasicInfoModel[]> {
    let allChargingPointsUri = `${ChargingPointEndpoints.GET_CHARGING_POINTS}`;

    return this.http.get<ChargingPointBasicInfoModel[]>(allChargingPointsUri);
  }

  public createChargingsPoint(newChargingPoint: ChargingPointIntentModel): Observable<ChargingPointBasicInfoModel[]> {
     return this.http.post<ChargingPointBasicInfoModel[]>(ChargingPointEndpoints.GET_CHARGING_POINTS, newChargingPoint);
   }
}
