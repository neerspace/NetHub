import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { TableComponentsModule } from 'neercms/table';
import { LangsFormComponent } from './langs-form/langs-form.component';
import { LangsTableComponent } from './langs-table/langs-table.component';

const routes: Routes = [
  { path: '', component: LangsTableComponent },
  { path: ':code', component: LangsFormComponent },
];

@NgModule({
  declarations: [LangsTableComponent, LangsFormComponent],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    // NeerCMS
    CoreComponentsModule,
    LayoutComponentsModule,
    TableComponentsModule,
    FormComponentsModule,
  ],
})
export class LanguagesModule {}
