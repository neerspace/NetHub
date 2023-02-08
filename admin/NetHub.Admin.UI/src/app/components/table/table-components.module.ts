import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TablePaginationComponent } from './pagination/table-pagination.component';
import { TablePageSizeComponent } from './page-size-select/table-page-size.component';
import { DataTableComponent } from './data-table/data-table.component';
import { FormComponentsModule } from '../form/form-components.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TableFilterFieldComponent } from './filter-field/table-filter-field.component';
import { NgxMaskModule } from 'ngx-mask';
import { ColumnChooserComponent } from './column-chooser/column-chooser.component';

@NgModule({
  declarations: [
    DataTableComponent,
    TablePaginationComponent,
    TablePageSizeComponent,
    TableFilterFieldComponent,
    ColumnChooserComponent,
  ],
  exports: [DataTableComponent],
  imports: [CommonModule, FormComponentsModule, ReactiveFormsModule, NgxMaskModule],
})
export class TableComponentsModule {}
