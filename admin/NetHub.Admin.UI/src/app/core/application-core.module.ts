import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LayoutComponentsModule } from 'neercms';
import { AdminLayoutComponent } from './layout/admin-layout.component';

@NgModule({
  declarations: [AdminLayoutComponent],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule,

    // NeerCMS
    LayoutComponentsModule,
  ],
})
export class ApplicationCoreModule {}
