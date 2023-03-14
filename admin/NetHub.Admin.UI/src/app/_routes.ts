import { Routes } from '@angular/router';
import { buildTitle } from 'neercms/shared/utilities';
import { AuthorizedGuard } from './api/guards/authorized.guard';
import { EnsurePermissionsGuard } from './api/guards/ensure-permissions.guard';
import { AdminLayoutComponent } from './core/layout/admin-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ErrorComponent } from './pages/public/error/error.component';
import { LoginComponent } from './pages/public/login/login.component';

const adminRouters: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, title: buildTitle('Dashboard') },
  {
    path: 'users',
    loadChildren: () => import('./pages/app/users/users.module').then(m => m.UsersModule),
    title: buildTitle('Users'),
  },
  {
    path: 'roles',
    loadChildren: () => import('./pages/system/roles/roles.module').then(m => m.RolesModule),
    title: buildTitle('Roles'),
  },
  {
    path: 'languages',
    loadChildren: () =>
      import('./pages/system/languages/languages.module').then(m => m.LanguagesModule),
    title: buildTitle('Languages'),
  },
  {
    path: 'article-sets',
    loadChildren: () =>
      import('./pages/app/article-sets/article-sets.module').then(m => m.ArticleSetsModule),
    title: buildTitle('Articles'),
  },
  {
    path: 'articles',
    loadChildren: () => import('./pages/app/articles/articles.module').then(m => m.ArticlesModule),
    title: buildTitle('Articles'),
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
