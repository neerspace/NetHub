import { Component, Injector, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ArticleLocalizationModel, ArticleModel } from 'app/api';
import { ArticleSharedService } from 'app/pages/app/article-shared.service';
import { articleSetColumns } from 'app/pages/app/article-sets/article-set-columns';
import { TabsComponent } from 'neercms/core';
import { SplitBaseComponent } from 'neercms/split';
import { DataTableComponent } from 'neercms/table';
import { ColumnInfo, IFiltered, IFilterInfo } from 'neercms/table/types';
import { finalize, Observable } from 'rxjs';

@Component({
  selector: 'app-article-sets-table',
  templateUrl: './article-sets-table.component.html',
  styleUrls: ['./article-sets-table.component.scss'],
})
export class ArticleSetsTableComponent extends SplitBaseComponent<ArticleModel> implements OnInit {
  @ViewChild(TabsComponent) tabsComponent!: TabsComponent;
  @ViewChild('table') table!: DataTableComponent<ArticleModel>;
  @ViewChild('articleForm') articleTemplate!: TemplateRef<any>;
  @ViewChild('localizationForm') localizationTemplate!: TemplateRef<any>;
  @ViewChild('localizationsColumn') localizationsColumnTemplate!: TemplateRef<any>;

  columns: ColumnInfo[] = [];

  constructor(injector: Injector, public readonly articleSharedService: ArticleSharedService) {
    super(injector, 'users');
    this.columns = articleSetColumns(this);
  }

  ngOnInit(): void {
    this.articleSharedService.getFlags();
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articleSharedService.filter(params).pipe(
      finalize(() => {
        const item = this.table.data?.[0].localizations[0];
        this.onLocalizationClick(item);
        this.onLocalizationClick(this.table.data?.[0].localizations[2]);
      }),
    );
  }

  onLocalizationClick(model: ArticleLocalizationModel) {
    const tabTitle = `${model.articleId} | ${model.languageCode.toUpperCase()}`;
    this.tabsComponent.openTab(
      this.getArticleLocalizationId(model),
      tabTitle,
      this.localizationTemplate,
      model,
    );
  }

  onDetailsClick(model: ArticleModel) {
    console.log(model);
    const tabTitle = `${model.id} | General`;
    this.tabsComponent.openTab(this.getArticleId(model), tabTitle, this.articleTemplate, model);
  }

  // onDetailsClick2(model: ArticleModel) {
  //   const article$ = this.articlesService.getById(model.id);
  //   const localizations$ = this.articlesService.getLocalizations(model.id);
  //
  //   combineLatest([article$, localizations$]).subscribe(([article, localizations]) => {
  //     this.tabsComponent.closeAllTabs();
  //
  //     this.tabsComponent.openTab(this.generateTabName(article), this.articleTemplate, article);
  //
  //     for (let localization of localizations) {
  //       this.tabsComponent.openTab(
  //         this.generateTabName(localization),
  //         this.localizationTemplate,
  //         localization,
  //         true,
  //       );
  //     }
  //
  //     // this.loaderService.hide();
  //   });
  // }

  private generateTabName(data: ArticleModel | ArticleLocalizationModel): string {
    if (data instanceof ArticleModel) {
      return `${data.id}: General`;
    } else {
      const code = data.languageCode.charAt(0).toUpperCase() + data.languageCode.slice(1);
      return `${data.articleId}: ${code}`;
    }
  }

  private getArticleId(model: ArticleModel): string {
    return model.id.toString();
  }

  private getArticleLocalizationId(model: ArticleLocalizationModel): string {
    return model.articleId + '_' + model.id;
  }
}
