import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleModel } from 'app/api';
import { FormReady } from 'app/components/form/types';
import { ColumnInfo, IFiltered, IFilterInfo } from 'app/components/table/types';
import { roleColumns } from 'app/pages/audience/roles/role-columns';
import { RoleService } from 'app/pages/audience/roles/role.service';

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
