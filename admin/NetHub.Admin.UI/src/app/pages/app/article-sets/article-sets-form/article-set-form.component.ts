import { Component, Input } from '@angular/core';
import { ArticleModel } from 'app/api';

@Component({
  selector: 'app-article-set-form',
  templateUrl: './article-set-form.component.html',
  styleUrls: ['./article-set-form.component.scss'],
})
export class ArticleSetFormComponent {
  @Input() article!: ArticleModel;
}
