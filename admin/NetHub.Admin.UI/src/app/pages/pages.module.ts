import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterLinkWithHref } from '@angular/router';
import { CoreComponentsModule } from '../components/core/core-components.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ErrorComponent } from './public/error/error.component';
import { LoginComponent } from './public/login/login.component';
import { FormComponentsModule } from '../components/form/form-components.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [DashboardComponent, ErrorComponent, LoginComponent],
  imports: [
    CommonModule,
    CoreComponentsModule,
    FormComponentsModule,
    ReactiveFormsModule,
    RouterLinkWithHref,
  ],
})
export class PagesModule {}
