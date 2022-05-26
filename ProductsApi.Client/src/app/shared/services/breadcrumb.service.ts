import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BreadcrumbService {
  private readonly _breadcrumb$ = new BehaviorSubject<MenuItem[]>([]);

  public readonly breadcrumb$ = this._breadcrumb$.asObservable();

  public constructor(private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this._breadcrumb$.next(
          this.buildBreadcrumb(this.router.routerState.snapshot.root)
        );
      }
    });
  }

  private buildBreadcrumb(route: ActivatedRouteSnapshot): MenuItem[] {
    const breadcrumb: MenuItem[] = [{ label: 'Home', routerLink: '/' }];
    let path = '';
    while (route.firstChild) {
      route = route.firstChild;
      path += '/' + route.url.join('/');
      if (
        route.data['breadcrumb'] &&
        route.parent?.data['breadcrumb'] != route.data['breadcrumb']
      ) {
        breadcrumb.push({ label: route.data['breadcrumb'], routerLink: path });
      }
    }
    return breadcrumb;
  }
}
