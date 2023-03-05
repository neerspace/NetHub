import { Component, Injector, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { combineLatest, Observable } from 'rxjs';
import { ArticleLocalizationModel, ArticleModel } from 'src/app/api';
import { TabsComponent } from 'src/app/components/core/tabs/tabs.component';
import { SplitBaseComponent } from 'src/app/components/split/split-base.component';
import { ColumnInfo, IFiltered, IFilterInfo } from 'src/app/components/table/types';
import { limitStringLength } from '../../../../components/table/formatters';
import { articleColumns } from '../article-columns';
import { ArticlesService } from '../article.service';

@Component({
  selector: 'app-articles-table',
  templateUrl: './articles-table.component.html',
  styleUrls: ['./articles-table.component.scss'],
})
export class ArticlesTableComponent extends SplitBaseComponent<ArticleModel> implements OnInit {
  @ViewChild(TabsComponent) tabsComponent!: TabsComponent;
  @ViewChild('articleForm') articleTemplate!: TemplateRef<any>;
  @ViewChild('localizationForm') localizationTemplate!: TemplateRef<any>;
  @ViewChild('localizationsColumn') localizationsColumnTemplate!: TemplateRef<any>;

  columns: ColumnInfo[] = [];

  constructor(injector: Injector, public articlesService: ArticlesService) {
    super(injector, 'users');
    this.columns = articleColumns(this);
  }

  ngOnInit(): void {
    this.articlesService.getFlags();
  }

  fetchDelete(model: ArticleModel): void {
    this.articlesService.delete(model.id);
  }

  downloadJson() {}

  fetchFilter(params: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesService.filter(params);
  }

  onLocalizationClick(model: ArticleLocalizationModel) {
    console.log(model);
    const tabTitle = model.articleId + ' | ' + limitStringLength(model.title, 20, 'Untitled');
    this.tabsComponent.openTab(tabTitle, this.localizationTemplate, model);
  }

  onDetailsClick(model: ArticleModel) {
    console.log(model);
    const tabTitle = model.id + ' | ' + limitStringLength(model.name, 20, 'Untitled');
    this.tabsComponent.openTab(tabTitle, this.articleTemplate, model);
  }

  onDetailsClick2(model: ArticleModel) {
    const article$ = this.articlesService.getById(model.id);
    const localizations$ = this.articlesService.getLocalizations(model.id);

    combineLatest([article$, localizations$]).subscribe(([article, localizations]) => {
      this.tabsComponent.closeAllTabs();

      this.tabsComponent.openTab(this.generateTabName(article), this.articleTemplate, article);

      for (let localization of localizations) {
        this.tabsComponent.openTab(
          this.generateTabName(localization),
          this.localizationTemplate,
          localization,
          true,
        );
      }

      // this.loaderService.hide();
    });
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
