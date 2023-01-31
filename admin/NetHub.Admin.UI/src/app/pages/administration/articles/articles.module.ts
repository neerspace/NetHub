import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import { ArticlesTableComponent } from './articles-table/articles-table.component';
import {LayoutComponentsModule} from "../../../components/layout/layout-components.module";
import {TableComponentsModule} from "../../../components/table/table-components.module";
import {CoreComponentsModule} from "../../../components/core/core-components.module";

const routes: Routes = [
  { path: '', component: ArticlesTableComponent },
];

@NgModule({
  declarations: [
    ArticlesTableComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
  ]
})
export class ArticlesModule {
}
