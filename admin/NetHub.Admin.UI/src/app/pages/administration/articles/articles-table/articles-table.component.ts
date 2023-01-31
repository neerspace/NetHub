import {Component, OnInit} from '@angular/core';
import {ColumnInfo, IFiltered, IFilterInfo, ITableAction} from "../../../../components/table/types";
import {LanguageModel, RoleModel} from "../../../../api";
import {Observable} from "rxjs";
import {deleteButton} from "../../../../components/table/buttons";
import {LanguageService} from "../../languages/language.service";
import {ArticlesService} from "../services/ArticlesService";

@Component({
  selector: 'app-articles-table',
  templateUrl: './articles-table.component.html',
  styleUrls: ['./articles-table.component.scss']
})
export class ArticlesTableComponent {

  columns: ColumnInfo[] = [];
  buttons: ITableAction<ArticleModel>[] = [
    { button: deleteButton, onClick: this.fetchDelete.bind(this) }
  ];
  constructor(public articlesService: ArticlesService) { }

  fetchDelete(model: ArticleModel): void {
  }

  downloadJson(){}

  // fetchFilter(params: IFilterInfo): Observable<IFiltered<RoleModel>> {
    // return this.roleService.filter(params);
  // }
}


class ArticleModel{
  hello!: number;
}