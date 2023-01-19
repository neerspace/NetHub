import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { LanguageModel } from 'src/app/api';
import { deleteButton, editButton } from 'src/app/components/table/buttons';
import { ColumnInfo, IFiltered, IFilterInfo, ITableAction } from 'src/app/components/table/types';
import { DownloadService } from 'src/app/services/download.service';
import { languageColumns } from '../language-columns';
import { LanguageService } from '../language.service';

@Component({
  selector: 'app-langs-table',
  templateUrl: './langs-table.component.html',
  styleUrls: ['./langs-table.component.scss'],
})
export class LangsTableComponent {
  columns: ColumnInfo[] = languageColumns;
  buttons: ITableAction<LanguageModel>[] = [
    { button: editButton, onClick: this.showForm.bind(this) },
    { button: deleteButton, onClick: this.fetchDelete.bind(this) },
  ];

  constructor(private downloadService: DownloadService, public languagesService: LanguageService) {}

  fetchFilter(params: IFilterInfo): Observable<IFiltered<LanguageModel>> {
    return this.languagesService.filter(params);
  }

  downloadJson() {
    // this.downloadService.downloadAsJson('users', );
  }

  fetchDelete(model: LanguageModel): void {
    this.languagesService.delete(model.code);
  }

  showForm(model?: LanguageModel | 'create'): void {
    if (model === 'create') {
      this.languagesService.form.reset();
      this.languagesService.lastFormId = 'create';
    } else {
      this.languagesService.lastFormId = model?.code;
    }
    this.languagesService.showForm();
  }
}
