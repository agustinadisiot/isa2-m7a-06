import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-explanation',
  templateUrl: './page-explanation.component.html',
  styleUrls: []
})
export class PageExplanationComponent implements OnInit {
  @Input() title: string;
  @Input() explanation: string;

  constructor() { }

  ngOnInit(): void{
  }

}
