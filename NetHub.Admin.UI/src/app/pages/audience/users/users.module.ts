import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule } from '../../../components/core/core-components.module';
import { FormComponentsModule } from '../../../components/form/form-components.module';
import { LayoutComponentsModule } from '../../../components/layout/layout-components.module';
import { TableComponentsModule } from '../../../components/table/table-components.module';
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
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,

    // App
    CoreComponentsModule,
    FormComponentsModule,
    LayoutComponentsModule,
    TableComponentsModule,
  ],
  exports: [RouterModule],
})
export class UsersModule {}
