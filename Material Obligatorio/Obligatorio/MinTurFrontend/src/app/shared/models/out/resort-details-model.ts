import { ImageBasicInfoModel } from './image-basic-info-model';
import { ReviewDetailsModel } from './review-details-model';

export interface ResortDetailsModel {
  id: number;
  touristPointId: number;
  name: string;
  stars: number;
  address: string;
  phoneNumber: string;
  description: string;
  pricePerNight: number;
  punctuation: number;
  available: boolean;
  reviews: ReviewDetailsModel[];
  images: ImageBasicInfoModel[];
}
