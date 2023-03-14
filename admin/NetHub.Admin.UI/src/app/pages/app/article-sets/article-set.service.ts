import { Injectable, Injector } from '@angular/core';
import {
  ArticleLocalizationModel,
  ArticleModel,
  ArticlesApi,
  ErrorDto,
  LanguagesApi,
} from 'app/api';
import { FormServiceBase } from 'neercms/form';
import { FormReady } from 'neercms/form/types';
import { Observable } from 'rxjs';

@Injectable()
export class ArticleSetService extends FormServiceBase {
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

  getById(id: number): Observable<ArticleModel> {
    return this.articlesApi.getById(id);
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
