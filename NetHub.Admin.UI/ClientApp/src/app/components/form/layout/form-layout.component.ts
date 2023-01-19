import { Component, Input } from '@angular/core';
import { FormReady } from '../types';

@Component({
  selector: 'form[app-form-layout]',
  templateUrl: './form-layout.component.html',
  styleUrls: ['./form-layout.component.scss'],
  host: {
    class: 'form',
  },
})
export class FormLayoutComponent {
  @Input() ready: FormReady = null;
}
