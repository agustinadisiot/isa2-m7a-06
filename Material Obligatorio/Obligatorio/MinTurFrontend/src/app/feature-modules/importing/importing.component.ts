import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ImporterService } from 'src/app/core/http-services/importer/importer.service';
import { ImporterParameterIntentModel } from 'src/app/shared/models/in/importer-parameter-intent-model';
import { ImporterUsageModel } from 'src/app/shared/models/in/importer-usage-model';
import { ImporterDetailModel } from 'src/app/shared/models/out/importer-detail-model';
import { ImporterParameterModel } from 'src/app/shared/models/out/importer-parameter-model';
import { ImportingResultModel } from 'src/app/shared/models/out/importing-result-model';

@Component({
  selector: 'app-importing',
  templateUrl: './importing.component.html',
  styleUrls: []
})
export class ImportingComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public availableImporters: ImporterDetailModel[] = [];
  public chosenImporterParameters: ImporterParameterModel[] = [];
  public importingResult: ImportingResultModel;
  public importingResultAvailable = false;
  public importerIsChosen = false;
  private chosenImporterParameterIntents: ImporterParameterIntentModel[] = [];
  private importerUsageModel: ImporterUsageModel;
  private chosenImporterName: string;

  constructor(private importerService: ImporterService) { }

  ngOnInit(): void{
    this.retrieveResources();
    this.populateExplanationParams();
  }

  public selectImporter(importerName: string): void{
    this.chosenImporterName = importerName;
    this.chosenImporterParameterIntents = [];
    this.importerIsChosen = true;
    this.loadParametersForChosenImporter();
  }

  public showFileParameter(parameterType: string): boolean{
    return parameterType === 'File';
  }

  public showTextParameter(parameterType: string): boolean{
    return parameterType === 'Text';
  }

  public showNumberParameter(parameterType: string): boolean{
    return parameterType === 'Number';
  }

  public showFlagParameter(parameterType: string): boolean{
    return parameterType === 'Flag';
  }

  public addParameter(parameterName: string, parameterValue: string): void{
    const newParameter: ImporterParameterIntentModel = {
      name: parameterName,
      value: parameterValue
    };
    const newParameterList = this.chosenImporterParameterIntents.filter(p => p.name !== parameterName);

    newParameterList.push(newParameter);
    this.chosenImporterParameterIntents = newParameterList;
  }

  public useImporter(): void {
    this.importerUsageModel = {
      importerName: this.chosenImporterName,
      parameters: this.chosenImporterParameterIntents
    };
    this.importerService.importResources(this.importerUsageModel).subscribe(result => this.loadImportingResult(result),
      (error: HttpErrorResponse) => this.showError(error));
  }

  private loadImportingResult(importingResult: ImportingResultModel): void{
    this.importingResult = importingResult;
    this.importingResultAvailable = true;
  }

  private loadParametersForChosenImporter(): void{
    const chosenImporter = this.availableImporters.find(i => i.name === this.chosenImporterName);
    this.chosenImporterParameters = chosenImporter.parameters;
  }

  private retrieveResources(): void{
    this.importerService.allImporters().subscribe(importers => this.loadImporters(importers),
      (error: HttpErrorResponse) => this.showError(error));
  }

  private loadImporters(importers: ImporterDetailModel[]): void{
    this.availableImporters = importers;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Ver Reserva';
    this.explanationDescription = 'Ingresa el código único de tu reserva para verificar el estado de esta.';
  }
}
