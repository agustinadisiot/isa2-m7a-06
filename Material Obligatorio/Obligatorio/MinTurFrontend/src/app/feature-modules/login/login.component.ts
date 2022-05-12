import { AdministratorIntentModel } from './../../shared/models/out/administrator-intent-model';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SessionService } from 'src/app/core/http-services/session/session.service';
import { Router } from '@angular/router';
import { isEmailValid } from 'src/app/shared/helpers/validators';
import { RegionRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: []
})
export class LoginComponent implements OnInit {
  public email: string;
  public password: string;
  public displayError: boolean;
  public errorMessages: string[] = [];
  public administratorIntentModel: AdministratorIntentModel;

  constructor(private router: Router, private sessionService: SessionService) { }

  ngOnInit(): void {
  }

  public closeError(): void{
    this.displayError = false;
  }

  public login(): void {
    this.validateInputs();

    if (!this.displayError){
      this.administratorIntentModel = {
        email: this.email,
        password: this.password
      };
      this.sessionService.login(this.administratorIntentModel).subscribe(
        token => {
          const userInfo = {
            token,
            email: this.email
          };
          this.saveUserInfo(JSON.stringify(userInfo));
          this.navigateToDashboard();
        },
        error => this.showError(error)
      );
    }
  }

  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];

    this.validateEmail();
    this.validatePassword();

  }

  private validateEmail(): void {
    if (this.email == null || !isEmailValid(this.email)){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un email y que este sea válido');
    }
  }

  private validatePassword(): void {
    if (!this.password?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una password');
    }
  }

  public setEmail(email: string): void{
    this.email = email;
  }

  public setPassword(password: string): void{
    this.password = password;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  private navigateToDashboard(): void{
    this.router.navigate([RegionRoutes.REGIONS], {replaceUrl: true});
  }

  private saveUserInfo(userInfo: string): void{
    localStorage.setItem('userInfo', userInfo);
  }

}
