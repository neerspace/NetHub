import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CoreComponentsModule } from 'neercms/core';
import { FormComponentsModule } from 'neercms/form';
import { LayoutComponentsModule } from 'neercms/layout';
import { ArticleFormComponent } from './article-form/article-form.component';
import { ArticlesTableComponent } from './articles-table/articles-table.component';

@NgModule({
  declarations: [ArticleFormComponent, ArticlesTableComponent],
  imports: [
    // Angular Core
    CommonModule,
    ReactiveFormsModule,
    // NeerCMS
    CoreComponentsModule,
    FormComponentsModule,
    LayoutComponentsModule,
  ],
  exports: [ArticleFormComponent],
})
export class ArticlesModule {}
