import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { RegionBasicInfoModel } from 'src/app/shared/models/out/region-basic-info-model';

@Component({
  selector: 'app-region-list',
  templateUrl: './region-list.component.html',
  styleUrls: []
})
export class RegionListComponent implements OnInit {
  public regions: RegionBasicInfoModel[] = [];

  constructor(private regionService: RegionService) { }

  ngOnInit(): void{
    this.retrieveComponentData();
  }

  public retrieveComponentData(): void{
    this.regionService.allRegions().subscribe(regions => this.loadRegions(regions), (error: HttpErrorResponse) => this.showError(error));
  }

  private loadRegions(regionsResponse: RegionBasicInfoModel[]): void{
    this.regions = regionsResponse;
  }

  private showError(error: HttpErrorResponse): void{
    console.log(error);
  }

}
