import { Injectable, Injector } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ErrorDto, PermissionModel, PermissionsApi, RoleModel, RolesApi } from 'src/app/api';
import { FormId, FormReady } from 'src/app/components/form/types';
import { IFiltered, IFilterInfo } from 'src/app/components/table/types';
import { ModalsService } from 'src/app/services/modals.service';
import { LoaderService, ToasterService } from 'src/app/services/viewport';
import { FormServiceBase } from '../../../services/abstractions/form-service-base';

export enum PermissionState {
  none = 0,
  read = 1,
  manage = 2,
}

export type PermissionModelExtended = PermissionModel & {
  state: PermissionState;
  parent: PermissionModelExtended | null;
};

@Injectable({ providedIn: 'root' })
export class RoleService extends FormServiceBase {
  public lastFormId?: FormId;
  public permissions?: PermissionModel[];

  constructor(
    injector: Injector,
    route: ActivatedRoute,
    formBuilder: FormBuilder,
    private readonly rolesApi: RolesApi,
    private readonly permissionsApi: PermissionsApi,
    private readonly modals: ModalsService,
    private readonly loader: LoaderService,
    private readonly toaster: ToasterService,
  ) {
    super(injector, {
      ready: [null as FormReady, []],
      id: ['', []],
      name: ['', []],
      permissions: [[] as string[], []],
    });
  }

  showForm(): void {
    this.router.navigate(['roles', this.lastFormId]);
  }

  getAllPermissions(): void {
    this.permissionsApi.get().subscribe({
      next: permissions => {
        this.permissions = permissions;
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  private extendPermissions(sourcePermissions: PermissionModel[]): PermissionModelExtended[] {
    const result: PermissionModelExtended[] = [];

    for (const permission of sourcePermissions as PermissionModelExtended[]) {
      // permission.state = ;
      //   ? PermissionState.manage
      //   : permission.result.push(permission);
    }

    return result;
  }

  getById(id: number): void {
    this.onRequestStart();
    this.rolesApi.getById(id).subscribe({
      next: (role: RoleModel | any) => {
        role.ready = true;
        this.form.setValue(role);
        this.onRequestSuccess();
        this.lastFormId = id;
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
        this.lastFormId = undefined;
      },
    });
  }

  filter(request: IFilterInfo): Observable<IFiltered<RoleModel>> {
    return this.rolesApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  }

  // create(): void {
  //   const request = this.form.value as RoleModel;
  //   this.onRequestStart();
  //   this.rolesApi.create(request).subscribe({
  //     next: (user: UserModel) => {
  //       this.toaster.showSuccess('User successfully created');
  //       this.onRequestSuccess();
  //       this.router.navigate(['users', user.id]);
  //     },
  //     error: (error: ErrorDto) => {
  //       this.onRequestError(error);
  //     },
  //   });
  // }

  // update(id: number): void {
  //   const request = this.form.value as UserUpdateRequest;
  //   request.id = id;
  //   this.onRequestStart();
  //   this.usersApi.update(request).subscribe({
  //     next: () => {
  //       this.toaster.showSuccess('User successfully updated');
  //       this.onRequestSuccess();
  //     },
  //     error: (error: ErrorDto) => {
  //       this.onRequestError(error);
  //     },
  //   });
  // }

  delete(id: number): void {
    this.modals.showConfirmDelete({
      confirmed: () => {
        this.onRequestStart();
        this.rolesApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('Role successfully deleted');
            this.router.navigateByUrl('roles');
            this.onRequestSuccess();
          },
          error: (error: ErrorDto) => {
            this.onRequestError(error);
          },
        });
      },
    });
  }

  private onRequestStart() {
    // console.log('request started');
    this.loader.show();
    if (this.isReady) {
      this.setReady('loading');
    }
  }

  private onRequestSuccess() {
    // console.log('request succeed');
    this.loader.hide();
    this.setReady('ready');
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.loader.hide();
    this.setReady('404');
    this.toaster.showFail(error.message);
  }
}
