import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { CoreComponentsModule, FormComponentsModule, LayoutComponentsModule } from 'neercms';
import { EditProfileComponent } from './edit/edit-profile.component';
import { SettingsComponent } from './settings/settings.component';

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
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,

    // NeerCMS
    CoreComponentsModule,
    LayoutComponentsModule,
    FormComponentsModule,
  ],
  exports: [RouterModule],
})
export class ProfileModule {}
