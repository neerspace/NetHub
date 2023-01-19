import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleModel } from 'src/app/api';
import { FormReady } from 'src/app/components/form/types';
import { ColumnInfo, IFiltered, IFilterInfo, ITableAction } from 'src/app/components/table/types';
import { DownloadService } from 'src/app/services/download.service';
import { roleColumns } from '../role-columns';
import { RoleService } from '../role.service';

@Component({
  selector: 'app-roles-table',
  templateUrl: './roles-table.component.html',
  styleUrls: ['./roles-table.component.scss'],
})
export class RolesTableComponent {
  columns: ColumnInfo[] = roleColumns;
  buttons: ITableAction<RoleModel>[] = [
    {
      button: { class: 'success', text: 'Permissions', icon: 'la-edit' },
      onClick: this.showForm.bind(this),
    },
  ];
  ready: FormReady = null;

  constructor(private downloadService: DownloadService, public roleService: RoleService) {}

  fetchFilter(params: IFilterInfo): Observable<IFiltered<RoleModel>> {
    return this.roleService.filter(params);
  }

  downloadJson() {
    // this.downloadService.downloadAsJson('users', );
  }

  fetchDelete(model: RoleModel): void {
    this.roleService.delete(model.id);
  }

  showForm(model?: RoleModel | 'create'): void {
    this.roleService.lastFormId = model === 'create' ? 'create' : model?.id;
    this.roleService.showForm();
  }
}
