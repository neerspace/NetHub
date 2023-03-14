import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { LocalizationFormComponent } from './localization-form/localization-form.component';
import { LocalizationTableComponent } from './localization-table/localization-table.component';

@NgModule({
  declarations: [LocalizationFormComponent, LocalizationTableComponent],
  imports: [
    // Angular Core
    CommonModule,
    ReactiveFormsModule,
    // NeerCMS
    CoreComponentsModule,
    FormComponentsModule,
    LayoutComponentsModule,
  ],
  exports: [LocalizationFormComponent],
})
export class LocalizationsModule {}
