import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskModule } from 'ngx-mask';
import { FieldMessageComponent } from './core/field-message/field-message.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { DateFieldComponent } from './date/date-field.component';
import { DatePickerComponent } from './date/date-picker.component';
import { DateRangePickerComponent } from './date/date-range-picker.component';
import { FormContentComponent } from './form-content/form-content.component';
import { FormLayoutComponent } from './layout/form-layout.component';
import { NumberFieldComponent } from './number/number-field.component';
import { PasswordFieldComponent } from './password/password-field.component';
import { TextAreaFieldComponent } from './text-area/text-area-field.component';
import { TextAreaInputComponent } from './text-area/text-area-input.component';
import { TextFieldComponent } from './text/text-field.component';

@NgModule({
  declarations: [
    FormLayoutComponent,
    NotFoundComponent,
    FieldMessageComponent,

    DatePickerComponent,
    DateRangePickerComponent,
    DateFieldComponent,

    TextFieldComponent,
    PasswordFieldComponent,

    TextAreaInputComponent,
    TextAreaFieldComponent,

    NumberFieldComponent,
    FormContentComponent,
  ],
  exports: [
    FormLayoutComponent,

    DateFieldComponent,
    TextFieldComponent,
    PasswordFieldComponent,

    TextAreaInputComponent,
    TextAreaFieldComponent,

    NumberFieldComponent,
    DatePickerComponent,
    FormContentComponent,
  ],
  imports: [
    CommonModule,
    NgxMaskModule.forRoot({
      validation: false,
    }),
    ReactiveFormsModule,
    FormsModule,
  ],
})
export class FormComponentsModule {}
