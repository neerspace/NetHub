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

    // App
    CoreComponentsModule,
    LayoutComponentsModule,
    TableComponentsModule,
    FormComponentsModule,
  ],
})
export class LanguagesModule {}
