import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AngularSplitModule } from 'angular-split';
import { CoreComponentsModule } from 'src/app/components/core/core-components.module';
import { LayoutComponentsModule } from 'src/app/components/layout/layout-components.module';
import { TableComponentsModule } from 'src/app/components/table/table-components.module';
import { FormComponentsModule } from '../../../components/form/form-components.module';
import { ArticleFormComponent } from './article-form/article-form.component';
import { ArticlesService } from './article.service';
import { ArticlesTableComponent } from './articles-table/articles-table.component';
import { LocalizationFormComponent } from './localization-form/localization-form.component';
import { LocalizationsService } from './localization.service';

const routes: Routes = [{ path: '', component: ArticlesTableComponent }];

@NgModule({
  providers: [ArticlesService, LocalizationsService],
  declarations: [ArticlesTableComponent, LocalizationFormComponent, ArticleFormComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutComponentsModule,
    TableComponentsModule,
    CoreComponentsModule,
    AngularSplitModule,
    FormComponentsModule,
  ],
})
export class ArticlesModule {}
