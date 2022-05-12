import { ResortDetailsModel } from './resort-details-model';

export interface FailedResortModel {
  resort: ResortDetailsModel;
  errorInCreation: string;
}
