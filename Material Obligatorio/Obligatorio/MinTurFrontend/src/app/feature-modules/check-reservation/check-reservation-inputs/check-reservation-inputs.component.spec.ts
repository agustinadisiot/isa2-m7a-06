import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckReservationInputsComponent } from './check-reservation-inputs.component';

describe('CheckReservationInputsComponent', () => {
  let component: CheckReservationInputsComponent;
  let fixture: ComponentFixture<CheckReservationInputsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckReservationInputsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckReservationInputsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
