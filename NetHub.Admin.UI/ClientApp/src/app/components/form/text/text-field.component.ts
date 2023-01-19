import { Component, Input } from '@angular/core';
import { FormGroupDirective, Validators } from '@angular/forms';
import { CopyPasteService } from '../../../services/copy-paste.service';
import { BoolInput } from '../../../shared/types';
import { FieldBaseComponent } from '../field-base.component';
import { InputType } from '../types';

@Component({
  selector: 'app-text-field',
  templateUrl: './text-field.component.html',
  styleUrls: ['../field-shared.scss'],
})
export class TextFieldComponent extends FieldBaseComponent {
  @Input() type: InputType = 'text';
  @Input() maxLength: number = -1;
  @Input() autocomplete: BoolInput = false;
  @Input() allowCopy: BoolInput = false;

  constructor(form: FormGroupDirective, public copyPaste: CopyPasteService) {
    super(form);
  }

  override afterInit() {
    if (this.maxLength > 0) {
      this.formControl.addValidators(Validators.maxLength(+this.maxLength));
    }

    setTimeout(() => {
      if (this.type === 'email') {
        this.formControl.addValidators(Validators.email);
      }
    });
  }
}
