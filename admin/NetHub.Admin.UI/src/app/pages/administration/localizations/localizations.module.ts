import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CoreComponentsModule } from 'app/components/core/core-components.module';
import { FormComponentsModule } from 'app/components/form/form-components.module';
import { LayoutComponentsModule } from 'app/components/layout/layout-components.module';
import { LocalizationFormComponent } from './localization-form/localization-form.component';
import { LocalizationTableComponent } from './localization-table/localization-table.component';

@NgModule({
  declarations: [LocalizationFormComponent, LocalizationTableComponent],
  imports: [
    CommonModule,
    LayoutComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
    ReactiveFormsModule,
  ],
  exports: [LocalizationFormComponent],
})
export class LocalizationsModule {}
