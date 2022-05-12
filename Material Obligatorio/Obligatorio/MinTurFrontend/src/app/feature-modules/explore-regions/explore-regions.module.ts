import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WelcomeBannerComponent } from './welcome-banner/welcome-banner.component';
import { RegionListComponent } from './region-list/region-list.component';
import { ExploreRegionsComponent } from './explore-regions.component';
import { RouterModule } from '@angular/router';
import { RegionService } from 'src/app/core/http-services/region/region.service';

@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  declarations: [
    ExploreRegionsComponent,
    WelcomeBannerComponent,
    RegionListComponent
  ],
  exports: [
    ExploreRegionsComponent
  ],
  providers: [
    RegionService
  ]
})
export class ExploreRegionsModule { }
