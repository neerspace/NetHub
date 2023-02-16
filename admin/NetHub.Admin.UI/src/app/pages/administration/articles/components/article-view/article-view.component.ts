import {Component, Input} from '@angular/core';
import {ArticleModel} from "../../../../../api";

@Component({
  selector: 'app-article-view',
  templateUrl: './article-view.component.html',
  styleUrls: ['./article-view.component.scss']
})
export class ArticleViewComponent{
  @Input() article!: ArticleModel;
}
