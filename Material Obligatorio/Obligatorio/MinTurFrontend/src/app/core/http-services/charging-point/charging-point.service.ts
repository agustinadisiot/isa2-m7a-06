import { ChargingPointEndpoints } from '../endpoints';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChargingPointBasicInfoModel} from "../../../shared/models/out/charging-point-basic-info-model";
import { ChargingPointIntentModel } from 'src/app/shared/models/out/charging-point-intent-model';
import { format } from 'util';


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

  public deleteChargingPoint(chargingPointId: number): Observable<void>{
    return this.http.delete<void>(ChargingPointEndpoints.DELETE_ONE_CHARGING_POINT+"/"+chargingPointId);
  }
}
