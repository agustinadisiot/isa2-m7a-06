import { TokenInterceptor } from './core/interceptors/token-interceptor';
import { ErrorHandlerInterceptor } from './core/interceptors/error-interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { APIInterceptor } from './core/interceptors/api-interceptor';
import { AuthGuard } from './core/guards/auth-guard';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    NoopAnimationsModule
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: APIInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
