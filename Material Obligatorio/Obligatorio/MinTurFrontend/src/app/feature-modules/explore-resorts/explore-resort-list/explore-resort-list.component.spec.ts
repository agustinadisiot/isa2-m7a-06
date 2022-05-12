/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ExploreResortListComponent } from './explore-resort-list.component';

describe('ExploreResortListComponent', () => {
  let component: ExploreResortListComponent;
  let fixture: ComponentFixture<ExploreResortListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExploreResortListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExploreResortListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
