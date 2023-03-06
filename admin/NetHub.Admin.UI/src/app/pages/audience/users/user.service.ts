import { Injectable, Injector } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { ErrorDto, UserCreateRequest, UserModel, UsersApi, UserUpdateRequest } from 'src/app/api';
import { FormId } from 'src/app/components/form/types';
import { IFiltered, IFilterInfo } from 'src/app/components/table/types';
import { FormServiceBase } from 'src/app/services/abstractions';

@Injectable({ providedIn: 'root' })
export class UserService extends FormServiceBase {
  public lastFormId?: FormId;

  constructor(injector: Injector, private readonly usersApi: UsersApi) {
    super(injector, {
      id: new FormControl(),
      userName: new FormControl(),
      firstName: new FormControl(),
      lastName: new FormControl(),
      middleName: new FormControl(),
      password: new FormControl(),
      email: new FormControl(),
      profilePhotoUrl: new FormControl(),
      emailConfirmed: new FormControl(),
      description: new FormControl(),
      registered: new FormControl(),
    });
  }

  showForm(): void {
    this.router.navigate(['users', this.lastFormId]);
  }

  getById(id: number): void {
    this.onRequestStart();
    this.usersApi.getById(id).subscribe({
      next: (user: UserModel | any) => {
        user.password = user.hasPassword ? '********' : null;
        delete user.hasPassword;
        this.form.setValue(user);
        this.onRequestSuccess();
        this.lastFormId = id;
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
        this.lastFormId = undefined;
      },
    });
  }

  filter(request: IFilterInfo): Observable<IFiltered<UserModel>> {
    return this.usersApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  }

  create(): void {
    const request = this.form.value as UserCreateRequest;
    this.onRequestStart();
    this.usersApi.create(request).subscribe({
      next: (user: UserModel) => {
        this.toaster.showSuccess('User successfully created');
        this.onRequestSuccess();
        this.router.navigate(['users', user.id]);
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  update(id: number): void {
    const request = this.form.value as UserUpdateRequest;
    request.id = id;
    this.onRequestStart();
    this.usersApi.update(request).subscribe({
      next: () => {
        this.toaster.showSuccess('User successfully updated');
        this.onRequestSuccess();
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  delete(id: number): void {
    this.modals.showConfirmDelete({
      confirmed: () => {
        this.onRequestStart();
        this.usersApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('User successfully deleted');
            this.router.navigateByUrl('users');
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
    if (this.isReady) {
      this.setReady('loading');
    }
  }

  private onRequestSuccess() {
    // console.log('request succeed');
    this.setReady('ready');
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.setReady('404');
    this.toaster.showFail(error.message);
  }
}
