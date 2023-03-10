import { Component, Input } from '@angular/core';
import { ArticleModel } from 'app/api';

@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.scss'],
})
export class ArticleFormComponent {
  @Input() article!: ArticleModel;
}
