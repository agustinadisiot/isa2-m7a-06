export interface ResortIntentModel {
  touristPointId: number;
  name: string;
  stars: number;
  address: string;
  phoneNumber: string;
  reservationMessage: string;
  description: string;
  imagesData: string[];
  pricePerNight: number;
  available: boolean;
}
