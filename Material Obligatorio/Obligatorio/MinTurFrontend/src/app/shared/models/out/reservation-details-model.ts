import { GuestBasicInfoModel } from './guest-basic-info-model';

export interface ReservationDetailsModel {
  id: string;
  description: string;
  state: string;
  resortId: number;
  totalPrice: number;
  checkIn: Date;
  checkOut: Date;
  adultsAmount: number;
  name: string;
  surname: string;
  email: string;
  guests: GuestBasicInfoModel;
}
