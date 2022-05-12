import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportingComponent } from './importing.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ImportingComponent
  ]
})
export class ImportingModule { }
