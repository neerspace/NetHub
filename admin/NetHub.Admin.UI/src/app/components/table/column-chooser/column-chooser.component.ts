import { Component, Input, OnInit } from '@angular/core';
import { ColumnInfo } from '../types';

@Component({
  selector: 'app-column-chooser',
  templateUrl: './column-chooser.component.html',
  styleUrls: ['./column-chooser.component.scss'],
})
export class ColumnChooserComponent implements OnInit {
  @Input() columns!: ColumnInfo[];

  constructor() {}

  ngOnInit(): void {}
}
