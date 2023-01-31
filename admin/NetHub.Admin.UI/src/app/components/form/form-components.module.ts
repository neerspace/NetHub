import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { FormLayoutComponent } from './layout/form-layout.component';
import { DatePickerComponent } from './date/date-picker.component';
import { DateRangePickerComponent } from './date/date-range-picker.component';
import { FieldMessageComponent } from './core/field-message/field-message.component';
import { NgxMaskModule } from 'ngx-mask';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PasswordFieldComponent } from './password/password-field.component';
import { TextFieldComponent } from './text/text-field.component';
import { TextAreaInputComponent } from './text-area/text-area-input.component';
import { TextAreaFieldComponent } from './text-area/text-area-field.component';
import { NumberFieldComponent } from './number/number-field.component';
import { DateFieldComponent } from './date/date-field.component';
import { FormContentComponent } from './form-content/form-content.component';

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
