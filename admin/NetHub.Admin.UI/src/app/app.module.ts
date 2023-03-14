import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CoreComponentsModule } from 'neercms/core';
import { LayoutComponentsModule } from 'neercms/layout';
import { routes } from './_routes';
import { ApiModule } from './api/api.module';
import { AuthorizedGuard } from './api/guards/authorized.guard';
import { EnsurePermissionsGuard } from './api/guards/ensure-permissions.guard';

import { AppComponent } from './app.component';
import { ApplicationCoreModule } from './core/application-core.module';
import { PagesModule } from './pages/pages.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    // Angular Core
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,

    // Router
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      // anchorScrolling: 'enabled',
    }),

    // NeerCMS
    CoreComponentsModule,
    LayoutComponentsModule,

    // App
    ApiModule,
    ApplicationCoreModule,
    PagesModule,
  ],
  providers: [AuthorizedGuard, EnsurePermissionsGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}
