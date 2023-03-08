import { Component, EventEmitter, Injector, Input, Output } from '@angular/core';
import { FieldBaseComponent } from '../field-base.component';
import { FormError, ISelectOption } from '../types';

@Component({
  selector: 'app-select-field',
  templateUrl: './select-field.component.html',
  styleUrls: ['./select-field.component.scss', '../field-shared.scss'],
})
export class SelectFieldComponent extends FieldBaseComponent {
  @Input() options: ISelectOption[] = [];
  @Input() selected!: ISelectOption;

  @Output() selectedChange: EventEmitter<ISelectOption> = new EventEmitter();

  constructor(injector: Injector) {
    super(injector);
  }

  override afterInit() {
    if (!this.options || this.options.length === 0) {
      throw new FormError(`Select field '${this.controlName}' has no options passed`);
    }

    if (!this.selected) {
      this.selected = this.options[0];
    }
  }

  onOptionSelect(option: ISelectOption) {
    this.selected = option;
    this.selectedChange.emit(option);
    this.formControl.setValue(option.key);
    option.onSelected?.(option);
  }
}
