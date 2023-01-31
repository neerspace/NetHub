import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, Validators } from '@angular/forms';
import { errorMessages } from '../../error-messages';
import { BoolInput } from '../../shared/types';

@Component({
  selector: 'field-base',
  template: ``,
})
export abstract class FieldBaseComponent implements OnInit {
  @Input() label!: string;
  @Input() controlName!: string;
  @Input() required: BoolInput = false;
  @Input() disabled: BoolInput = false;

  errors = errorMessages;
  formControl!: FormControl;
  changed: boolean = false;
  focused: boolean = false;

  protected constructor(protected formGroup: FormGroupDirective) {}

  ngOnInit(): void {
    // this.formGroup.form.error

    this.formControl ??= this.formGroup.form.get(this.controlName) as FormControl;

    setTimeout(() => {
      if (this.required === true || this.required === 'true') {
        this.formControl.addValidators(Validators.required);
      }

      if (this.isTrue(this.disabled)) {
        this.formControl.disable();
      } else {
        this.formControl.enable();
      }
    });
    this.afterInit();
  }

  onChange(event?: Event): void {
    this.changed = true;
  }

  afterInit(): void {}

  isTrue(bool: BoolInput) {
    return bool === true || bool === 'true';
  }
}
