import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: []
})
export class AppComponent implements OnInit {
  public title = 'Ministerio de Turismo';

  constructor(private titleService: Title) { }

  ngOnInit(): void {
    this.titleService.setTitle(this.title);
  }

}
