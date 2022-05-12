import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';
import { RegionEndpoints } from '../endpoints';
import { format } from 'util';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  constructor(private http: HttpClient) {}

  public allRegions(): Observable<RegionBasicInfoModel[]>{
    return this.http.get<RegionBasicInfoModel[]>(RegionEndpoints.GET_REGIONS);
  }

  public oneRegion(regionId: number): Observable<RegionBasicInfoModel>{
    return this.http.get<RegionBasicInfoModel>(format(RegionEndpoints.GET_ONE_REGION, regionId));
  }

}
