import { ImageBasicInfoModel } from './image-basic-info-model';

export interface ResortSearchResultModel {
  id: number;
  touristPointId: number;
  name: string;
  stars: number;
  address: string;
  description: string;
  pricePerNight: number;
  totalPrice: number;
  punctuation: number;
  images: ImageBasicInfoModel[];
}
