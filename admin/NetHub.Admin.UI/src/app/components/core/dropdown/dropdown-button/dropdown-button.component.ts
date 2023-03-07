import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-dropdown-button',
  templateUrl: './dropdown-button.component.html',
  styleUrls: ['./dropdown-button.component.scss'],
})
export class DropdownButtonComponent {
  @Input() buttonText!: any;
  @Output() onclick: EventEmitter<MouseEvent> = new EventEmitter();
}
