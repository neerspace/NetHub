import { Routes } from '@angular/router';
import { AuthorizedGuard } from './api/guards/authorized.guard';
import { EnsurePermissionsGuard } from './api/guards/ensure-permissions.guard';
import { AdminLayoutComponent } from './components/layout/admin-layout/admin-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ErrorComponent } from './pages/public/error/error.component';
import { LoginComponent } from './pages/public/login/login.component';
import { buildTitle } from './shared/utilities';

const adminRouters: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, title: buildTitle('Dashboard') },
  {
    path: 'users',
    loadChildren: () => import('./pages/audience/users/users.module').then(m => m.UsersModule),
    title: buildTitle('Users'),
  },
  {
    path: 'roles',
    loadChildren: () => import('./pages/audience/roles/roles.module').then(m => m.RolesModule),
    title: buildTitle('Roles'),
  },
  {
    path: 'languages',
    loadChildren: () =>
      import('./pages/administration/languages/languages.module').then(m => m.LanguagesModule),
    title: buildTitle('Languages'),
  },
  {
    path: 'articles',
    loadChildren: () =>
      import('./pages/administration/articles/articles.module').then(m => m.ArticlesModule),
    title: buildTitle('Articles')
  },
  {
    path: 'profile',
    loadChildren: () => import('./pages/manage/profile/profile.module').then(m => m.ProfileModule),
    title: buildTitle('Profile'),
  },
];

export const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    canActivate: [AuthorizedGuard],
    canActivateChild: [EnsurePermissionsGuard],
    children: adminRouters,
  },

  { path: 'login', component: LoginComponent, title: buildTitle('Login') },
  { path: 'error/:code', component: ErrorComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: '404' },
];
