import { CategoryBasicInfoModel } from './category-basic-info-model';
import { ImageBasicInfoModel } from './image-basic-info-model';
import { RegionBasicInfoModel } from './region-basic-info-model';

export interface TouristPointBasicInfoModel {
  id: number;
  name: string;
  description: string;
  image: ImageBasicInfoModel;
  region: RegionBasicInfoModel;
  categories: CategoryBasicInfoModel[];
}
