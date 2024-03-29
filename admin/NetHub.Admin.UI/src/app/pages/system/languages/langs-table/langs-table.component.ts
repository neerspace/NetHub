import { Component } from '@angular/core';
import { LanguageModel } from 'app/api';
import { languageColumns } from 'app/pages/system/languages/language-columns';
import { LanguageService } from 'app/pages/system/languages/language.service';
import { ColumnInfo, IFiltered, IFilterInfo } from 'neercms/table/types';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-langs-table',
  templateUrl: './langs-table.component.html',
  styleUrls: ['./langs-table.component.scss'],
})
export class LangsTableComponent {
  columns: ColumnInfo[];

  constructor(
    // private readonly downloadService: DownloadService,
    public readonly languageService: LanguageService,
  ) {
    this.columns = languageColumns(this);
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<LanguageModel>> {
    return this.languageService.filter(params);
  }

  downloadJson() {
    // this.downloadService.downloadAsJson('users', );
  }

  fetchDelete(model: LanguageModel): void {
    this.languageService.delete(model.code);
  }

  showForm(model?: LanguageModel | 'create'): void {
    if (model === 'create') {
      this.languageService.form.reset();
      this.languageService.lastFormId = 'create';
    } else {
      this.languageService.lastFormId = model?.code;
    }
    this.languageService.showForm();
  }
}
