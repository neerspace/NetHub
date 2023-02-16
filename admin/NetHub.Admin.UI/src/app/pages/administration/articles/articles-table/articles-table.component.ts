import { Component, Injector, TemplateRef, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { ArticleModel } from '../../../../api';
import { TabsComponent } from '../../../../components/core/tabs/tabs.component';
import { SplitBaseComponent } from '../../../../components/split/split-base.component';
import { ColumnInfo, IFiltered, IFilterInfo } from '../../../../components/table/types';
import { SettingsKey } from '../../../../services/storage/types';
import { articleColumns } from '../article-columns';
import { ArticlesService } from '../services/article.service';

@Component({
  selector: 'app-articles-table',
  templateUrl: './articles-table.component.html',
  styleUrls: ['./articles-table.component.scss'],
})
export class ArticlesTableComponent extends SplitBaseComponent<ArticleModel> {
  @ViewChild(TabsComponent) tabsComponent!: TabsComponent;
  @ViewChild('article') articleTemplate!: TemplateRef<any>;
  @ViewChild('localization') localizationTemplate!: TemplateRef<any>;

  columns: ColumnInfo[];

  constructor(injector: Injector, public articlesService: ArticlesService) {
    super(injector, SettingsKey.UsersSplitSizes);
    this.columns = articleColumns(this);
  }

  fetchDelete(model: ArticleModel): void {
    this.articlesService.delete(model.id);
  }

  downloadJson() {}

  fetchFilter(params: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesService.filter(params);
  }

  onLocalizationClick() {
    this.tabsComponent.openTab('New Tab', this.localizationTemplate, 'aoa', true);
  onDetailsClick(model: ArticleModel) {

    const article$ = this.articlesService.getById(model.id);
    const localizations$ = this.articlesService.getLocalizations(model.id);

    combineLatest([article$, localizations$])
      .subscribe(([article, localizations]) => {

        this.tabsComponent.closeAllTabs();

        this.tabsComponent.openTab(
          this.generateTabName(article),
          this.articleTemplate,
          article
        );

        for (let localization of localizations) {
          this.tabsComponent.openTab(
            this.generateTabName(localization),
            this.localizationTemplate,
            localization,
            true);
        }

        this.loaderService.hide();
      })
  }

  private generateTabName(data: ArticleModel | ArticleLocalizationModel): string {
    if (data instanceof ArticleModel) {
      return `${data.id}: General`;
    } else {
      const code = data.languageCode.charAt(0).toUpperCase() + data.languageCode.slice(1);
      return `${data.articleId}: ${code}`;
    }
  }
}
