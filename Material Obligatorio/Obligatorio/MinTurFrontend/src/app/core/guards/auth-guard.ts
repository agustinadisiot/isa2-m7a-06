import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SessionRoutes } from '../routes';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (localStorage.getItem('userInfo')) {
            // logged in so return true
            return true;
        }

        // not logged in so redirect to login page
        this.router.navigate([`/${SessionRoutes.LOGIN}`], { queryParams: { returnUrl: state.url }});
        return false;
    }
}
