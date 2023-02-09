import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StorageService } from '../../../services/storage';
import { ColumnInfo } from '../types';

@Component({
  selector: 'app-column-chooser',
  templateUrl: './column-chooser.component.html',
  styleUrls: ['./column-chooser.component.scss'],
})
export class ColumnChooserComponent implements OnInit {
  @Input() columns!: ColumnInfo[];
  @Input() sequenceName!: string;

  @Output() columnsChange: EventEmitter<ColumnInfo[]> = new EventEmitter();

  sequence: string[] = [];
  optionColumns!: ColumnInfo[];

  constructor(private storage: StorageService) {}

  ngOnInit(): void {
    this.sequence = this.storage.getColumnSequence(this.sequenceName) || [];

    if (this.sequence && this.sequence.length > 0) {
      for (const column of this.columns as any[]) {
        column.hidden = column.hideable && !this.sequence.includes(column.key);
      }
      this.optionColumns = this.columns.filter(x => x.key && x.title);
    } else {
      this.sequence = this.columns.filter(x => !x.hidden).map(x => x.key!);
    }

    this.optionColumns = this.columns.filter(x => x.key && x.title);
  }

  onOptionChange(column: ColumnInfo, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    column.hidden = !checkbox.checked;

    if (column.hidden) {
      // Disallow hide last column
      if (this.sequence.length <= 1) {
        checkbox.checked = true;
        column.hidden = false;
      } else {
        this.sequence = this.sequence.filter(x => x !== column.key);
      }
    } else {
      this.sequence.push(column.key!);
    }

    this.columnsChange.emit(this.columns);
    this.storage.setColumnSequence(this.sequenceName, this.sequence);
  }
}
