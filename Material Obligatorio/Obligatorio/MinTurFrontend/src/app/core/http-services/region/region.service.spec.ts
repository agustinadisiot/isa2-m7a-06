/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RegionService } from './region.service';

describe('Service: Regions', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegionService]
    });
  });

  it('should ...', inject([RegionService], (service: RegionService) => {
    expect(service).toBeTruthy();
  }));
});
