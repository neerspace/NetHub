import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { CoreComponentsModule } from 'app/components/core/core-components.module';
import { FormComponentsModule } from 'app/components/form/form-components.module';
import { LayoutComponentsModule } from 'app/components/layout/layout-components.module';
import { TableComponentsModule } from 'app/components/table/table-components.module';
import { LocalizationsModule } from 'app/pages/administration/localizations/localizations.module';
import { ArticleFormComponent } from './article-form/article-form.component';
import { ArticleTableComponent } from './article-table/article-table.component';

const routes: Routes = [{ path: '', component: ArticleTableComponent }];

@NgModule({
  declarations: [ArticleTableComponent, ArticleFormComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    AngularSplitModule,
    FormComponentsModule,
    ReactiveFormsModule,
    LocalizationsModule,
  ],
})
export class ArticlesModule {}
