import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import {
  CoreComponentsModule,
  FormComponentsModule,
  LayoutComponentsModule,
  TableComponentsModule,
} from 'neercms';
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
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
    ReactiveFormsModule,
  ],
})
export class RolesModule {}
