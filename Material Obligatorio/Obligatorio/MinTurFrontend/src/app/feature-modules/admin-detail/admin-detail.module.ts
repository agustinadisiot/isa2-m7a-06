import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminDetailComponent } from './admin-detail.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [AdminDetailComponent]
})
export class AdminDetailModule { }
