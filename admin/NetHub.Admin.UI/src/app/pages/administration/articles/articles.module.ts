import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { LocalizationsModule } from 'app/pages/administration/localizations/localizations.module';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { TableComponentsModule } from 'neercms/table';
import { ArticleFormComponent } from './article-form/article-form.component';
import { ArticleTableComponent } from './article-table/article-table.component';

const routes: Routes = [{ path: '', component: ArticleTableComponent }];

@NgModule({
  declarations: [ArticleTableComponent, ArticleFormComponent],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    LocalizationsModule,
    // Libs
    AngularSplitModule,
    // NeerCMS
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
  ],
})
export class ArticlesModule {}
