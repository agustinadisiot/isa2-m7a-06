import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [LoginComponent]
})
export class LoginModule { }
