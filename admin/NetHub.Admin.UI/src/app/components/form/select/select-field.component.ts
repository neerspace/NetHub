import { Component, Injector, Input } from '@angular/core';
import { FieldBaseComponent } from '../field-base.component';
import { ISelectOption } from '../types';

@Component({
  selector: 'app-select-field',
  templateUrl: './select-field.component.html',
  styleUrls: ['./select-field.component.scss', '../field-shared.scss'],
})
export class SelectFieldComponent extends FieldBaseComponent {
  @Input() options: ISelectOption[] = [];

  constructor(injector: Injector) {
    super(injector);
  }
}
