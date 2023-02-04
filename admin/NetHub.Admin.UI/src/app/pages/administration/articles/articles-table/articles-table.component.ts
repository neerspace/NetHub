import {Component, Injector, TemplateRef, ViewChild} from '@angular/core';
import {ColumnInfo, IFiltered, IFilterInfo, ITableAction} from "../../../../components/table/types";
import {combineLatest, Observable} from "rxjs";
import {deleteButton, detailsButton} from "../../../../components/table/buttons";
import {ArticlesService} from "../services/article.service";
import {ArticleLocalizationModel, ArticleModel} from "../../../../api";
import {articleColumns} from "../article-columns";
import {SettingsKey} from "../../../../services/storage/types";
import {SplitBaseComponent} from "../../../../components/split/split-base.component";
import {TabsComponent} from "../../../../components/core/tabs/tabs.component";
import {LoaderService} from "../../../../services/viewport";

@Component({
  selector: 'app-articles-table',
  templateUrl: './articles-table.component.html',
  styleUrls: ['./articles-table.component.scss']
})
export class ArticlesTableComponent extends SplitBaseComponent<ArticleModel> {
  @ViewChild(TabsComponent) tabsComponent!: TabsComponent;
  @ViewChild('article') articleTemplate!: TemplateRef<any>;
  @ViewChild('localization') localizationTemplate!: TemplateRef<any>;

  additionColumns: ITableAction<ArticleModel>[] = [
    {button: detailsButton, onClick: this.onDetailsClick.bind(this)}
  ];
  columns: ColumnInfo[] = articleColumns;
  buttons: ITableAction<ArticleModel>[] = [
    {button: deleteButton, onClick: this.fetchDelete.bind(this)}
  ];

  constructor(injector: Injector,
              public articlesService: ArticlesService,
              private loaderService: LoaderService) {
    super(injector, SettingsKey.UsersSplitSizes);
  }

  fetchDelete(model: ArticleModel): void {
    this.articlesService.delete(model.id)
  }

  downloadJson() {
  }

  fetchFilter(params: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesService.filter(params);
  }

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
