import { AccommodationIntentModel } from './accommodation-intent-model';

export interface ResortSearchModel {
  touristPointId?: number;
  available?: boolean;
  acommodationDetails?: AccommodationIntentModel;
}
