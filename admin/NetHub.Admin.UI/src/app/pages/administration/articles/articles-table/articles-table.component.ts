import {Component, Injector, TemplateRef, ViewChild} from '@angular/core';
import {ColumnInfo, IFiltered, IFilterInfo, ITableAction} from "../../../../components/table/types";
import {Observable} from "rxjs";
import {deleteButton} from "../../../../components/table/buttons";
import {ArticlesService} from "../services/article.service";
import {ArticleModel} from "../../../../api";
import {articleColumns} from "../article-columns";
import {SettingsKey} from "../../../../services/storage/types";
import {SplitBaseComponent} from "../../../../components/split/split-base.component";
import {TabsComponent} from "../../../../components/core/tabs/tabs.component";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-articles-table',
  templateUrl: './articles-table.component.html',
  styleUrls: ['./articles-table.component.scss']
})
export class ArticlesTableComponent extends SplitBaseComponent<ArticleModel>{
  @ViewChild(TabsComponent) tabsComponent!: TabsComponent;
  @ViewChild('localization') localizationTemplate!: TemplateRef<any>;

  additionColumns: ITableAction<ArticleModel>[] = [
    {
      button: {
        class: 'details-button',
        text: 'Details'
      },
      onClick: this.onLocalizationClick.bind(this)
    }

  ];
  columns: ColumnInfo[] = articleColumns;
  buttons: ITableAction<ArticleModel>[] = [
    { button: deleteButton, onClick: this.fetchDelete.bind(this) }
  ];
  constructor(injector: Injector,
              public articlesService: ArticlesService,
              private sanitized: DomSanitizer) {
    super(injector, SettingsKey.UsersSplitSizes);
  }

  fetchDelete(model: ArticleModel): void {
    this.articlesService.delete(model.id)
  }

  downloadJson(){}

  fetchFilter(params: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesService.filter(params);
  }

  onLocalizationClick(){
    this.tabsComponent.openTab(
      'New Tab',
      this.localizationTemplate,
      'aoa',
      true,
    );
  }
}
