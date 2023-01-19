import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { routes } from './_routes';
import { ApiModule } from './api/api.module';

import { AppComponent } from './app.component';
import { CoreComponentsModule } from './components/core/core-components.module';
import { LayoutComponentsModule } from './components/layout/layout-components.module';
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

    // App
    ApiModule,
    CoreComponentsModule,
    PagesModule,
    LayoutComponentsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
