import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExploreChargingPointsComponent } from './explore-charging-points.component';
import { ChargingPointListComponent} from "./charging-point-list/charging-point-list.component";
import { RegionService } from 'src/app/core/http-services/region/region.service';
import { ChargingPointService } from 'src/app/core/http-services/charging-point/charging-point.service';
import { UtilitiesModule } from 'src/app/shared/utilities/utilities.module';

@NgModule({
  imports: [
    CommonModule,
    UtilitiesModule
  ],
  declarations: [
    ExploreChargingPointsComponent,
    ChargingPointListComponent,
  ],
  exports: [
    ExploreChargingPointsComponent
  ],
  providers: [
    RegionService,
    ChargingPointService,
  ]
})
export class ExploreChargingPointsModule { }
