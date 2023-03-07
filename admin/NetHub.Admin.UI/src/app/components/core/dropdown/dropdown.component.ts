import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
  host: {
    class: 'dropdown',
  },
})
export class DropdownComponent {
  @Input() key!: string;
  @Input() buttonIcon?: string;
  @Input() buttonText!: any;
}
