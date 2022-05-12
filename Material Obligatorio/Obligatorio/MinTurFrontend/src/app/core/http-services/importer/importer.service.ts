import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ImporterUsageModel } from 'src/app/shared/models/in/importer-usage-model';
import { ImporterDetailModel } from 'src/app/shared/models/out/importer-detail-model';
import { ImportingResultModel } from 'src/app/shared/models/out/importing-result-model';
import { ImporterEndpoints } from '../endpoints';

@Injectable({
  providedIn: 'root'
})
export class ImporterService {

  constructor(private http: HttpClient) { }

  public allImporters(): Observable<ImporterDetailModel[]>{
    return this.http.get<ImporterDetailModel[]>(ImporterEndpoints.GET_IMPORTERS);
  }

  public importResources(importerUsage: ImporterUsageModel): Observable<ImportingResultModel>{
    return this.http.post<ImportingResultModel>(ImporterEndpoints.IMPORT_RESOURCES, importerUsage);
  }
}
