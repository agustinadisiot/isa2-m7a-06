import { RegionBasicInfoModel } from './region-basic-info-model';

export interface ChargingPointBasicInfoModel {
  id: number;
  name: string;
  description: string;
  address: string;
  region: RegionBasicInfoModel;

}
