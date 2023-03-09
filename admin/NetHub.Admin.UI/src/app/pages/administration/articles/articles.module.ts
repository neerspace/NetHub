import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { CoreComponentsModule } from 'src/app/components/core/core-components.module';
import { FormComponentsModule } from 'src/app/components/form/form-components.module';
import { LayoutComponentsModule } from 'src/app/components/layout/layout-components.module';
import { TableComponentsModule } from 'src/app/components/table/table-components.module';
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
  ],
})
export class ArticlesModule {}
