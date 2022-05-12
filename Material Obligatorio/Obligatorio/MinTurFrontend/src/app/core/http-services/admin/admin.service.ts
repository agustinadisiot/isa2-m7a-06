import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { format } from 'util';
import { AdminEndpoints } from '../endpoints';
import { AdministratorBasicInfoModel } from 'src/app/shared/models/in/administrator-basic-info-model';
import { AdministratorIntentModel } from 'src/app/shared/models/out/administrator-intent-model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) {}

  public allAdministrators(): Observable<AdministratorBasicInfoModel[]>{
    return this.http.get<AdministratorBasicInfoModel[]>(AdminEndpoints.GET_ADMINISTRATORS);
  }

  public oneAdministrator(administratorId: number): Observable<AdministratorBasicInfoModel>{
    return this.http.get<AdministratorBasicInfoModel>(format(AdminEndpoints.GET_ONE_ADMINISTRATOR, administratorId));
  }

  public deleteOneAdministrator(administratorId: number): Observable<void>{
    return this.http.delete<void>(format(AdminEndpoints.DELETE_ONE_ADMINISTRATOR, administratorId));
  }

  public createOneAdministrator(newAdministrator: AdministratorIntentModel): Observable<AdministratorBasicInfoModel>{
    return this.http.post<AdministratorBasicInfoModel>(AdminEndpoints.CREATE_ADMINISTRATOR, newAdministrator);
  }

  public updateOneAdministrator(administratorId: number, updatedAdministrator: AdministratorIntentModel):
    Observable<AdministratorBasicInfoModel>{
    return this.http.put<AdministratorBasicInfoModel>(
          format(AdminEndpoints.UPDATE_ONE_ADMINISTRATOR, administratorId),
          updatedAdministrator
        );
  }
}
