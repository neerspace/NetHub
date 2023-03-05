import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import {
  ArticleLocalizationModel,
  ArticleModel,
  ArticlesApi,
  ErrorDto,
  LanguagesApi,
} from 'src/app/api';
import { FormId, FormReady } from 'src/app/components/form/types';
import { IFiltered, IFilterInfo } from 'src/app/components/table/types';
import { FormServiceBase } from '../../../services/abstractions';
import { IDictionary } from '../../../shared/types';

@Injectable()
export class ArticlesService extends FormServiceBase {
  flags: IDictionary<string> = {};

  constructor(
    injector: Injector,
    private readonly articlesApi: ArticlesApi,
    private readonly languagesApi: LanguagesApi,
  ) {
    super(injector, {
      ready: [null as FormReady, []],
      id: ['', []],
      name: ['', []],
      permissions: [[], []],
    });
  }

  public lastFormId?: FormId;

  showForm(): void {
    this.router.navigate(['articles', this.lastFormId]);
  }

  getByIdAndSetToForm(id: number): void {
    this.onRequestStart();
    this.articlesApi.getById(id).subscribe({
      next: (article: ArticleModel | any) => {
        article.ready = true;
        this.form.setValue(article);
        this.onRequestSuccess();
        this.lastFormId = id;
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
        this.lastFormId = undefined;
      },
    });
  }

  getById(id: number): Observable<ArticleModel> {
    return this.articlesApi.getById(id);
  }

  filter(request: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  }

  getFlags() {
    this.languagesApi.filter(undefined, undefined, undefined, undefined).subscribe(langs => {
      for (const lang of langs.data) {
        this.flags[lang.code] = lang.flagUrl ?? '/assets/default-flag.svg';
      }
    });
  }

  getLocalizations(id: number): Observable<ArticleLocalizationModel[]> {
    return this.articlesApi.getByArticleId(id);
  }

  // update(id: number): void {
  //   const request = this.form.value as ArticleUpdateRequest;
  //   request.id = id;
  //   this.onRequestStart();
  //   this.articlesApi.update(request).subscribe({
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
        this.articlesApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('Article successfully deleted');
            this.router.navigateByUrl('articles');
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
