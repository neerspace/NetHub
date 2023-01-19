import { Component } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { FieldBaseComponent } from '../field-base.component';

@Component({
  selector: 'app-password-field',
  templateUrl: './password-field.component.html',
  styleUrls: ['../field-shared.scss'],
})
export class PasswordFieldComponent extends FieldBaseComponent {
  showText: boolean = false;

  constructor(form: FormGroupDirective) {
    super(form);
  }

  override afterInit() {}
}
