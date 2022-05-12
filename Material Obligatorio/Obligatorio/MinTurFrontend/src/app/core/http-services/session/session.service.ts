import { Observable } from 'rxjs';
import { SessionEndpoints } from './../endpoints';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AdministratorIntentModel } from 'src/app/shared/models/out/administrator-intent-model';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private http: HttpClient) {}

  public login(administratorIntentModel: AdministratorIntentModel): Observable<any>{
    return this.http.post<any>(SessionEndpoints.LOGIN, administratorIntentModel);
  }

  public logout(): void{
    localStorage.removeItem('userInfo');
  }
}
