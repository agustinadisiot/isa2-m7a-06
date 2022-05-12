import { AdministratorBasicInfoModel } from 'src/app/shared/models/in/administrator-basic-info-model';
import { Component, Input, OnInit } from '@angular/core';
import { AdminService } from 'src/app/core/http-services/admin/admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AdministratorIntentModel } from 'src/app/shared/models/out/administrator-intent-model';
import { isEmailValid } from 'src/app/shared/helpers/validators';
import { AdminSpecificRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-admin-detail',
  templateUrl: './admin-detail.component.html',
  styleUrls: []
})
export class AdminDetailComponent implements OnInit {
  public justUpdatedAdministrator = false;
  public explanationTitle: string;
  public explanationDescription: string;
  public administrator: AdministratorBasicInfoModel;
  private administratorId: number;
  private initialEmail: string;
  public emailNewValue: string;
  public passwordNewValue = '';
  public displayError: boolean;
  public errorMessages: string[] = [];
  public isUpdateEnable = false;
  private administratorIntentModel: AdministratorIntentModel;

  constructor(private adminService: AdminService, private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.loadParams();
    this.retrieveComponentData();
    this.populateExplanationParams();
  }

  private loadParams(): void{
    this.administratorId = +this.getSpecificRouteParam('administratorId');
  }

  private getSpecificRouteParam(paramName: string): string{
    let retrievedParam: string;

    this.activatedRoute.paramMap.subscribe(param => {
      if (param.has(paramName)){
        retrievedParam = param.get(paramName);
      }else{
        retrievedParam = null;
        this.navigateTo404();
      }
    });

    return retrievedParam;
  }

  private retrieveComponentData(): void{
    this.adminService.oneAdministrator(this.administratorId).subscribe(resort => this.loadAdministrator(resort),
     (error: HttpErrorResponse) => this.showError(error));
  }

  private loadAdministrator(administrator: AdministratorBasicInfoModel): void {
    this.administrator = administrator;
    this.initialEmail = administrator.email;
    this.emailNewValue = administrator.email;
  }

  public setEmail(email: string): void{
    this.emailNewValue = email;
    this.checkUpdateEnable();
  }

  public setPassword(password: string): void{
    this.passwordNewValue = password;
    this.checkUpdateEnable();
  }

  private checkUpdateEnable(): void{
    if (this.emailNewValue !== this.initialEmail || this.passwordNewValue !== ''){
      this.isUpdateEnable = true;
    }else{
      this.isUpdateEnable = false;
    }
  }

  public updateAdministrator(): void{
    this.validateInputs();

    if (!this.displayError){
      this.administratorIntentModel = {
        email: this.emailNewValue,
        password: this.passwordNewValue
      };
      this.adminService.updateOneAdministrator(this.administratorId ,this.administratorIntentModel).subscribe(
        administratorBasicInfoModel => {
          this.loadAdministrator(administratorBasicInfoModel);
          this.justUpdatedAdministrator = true;
        },
        error => this.showError(error)
      );
    }else{
      this.justUpdatedAdministrator = false;
    }
  }


  private validateInputs(): void{
    this.displayError = false;
    this.errorMessages = [];

    this.validateDifferentValues();
    this.validatePassword();
    this.validateEmail();
  }

  private validatePassword(): void {
    if (!this.passwordNewValue?.trim()){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar una nueva password');
    }
  }

  private validateEmail(): void {
    if (this.emailNewValue == null || !isEmailValid(this.emailNewValue)){
      this.displayError = true;
      this.errorMessages.push('Es necesario especificar un email y que este sea válido');
    }
  }


  private validateDifferentValues(): void {
    if (!this.isUpdateEnable){
      this.displayError = true;
      this.errorMessages.push('Debes colocar valores diferentes a los ya existentes');
    }
  }


  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

  public closeError(): void{
    this.displayError = false;
  }

  private navigateTo404(): void{
    this.router.navigate(['/404'], {replaceUrl: true});
  }

  public navigateToListOfAdministrators(): void{
    this.router.navigate([AdminSpecificRoutes.ADMIN_LIST], {replaceUrl: true});
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Detalle de administrador';
    this.explanationDescription = 'Aquí puedes ver el detalle de cada administrador, en caso de querer actualizar '
      + 'algún campo solamente modificarlo y dar en Actualizar!';
  }
}
