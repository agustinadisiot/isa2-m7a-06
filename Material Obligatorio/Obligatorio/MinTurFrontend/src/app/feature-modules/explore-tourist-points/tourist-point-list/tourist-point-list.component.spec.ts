/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TouristPointListComponent } from './tourist-point-list.component';

describe('TouristPointListComponent', () => {
  let component: TouristPointListComponent;
  let fixture: ComponentFixture<TouristPointListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TouristPointListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TouristPointListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
