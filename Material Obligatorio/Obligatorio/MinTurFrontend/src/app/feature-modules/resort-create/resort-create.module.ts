import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResortCreateComponent } from './resort-create.component';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [ResortCreateComponent]
})
export class ResortCreateModule { }
