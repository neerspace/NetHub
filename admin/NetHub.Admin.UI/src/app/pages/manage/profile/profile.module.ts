import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule } from '../../../components/core/core-components.module';
import { EditProfileComponent } from './edit/edit-profile.component';
import { SettingsComponent } from './settings/settings.component';
import { LayoutComponentsModule } from '../../../components/layout/layout-components.module';
import { FormComponentsModule } from '../../../components/form/form-components.module';
import { TextAreaFieldComponent } from '../../../components/form/text-area/text-area-field.component';

const routes: Routes = [
  {
    path: 'edit',
    component: EditProfileComponent,
  },
  {
    path: 'settings',
    component: SettingsComponent,
  },
];

@NgModule({
  declarations: [EditProfileComponent, SettingsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CoreComponentsModule,
    ReactiveFormsModule,
    LayoutComponentsModule,
    FormComponentsModule,
  ],
  exports: [RouterModule],
})
export class ProfileModule {}
