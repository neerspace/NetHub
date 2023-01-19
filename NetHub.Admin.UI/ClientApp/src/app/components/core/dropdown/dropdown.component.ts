import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'div[app-dropdown]',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
  host: {
    class: 'dropdown',
  },
})
export class DropdownComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
