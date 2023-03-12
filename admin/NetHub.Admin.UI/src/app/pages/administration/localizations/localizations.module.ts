import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CoreComponentsModule, FormComponentsModule, LayoutComponentsModule } from 'neercms';
import { LocalizationFormComponent } from './localization-form/localization-form.component';
import { LocalizationTableComponent } from './localization-table/localization-table.component';

@NgModule({
  declarations: [LocalizationFormComponent, LocalizationTableComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,

    LayoutComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
  ],
  exports: [LocalizationFormComponent],
})
export class LocalizationsModule {}
