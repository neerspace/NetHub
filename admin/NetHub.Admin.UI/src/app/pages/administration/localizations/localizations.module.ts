import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LocalizationFormComponent } from './localization-form/localization-form.component';
import { LocalizationTableComponent } from './localization-table/localization-table.component';

@NgModule({
  declarations: [LocalizationFormComponent, LocalizationTableComponent],
  imports: [CommonModule],
})
export class LocalizationsModule {}
