import { ResortService } from 'src/app/core/http-services/resort/resort.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResortDetailsModel } from 'src/app/shared/models/out/resort-details-model';
import { HttpErrorResponse } from '@angular/common/http';
import { ResortRoutes } from 'src/app/core/routes';

@Component({
  selector: 'app-resort-list',
  templateUrl: './resort-list.component.html',
  styleUrls: []
})
export class ResortListComponent implements OnInit {
  public resorts: ResortDetailsModel[] = [];
  public displayError: boolean;

  constructor(private resortService: ResortService, private router: Router) { }

  ngOnInit(): void {
    this.retrieveComponentData();
  }

  private retrieveComponentData(): void{
    this.resortService.allResortsNotClientSearch({})
      .subscribe(resorts => this.loadResorts(resorts), (error: HttpErrorResponse) => this.showError(error));
  }

  private loadResorts(resorts: ResortDetailsModel[]): void{
    this.resorts = resorts;
  }

  public deleteResort(resortId: number): void {
    this.resortService.deleteOneResort(resortId)
      .subscribe((response) => this.retrieveComponentData(),
        (error: HttpErrorResponse) => this.showError(error));
  }

  public goToCreateResort(): void {
    this.router.navigate([ResortRoutes.RESORT_CREATE]);
  }

  public goToUpdateResort(): void {
    this.router.navigate([ResortRoutes.UPDATE_AVAILABILITY]);
  }

  public closeError(): void{
    this.displayError = false;
  }

  private showError(error: HttpErrorResponse): void {
    console.log(error);
  }

}
