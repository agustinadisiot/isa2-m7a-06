/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PageExplanationComponent } from './page-explanation.component';

describe('PageExplanationComponent', () => {
  let component: PageExplanationComponent;
  let fixture: ComponentFixture<PageExplanationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageExplanationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageExplanationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
