import {
  ActivatedRouteSnapshot,
  DetachedRouteHandle,
  RouteReuseStrategy,
} from '@angular/router';

export class AppRouteReuseStrategy implements RouteReuseStrategy {
  public storedRouteHandles = new Map<string, DetachedRouteHandle>();

  public retrieve(route: ActivatedRouteSnapshot): DetachedRouteHandle | null {
    return this.storedRouteHandles.get(route.routeConfig?.path!) ?? null;
  }

  public shouldAttach(route: ActivatedRouteSnapshot): boolean {
    return this.storedRouteHandles.has(route.routeConfig?.path!);
  }

  public shouldDetach(route: ActivatedRouteSnapshot): boolean {
    return route.data['reuseRoute'] === true;
  }

  public shouldReuseRoute(
    future: ActivatedRouteSnapshot,
    curr: ActivatedRouteSnapshot
  ): boolean {
    return future.routeConfig === curr.routeConfig;
  }

  public store(
    route: ActivatedRouteSnapshot,
    handle: DetachedRouteHandle | null
  ): void {
    this.storedRouteHandles.set(route.routeConfig?.path!, handle!);
  }
}
