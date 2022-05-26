import { Component, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Menubar } from 'primeng/menubar';
import { AuthService } from '../../../auth/services/auth.service';

enum Menu {
  Home = 'Home',
  Categories = 'Categories',
  Products = 'Products',
  Subcategories = 'Subcategories',
  Users = 'Users',
}

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent {
  public isLoggedIn: boolean = false;

  public items: MenuItem[] = [
    {
      label: Menu.Home,
      icon: 'pi pi-fw pi-home',
      routerLink: ['/'],
    },
    {
      label: Menu.Categories,
      icon: 'pi pi-fw pi-tag',
      routerLink: ['/categories'],
    },
    {
      label: Menu.Subcategories,
      icon: 'pi pi-fw pi-tags',
      routerLink: ['/subcategories'],
    },
    {
      label: Menu.Products,
      icon: 'pi pi-fw pi-box',
      routerLink: ['/products'],
    },
    {
      label: Menu.Users,
      icon: 'pi pi-fw pi-users',
      routerLink: ['/users'],
    },
  ];

  @ViewChild('mb') public mb: Menubar;

  public constructor(private authService: AuthService) {
    this.mb?.cd.detectChanges();
    this.authService.user$.subscribe((user) => {
      this.isLoggedIn = user ? true : false;
      this.items?.forEach((item) => {
        if (item.label !== Menu.Home) {
          item.visible = this.isLoggedIn;
        }
        if (item.label == Menu.Users) {
          item.visible = user?.role ? user.role === 'admin' : false;
        }
      });
      this.mb?.cd.detectChanges();
    });
  }

  public logout(): void {
    this.authService.logout();
  }
}
