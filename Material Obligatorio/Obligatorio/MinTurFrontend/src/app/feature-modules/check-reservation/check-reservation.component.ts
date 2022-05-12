import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-check-reservation',
  templateUrl: './check-reservation.component.html',
  styleUrls: []
})
export class CheckReservationComponent implements OnInit {
  public explanationTitle: string;
  public explanationDescription: string;

  constructor() { }

  ngOnInit(): void{
    this.populateExplanationParams();
  }

  private populateExplanationParams(): void{
    this.explanationTitle = 'Ver Reserva';
    this.explanationDescription = 'Ingresa el código único de tu reserva para verificar el estado de esta.';
  }

}
