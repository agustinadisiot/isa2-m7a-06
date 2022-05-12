import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResortSearchModel } from 'src/app/shared/models/in/resort-search-model';

@Component({
  selector: 'app-explore-resorts',
  templateUrl: './explore-resorts.component.html',
  styleUrls: []
})
export class ExploreResortsComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;
  public resortSearchModel: ResortSearchModel;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void{
    this.loadParams();
    this.populateExplanationParams();
  }

  private loadParams(): void{
    let touristPointIdParam: number;
    let checkInParam: Date;
    let checkOutParam: Date;
    let adultsAmountParam: number;
    let kidsAmountParam: number;
    let babiesAmountParam: number;
    let retiredAmountParam: number;
    const availableParam = true;

    touristPointIdParam = +this.getSpecificParamOrFail('touristPointId');
    checkInParam = new Date(this.getSpecificParamOrFail('checkIn'));
    checkOutParam = new Date(this.getSpecificParamOrFail('checkOut'));
    adultsAmountParam = +this.getSpecificParamOrFail('adultsAmount');
    kidsAmountParam = +this.getSpecificParamOrFail('kidsAmount');
    babiesAmountParam = +this.getSpecificParamOrFail('babiesAmount');
    retiredAmountParam = +this.getSpecificParamOrFail('retiredAmount');

    this.resortSearchModel = {
      touristPointId: touristPointIdParam,
      available: availableParam,
      acommodationDetails: {
        checkIn: checkInParam,
        checkOut: checkOutParam,
        adultsAmount: adultsAmountParam,
        kidsAmount: kidsAmountParam,
        babiesAmount: babiesAmountParam,
        retiredAmount: retiredAmountParam
      }
    };
  }

  private getSpecificParamOrFail(paramName: string): string{
    let retrievedParam: string;

    this.activatedRoute.queryParamMap.subscribe(param => {
      if (param.has(paramName)){
        retrievedParam = param.get(paramName);
      }else{
        retrievedParam = null;
        this.navigateToDefaultSite();
      }
    });

    return retrievedParam;
  }

  private navigateToDefaultSite(): void{
    this.router.navigate([''], {replaceUrl: true});
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Elige un Hospedaje';
    this.explanationDescription = 'Selecciona un hospedaje para poder ver mas detalles y realizar una reserva.';
  }

}
