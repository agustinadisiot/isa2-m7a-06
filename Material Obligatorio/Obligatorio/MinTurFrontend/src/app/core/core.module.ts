import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavbarComponent,
    HttpClientModule
  ],
})
export class CoreModule { }
