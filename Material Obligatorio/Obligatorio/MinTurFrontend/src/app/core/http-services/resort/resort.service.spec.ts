/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ResortService } from './resort.service';

describe('Service: Resort', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ResortService]
    });
  });

  it('should ...', inject([ResortService], (service: ResortService) => {
    expect(service).toBeTruthy();
  }));
});
