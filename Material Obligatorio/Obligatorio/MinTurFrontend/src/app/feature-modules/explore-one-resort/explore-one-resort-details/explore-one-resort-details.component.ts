import { Component, Input, OnInit } from '@angular/core';
import { ResortDetailsModel } from 'src/app/shared/models/out/resort-details-model';

@Component({
  selector: 'app-explore-one-resort-details',
  templateUrl: './explore-one-resort-details.component.html',
  styleUrls: []
})
export class ExploreOneResortDetailsComponent implements OnInit {
  @Input() resort: ResortDetailsModel;

  constructor() { }

  ngOnInit(): void{
  }

}
