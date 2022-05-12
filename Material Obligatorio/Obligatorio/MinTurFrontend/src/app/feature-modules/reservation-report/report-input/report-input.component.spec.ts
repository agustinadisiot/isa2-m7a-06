/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ReportInputComponent } from './report-input.component';

describe('ReportInputComponent', () => {
  let component: ReportInputComponent;
  let fixture: ComponentFixture<ReportInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
