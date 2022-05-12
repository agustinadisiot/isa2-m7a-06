/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TouristPointService } from './tourist-point.service';

describe('Service: TouristPoint', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TouristPointService]
    });
  });

  it('should ...', inject([TouristPointService], (service: TouristPointService) => {
    expect(service).toBeTruthy();
  }));
});
