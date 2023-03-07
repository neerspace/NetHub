import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ArticleModel, ArticlesApi, LanguagesApi } from '../../api';
import { FormId } from '../../components/form/types';
import { IFiltered, IFilterInfo } from '../../components/table/types';
import { IDictionary } from '../../shared/types';

@Injectable({ providedIn: 'root' })
export class ArticleSharedService {
  public lastFormId?: FormId;
  flags: IDictionary<string> = {};

  constructor(
    private readonly router: Router,
    private readonly articlesApi: ArticlesApi,
    private readonly languagesApi: LanguagesApi,
  ) {}

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
}
