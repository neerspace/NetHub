import { Component } from '@angular/core';
import { RoleModel } from 'app/api';
import { roleColumns } from 'app/pages/system/roles/role-columns';
import { RoleService } from 'app/pages/system/roles/role.service';
import { FormReady } from 'neercms/form/types';
import { ColumnInfo, IFiltered, IFilterInfo } from 'neercms/table/types';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-roles-table',
  templateUrl: './roles-table.component.html',
})
export class RolesTableComponent {
  columns: ColumnInfo[];
  ready: FormReady = null;

  constructor(
    // private readonly downloadService: DownloadService,
    public readonly roleService: RoleService,
  ) {
    this.columns = roleColumns(this);
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<RoleModel>> {
    return this.roleService.filter(params);
  }

  // downloadJson() {
  // this.downloadService.downloadAsJson('users', );
  // }

  fetchDelete(model: RoleModel): void {
    this.roleService.delete(model.id);
  }

  showForm(model?: RoleModel | 'create'): void {
    this.roleService.lastFormId = model === 'create' ? 'create' : model?.id;
    this.roleService.showForm();
  }
}
