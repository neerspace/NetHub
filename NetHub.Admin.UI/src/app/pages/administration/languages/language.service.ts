import { Injectable } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ErrorDto, LanguageModel, LanguagesApi } from '../../../api';
import { FormGroupReady, FormId, FormReady } from '../../../components/form/types';
import { IFiltered, IFilterInfo } from '../../../components/table/types';
import { ModalsService } from '../../../services/modals.service';
import { LoaderService, ToasterService } from '../../../services/viewport';

@Injectable({ providedIn: 'root' })
export class LanguageService {
  readonly form: FormGroupReady;
  public lastFormId?: FormId<string>;

  constructor(
    route: ActivatedRoute,
    formBuilder: FormBuilder,
    private router: Router,
    private modals: ModalsService,
    private loader: LoaderService,
    private toaster: ToasterService,
    private languagesApi: LanguagesApi,
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;

    this.form = formBuilder.group({
      ready: [null as FormReady, []],
      code: ['', []],
      name: ['', []],
      nameLocal: ['', []],
    }) as any;

    this.form.ready = this.form.get('ready') as FormControl;
  }

  get onReadyChanges(): Observable<FormReady> {
    return this.form.ready.valueChanges;
  }

  showForm(): void {
    this.router.navigate(['languages', this.lastFormId]);
  }

  getByCode(code: string): void {
    this.onRequestStart();
    this.languagesApi.getByCode(code).subscribe({
      next: (lang: LanguageModel | any) => {
        lang.ready = true;
        lang.nameLocal = lang.name[code] || '';
        lang.name = lang.name.en;
        this.form.setValue(lang);
        this.onRequestSuccess();
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  filter(request: IFilterInfo): Observable<IFiltered<LanguageModel>> {
    return this.languagesApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  }

  create(): void {
    this.onRequestStart();
    this.languagesApi.create(this.getRequestModel()).subscribe({
      next: (lang: LanguageModel) => {
        this.toaster.showSuccess('Language successfully created');
        this.onRequestSuccess();
        this.router.navigate(['languages', lang.code]);
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  update(code: string): void {
    this.onRequestStart();
    this.languagesApi.update(this.getRequestModel(code)).subscribe({
      next: () => {
        this.toaster.showSuccess('Language successfully updated');
        this.onRequestSuccess();
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  delete(code: string): void {
    this.modals.showConfirmDelete({
      confirmed: () => {
        this.onRequestStart();
        this.languagesApi.delete(code).subscribe({
          next: () => {
            this.toaster.showSuccess('Language successfully deleted');
            this.router.navigateByUrl('languages');
            this.onRequestSuccess();
          },
          error: (error: ErrorDto) => {
            this.onRequestError(error);
          },
        });
      },
    });
  }

  private getRequestModel(code?: string): LanguageModel {
    const value = this.form.value;
    code = value.code || code;
    return LanguageModel.fromJS({
      code: code!,
      name: {
        en: value.name,
        [code!]: value.nameLocal,
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
    this.loader.hide();

    if (error.status === 400) {
      this.form.get('code')!.setErrors({
        'api-error': error['errors'].code,
      });
      this.toaster.showFail(error.message);
      this.form.ready.setValue('ready');
    } else if (error.status === 500) {
      this.toaster.showFail(error['errors'].exception);
      this.form.ready.setValue('ready');
    } else {
      this.toaster.showFail(error.message);
      this.form.ready.setValue('404');
    }
  }
}
