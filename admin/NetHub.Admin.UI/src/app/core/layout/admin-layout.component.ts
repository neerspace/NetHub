import { Component } from '@angular/core';
import { footerItems, items } from 'app/_menu-items';
import { IMenuItem, MenuItems } from 'neercms/layout/types';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss'],
})
export class AdminLayoutComponent {
  menuItems: MenuItems = items;
  footerMenu: IMenuItem = {
    text: 'Administrator',
    image: 'https://github.com/jurilents.png',
    collapseByDefault: true,
    children: footerItems,
  };
}
