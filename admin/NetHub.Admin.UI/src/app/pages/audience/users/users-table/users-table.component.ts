import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/api';
import { FormReady } from 'src/app/components/form/types';
import { ColumnInfo, IFiltered, IFilterInfo } from 'src/app/components/table/types';
import { DownloadService } from 'src/app/services/download.service';
import { userColumns } from '../user-columns';
import { UserService } from '../user.service';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
})
export class UsersTableComponent {
  columns: ColumnInfo[];
  ready: FormReady = null;

  constructor(private downloadService: DownloadService, public usersService: UserService) {
    this.columns = userColumns(this);
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<UserModel>> {
    return this.usersService.filter(params);
  }

  downloadJson() {
    // this.downloadService.downloadAsJson('users', );
  }

  fetchDelete(model: UserModel): void {
    this.usersService.delete(model.id);
  }

  showForm(model?: UserModel | 'create'): void {
    this.usersService.lastFormId = model === 'create' ? 'create' : model?.id;
    this.usersService.showForm();
  }
}
