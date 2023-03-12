import { Component } from '@angular/core';
import { UserModel } from 'app/api';
import { userColumns } from 'app/pages/audience/users/user-columns';
import { UserService } from 'app/pages/audience/users/user.service';
import { FormReady } from 'neercms/form/types';
import { ColumnInfo, IFiltered, IFilterInfo } from 'neercms/table/types';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
})
export class UsersTableComponent {
  columns: ColumnInfo[];
  ready: FormReady = null;

  constructor(
    // private readonly downloadService: DownloadService,
    public readonly userService: UserService,
  ) {
    this.columns = userColumns(this);
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<UserModel>> {
    return this.userService.filter(params);
  }

  downloadJson() {
    // this.downloadService.downloadAsJson('users', );
  }

  fetchDelete(model: UserModel): void {
    this.userService.delete(model.id);
  }

  showForm(model?: UserModel | 'create'): void {
    this.userService.lastFormId = model === 'create' ? 'create' : model?.id;
    this.userService.showForm();
  }
}
