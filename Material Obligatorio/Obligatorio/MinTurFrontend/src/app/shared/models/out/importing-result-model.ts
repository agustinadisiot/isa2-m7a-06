import { FailedResortModel } from './failed-resort-model';
import { ResortDetailsModel } from './resort-details-model';
import { TouristPointBasicInfoModel } from './tourist-point-basic-info-model';

export interface ImportingResultModel {
  succesfulImportedResorts: ResortDetailsModel[];
  succesfulImportedTouristPoints: TouristPointBasicInfoModel[];
  failedImportingResorts: FailedResortModel[];
}
