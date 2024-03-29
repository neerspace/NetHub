import { MenuItems } from 'neercms/layout/types';

export const items: MenuItems = [
  {
    text: 'Dashboard',
    icon: 'la-chalkboard',
    routerLink: '/dashboard',
  },
  {
    text: 'Application',
    iconKind: 'lar',
    icon: 'la-star',
    children: [
      { routerLink: '/users', text: 'Users', icon: 'la-user-friends' },
      { routerLink: '/article-sets', text: 'Article Sets', icon: 'la-th-list' },
      { routerLink: '/articles', text: 'Articles', icon: 'la-book' },
    ],
  },
  {
    text: 'System',
    icon: 'la-tools',
    children: [
      { routerLink: '/roles', text: 'Roles', icon: 'la-key' },
      { routerLink: '/languages', text: 'Languages', icon: 'la-globe-africa' },
    ],
  },
];

export const footerItems: MenuItems = [
  { text: 'Account', icon: 'la-user', routerLink: '/profile/edit' },
  { text: 'Settings', icon: 'la-cog', routerLink: '/profile/settings' },
  { text: 'Log Out', icon: 'la-sign-out-alt', routerLink: '/login', stateData: { logout: true } },
];
