import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { LocalizationsModule } from 'app/pages/administration/localizations/localizations.module';
import {
  CoreComponentsModule,
  FormComponentsModule,
  LayoutComponentsModule,
  TableComponentsModule,
} from 'neercms';
import { ArticleFormComponent } from './article-form/article-form.component';
import { ArticleTableComponent } from './article-table/article-table.component';

const routes: Routes = [{ path: '', component: ArticleTableComponent }];

@NgModule({
  declarations: [ArticleTableComponent, ArticleFormComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    AngularSplitModule,
    ReactiveFormsModule,
    LocalizationsModule,

    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
  ],
})
export class ArticlesModule {}
