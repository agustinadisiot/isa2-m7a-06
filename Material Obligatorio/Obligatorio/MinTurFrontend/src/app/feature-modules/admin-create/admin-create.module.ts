import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminCreateComponent } from './admin-create.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [AdminCreateComponent]
})
export class AdminCreateModule { }
