
export enum ReservationRoutes {
  CHECK_RESERVATION = 'checkReservation',
  REPORT = 'admin/reservation-report',
  UPDATE_STATE = 'admin/update-reservation-state'
}

export enum RegionRoutes {
  DEFAULT = 'explore',
  REGIONS = 'explore/regions'
}

export enum ResortRoutes {
  RESORTS = 'explore/resorts',
  RESORT_LIST = 'admin/resort',
  RESORT_CREATE = 'admin/resort-create',
  RESORT_DETAIL = 'explore/resorts/:resortId',
  UPDATE_AVAILABILITY = 'admin/update-resort-availability'
}

export enum ReviewRoutes {
  REVIEW = 'submitReview'
}

export enum TouristPointRoutes {
  TOURIST_POINTS = 'explore/tourist-points',
  TOURIST_POINT_CREATE = 'admin/tourist-point-create'
}

export enum ChargingPointRoutes {
  CHARGING_POINTS = 'explore/charging-points',
}

export enum SessionRoutes {
  LOGIN = 'login'
}

export enum AdminSpecificRoutes {
  ADMIN_LIST = 'admin/administrator',
  ADMIN_DETAIL = 'admin/administrator-detail',
  ADMIN_CREATE = 'admin/administrator-create'
}

export enum ImporterRoutes{
  IMPORT = 'admin/import'
}
