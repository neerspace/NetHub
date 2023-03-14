import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { TableComponentsModule } from 'neercms/table';
import { UserFormComponent } from './user-form/user-form.component';
import { UsersTableComponent } from './users-table/users-table.component';

const routes: Routes = [
  { path: '', component: UsersTableComponent },
  { path: ':id', component: UserFormComponent },
];

@NgModule({
  declarations: [
    // Components
    UsersTableComponent,
    UserFormComponent,
  ],
  exports: [RouterModule],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    // NeerCMS
    CoreComponentsModule,
    FormComponentsModule,
    TableComponentsModule,
    LayoutComponentsModule,
  ],
})
export class UsersModule {}
