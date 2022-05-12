import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/core/http-services/admin/admin.service';
import { AdminSpecificRoutes } from 'src/app/core/routes';
import { isEmailValid } from 'src/app/shared/helpers/validators';
import { AdministratorIntentModel } from 'src/app/shared/models/out/administrator-intent-model';

@Component({
  selector: 'app-admin-create',
  templateUrl: './admin-create.component.html',
  styleUrls: []
})
export class AdminCreateComponent implements OnInit {
  public email: string;
  public password = '';
  public displayError: boolean;
  public errorMessages: string[] = [];
  private administratorIntentModel: AdministratorIntentModel;
  public explanationTitle: string;
  public explanationDescription: string;

  constructor(private adminService: AdminService, private router: Router) { }

  ngOnInit(): void {
    this.populateExplanationParams();
  }

  public setEmail(email: string): void{
    this.email = email;
  }

  public setPassword(password: string): void{
    this.password = password;
  }

  public createAdministrator(): void{
    this.validateInputs();

    if (!this.displayError){
      this.administratorIntentModel = {
        email: this.email,
        password: this.password
      };
      this.adminService.createOneAdministrator(this.administratorIntentModel).subscribe(
        administratorBasicInfoModel => {
          this.goToAdministratorDetail(administratorBasicInfoModel.id)
        },
        error => this.showError(error)
      );
    }
  }


  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];

    this.validatePassword();
    this.validateEmail();
  }

  private validatePassword(): void {
    if (!this.password?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una password');
    }
  }

  private validateEmail(): void {
    if (this.email == null || !isEmailValid(this.email)){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un email y que este sea válido');
    }
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public closeError(): void{
    this.displayError = false;
  }

  public navigateToListOfAdministrators(): void{
    this.router.navigate([AdminSpecificRoutes.ADMIN_LIST], {replaceUrl: true});
  }

  private goToAdministratorDetail(administratorId: number): void{
    this.router.navigate([AdminSpecificRoutes.ADMIN_DETAIL, administratorId], {replaceUrl: true});
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Crear un administrador';
    this.explanationDescription = 'Aquí puedes crear administradores';
  }

}
