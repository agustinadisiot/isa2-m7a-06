import { SessionService } from 'src/app/core/http-services/session/session.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./styles.css']
})
export class NavbarComponent implements OnInit {

  constructor(private sessionService: SessionService, private router: Router) {
  }

  ngOnInit(): void{
  }

  public IsLoggedIn(): boolean{
    return localStorage.getItem('userInfo') != null;
  }

  public logout(): void{
    this.sessionService.logout();
    this.router.navigate(['/404'], {replaceUrl: true});
  }

  public getActiveAdminEmail(): string{
    return JSON.parse(localStorage.getItem('userInfo'))?.email ?? 'Administrador';
  }

}
