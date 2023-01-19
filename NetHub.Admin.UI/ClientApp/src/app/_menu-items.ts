import { MenuItems } from './components/layout/types';

export const items: MenuItems = [
  {
    text: 'Dashboard',
    icon: 'la-chalkboard',
    routerLink: '/dashboard',
  },
  {
    text: 'Audience',
    icon: 'la-user-shield',
    children: [
      { routerLink: '/users', text: 'Users', icon: 'la-user-friends' },
      { routerLink: '/roles', text: 'Roles', icon: 'la-key' },
    ],
  },
  {
    text: 'Administration',
    icon: 'la-tools',
    children: [
      //
      { routerLink: '/languages', text: 'Languages', icon: 'la-globe-africa' },
    ],
  },
];

export const footerItems: MenuItems = [
  {
    text: 'My Profile',
    icon: 'la-user',
    routerLink: '/profile/edit',
  },
  {
    text: 'Settings',
    icon: 'la-cog',
    routerLink: '/profile/settings',
  },
  {
    text: 'Log Out',
    icon: 'la-sign-out-alt',
    routerLink: '/login',
    stateData: { logout: true },
  },
];
