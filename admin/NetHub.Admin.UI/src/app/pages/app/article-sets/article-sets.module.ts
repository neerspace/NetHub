import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { ArticlesModule } from 'app/pages/app/articles/articles.module';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { TableComponentsModule } from 'neercms/table';
import { ArticleSetFormComponent } from './article-sets-form/article-set-form.component';
import { ArticleSetsTableComponent } from './article-sets-table/article-sets-table.component';

const routes: Routes = [{ path: '', component: ArticleSetsTableComponent }];

@NgModule({
  declarations: [ArticleSetsTableComponent, ArticleSetFormComponent],
  imports: [
    // Angular Core
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    // Libs
    AngularSplitModule,
    // NeerCMS
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    FormComponentsModule,
    // App
    ArticlesModule,
  ],
})
export class ArticleSetsModule {}
