import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { TableComponentsModule } from 'neercms/table';
import { RolesFormComponent } from './roles-form/roles-form.component';
import { RolesTableComponent } from './roles-table/roles-table.component';

const routes: Routes = [
  { path: '', component: RolesTableComponent },
  { path: ':id', component: RolesFormComponent },
];

@NgModule({
  declarations: [RolesFormComponent, RolesTableComponent],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    // NeerCMS
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
  ],
})
export class RolesModule {}
