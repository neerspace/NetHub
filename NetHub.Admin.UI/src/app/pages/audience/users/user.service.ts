import { Injectable } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ErrorDto, UserCreateRequest, UserModel, UsersApi, UserUpdateRequest } from '../../../api';
import { FormGroupReady, FormId, FormReady } from '../../../components/form/types';
import { IFiltered, IFilterInfo } from '../../../components/table/types';
import { ModalsService } from '../../../services/modals.service';
import { LoaderService, ToasterService } from '../../../services/viewport';

@Injectable({ providedIn: 'root' })
export class UserService {
  readonly form: FormGroupReady;
  public lastFormId?: FormId;

  constructor(
    route: ActivatedRoute,
    private router: Router,
    private usersApi: UsersApi,
    private modals: ModalsService,
    private loader: LoaderService,
    private toaster: ToasterService,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;

    this.form = new FormGroup({
      ready: new FormControl<FormReady>(null),
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
    }) as any;
    this.form.ready = this.form.get('ready') as FormControl;
  }

  get onReadyChanges(): Observable<FormReady> {
    return this.form.ready.valueChanges;
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
        user.ready = true;
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
    this.loader.show();
    if (this.form.ready.value === 'ready') {
      this.form.ready.setValue('loading');
    }
  }

  private onRequestSuccess() {
    // console.log('request succeed');
    this.loader.hide();
    this.form.ready.setValue('ready');
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.loader.hide();
    this.form.ready.setValue('404');
    this.toaster.showFail(error.message);
  }
}
