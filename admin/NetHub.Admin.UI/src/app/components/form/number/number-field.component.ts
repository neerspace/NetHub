import { Component, Input } from '@angular/core';
import { FormGroupDirective } from '@angular/forms';
import { CopyPasteService } from '../../../services/copy-paste.service';
import { NumberInputProcessorService } from '../../../services/number-input-processor.service';
import { BoolInput } from '../../../shared/types';
import { FieldBaseComponent } from '../field-base.component';
import { NumberPattern } from '../types';

@Component({
  selector: 'app-number-field',
  templateUrl: './number-field.component.html',
  styleUrls: ['../field-shared.scss', './number-field.component.scss'],
})
export class NumberFieldComponent extends FieldBaseComponent {
  @Input() format: NumberPattern = 'integer';
  @Input() minValue: number = -Infinity;
  @Input() maxValue: number = Infinity;
  @Input() allowCopy: BoolInput = false;

  mask!: string;

  constructor(
    form: FormGroupDirective,
    private numberInputProcessor: NumberInputProcessorService,
    public copyPaste: CopyPasteService,
  ) {
    super(form);
  }

  override afterInit() {
    this.mask = this.numberInputProcessor.patternToMask(this.format);
  }

  override onChange(event?: Event) {
    super.onChange(event);

    const value = +this.formControl.value;
    if (value < this.minValue) {
      this.formControl.setValue(this.minValue);
    } else if (value > this.maxValue) {
      this.formControl.setValue(this.maxValue);
    }
  }

  onKeyUp(event: Event): void {
    // To avoid a float numbers math hell
    function fuckJS(n: number, inc: number): number {
      const [integer, floating] = n.toString().split('.');
      return parseFloat(+integer + inc + '.' + floating);
    }

    if ((event as KeyboardEvent).key === 'ArrowUp') {
      this.formControl.setValue(fuckJS(this.formControl.value, 1));
      this.onChange();
    } else if ((event as KeyboardEvent).key === 'ArrowDown') {
      this.formControl.setValue(fuckJS(this.formControl.value, -1));
      this.onChange();
    }
  }
}
