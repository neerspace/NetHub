import { Injectable, Injector } from '@angular/core';
import { ArticleLocalizationModel, ErrorDto, LocalizationsApi } from 'src/app/api';
import { FormServiceBase } from '../../../services/abstractions';

@Injectable()
export class LocalizationsService extends FormServiceBase {
  constructor(injector: Injector, private readonly localizationsApi: LocalizationsApi) {
    super(injector, {
      id: ['', []],
      articleId: ['', []],
      languageCode: ['', []],
      contributors: ['', []],
      title: ['', []],
      description: ['', []],
      html: ['', []],
      status: ['', []],
      views: ['', []],
      rate: ['', []],
      created: ['', []],
      updated: ['', []],
      published: ['', []],
      banned: ['', []],
      isSaved: ['', []],
      savedDate: ['', []],
      vote: ['', []],
    });
  }

  getById(id: number): void {
    this.onRequestStart();
    this.localizationsApi.getById(id).subscribe({
      next: (localization: ArticleLocalizationModel | any) => {
        localization.ready = true;
        this.onRequestSuccess();
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
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
        this.localizationsApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('Localization successfully deleted');
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
  }

  private onRequestSuccess() {
    // console.log('request succeed');
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.toaster.showFail(error.message);
  }
}
