import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {ArticlesTableComponent} from './articles-table/articles-table.component';
import {LayoutComponentsModule} from "../../../components/layout/layout-components.module";
import {TableComponentsModule} from "../../../components/table/table-components.module";
import {CoreComponentsModule} from "../../../components/core/core-components.module";
import {AngularSplitModule} from "angular-split";
import { LocalizationViewComponent } from './components/localization-view/localization-view.component';
import { ArticleViewComponent } from './components/article-view/article-view.component';

const routes: Routes = [
  { path: '', component: ArticlesTableComponent },
];

@NgModule({
  declarations: [
    ArticlesTableComponent,
    LocalizationViewComponent,
    ArticleViewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    AngularSplitModule,
  ]
})
export class ArticlesModule {
}
