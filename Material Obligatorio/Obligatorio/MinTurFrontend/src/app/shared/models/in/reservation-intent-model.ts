export interface ReservationIntentModel {
  resortId: number;
  checkIn: Date;
  checkOut: Date;
  adultsAmount: number;
  kidsAmount: number;
  babiesAmount: number;
  retiredAmount: number;
  name: string;
  surname: string;
  email: string;
}
