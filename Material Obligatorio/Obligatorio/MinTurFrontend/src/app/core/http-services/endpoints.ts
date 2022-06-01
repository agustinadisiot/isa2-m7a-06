
export enum ReservationEndpoints {
  GET_RESERVATIONS = 'reservations',
  CREATE_RESERVATION = 'reservations',
  GET_RESERVATION_STATE = 'reservations/%s/state',
  UPDATE_RESERVATION_STATE = 'reservations/%s/state',
  GENERATE_REPORT = 'reservations/generateReport'
}

export enum CategoryEndpoints {
  GET_CATEGORIES = 'categories',
  GET_ONE_CATEGORY = 'categories/%s',
}

export enum RegionEndpoints {
  GET_REGIONS = 'regions',
  GET_ONE_REGION = 'regions/%s',
}

export enum ResortEndpoints {
  GET_RESORTS = 'resorts/?clientSearch=%s',
  GET_ONE_RESORT = 'resorts/%s',
  CREATE_RESORT = 'resorts',
  DELETE_ONE_RESORT = 'resorts/%s',
  UPDATE_RESORT_AVAILABILITY = 'resorts/%s',
}

export enum ReviewEndpoints {
  CREATE_REVIEW = 'reviews'
}

export enum TouristPointEndpoints {
  GET_TOURIST_POINTS = 'touristPoints',
  CREATE_TOURIST_POINTS = 'touristPoints'
}

export enum ChargingPointEndpoints {
  GET_CHARGING_POINTS = 'touristPoints',
}

export enum SessionEndpoints {
  LOGIN = 'login'
}

export enum AdminEndpoints {
  GET_ADMINISTRATORS = 'administrators',
  GET_ONE_ADMINISTRATOR = 'administrators/%s',
  DELETE_ONE_ADMINISTRATOR = 'administrators/%s',
  CREATE_ADMINISTRATOR = 'administrators',
  UPDATE_ONE_ADMINISTRATOR = 'administrators/%s',
}

export enum ImporterEndpoints{
  GET_IMPORTERS = 'importers',
  IMPORT_RESOURCES = 'importers/importResources'
}
