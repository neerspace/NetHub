import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/api';
import { FormReady } from 'src/app/components/form/types';
import { deleteButton, editButton } from 'src/app/components/table/buttons';
import { ColumnInfo, IFiltered, IFilterInfo, ITableAction } from 'src/app/components/table/types';
import { DownloadService } from 'src/app/services/download.service';
import { userColumns } from '../user-columns';
import { UserService } from '../user.service';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
})
export class UsersTableComponent {
  columns: ColumnInfo[] = userColumns;
  buttons: ITableAction<UserModel>[] = [
    { button: editButton(), onClick: this.showForm.bind(this) },
    { button: deleteButton(), onClick: this.fetchDelete.bind(this) },
  ];
  ready: FormReady = null;

  constructor(private downloadService: DownloadService, public usersService: UserService) {}

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
