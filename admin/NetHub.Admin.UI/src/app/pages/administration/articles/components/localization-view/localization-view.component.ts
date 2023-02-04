import {Component, Input} from '@angular/core';
import {ArticleLocalizationModel} from "../../../../../api";

@Component({
  selector: 'app-localization-view',
  templateUrl: './localization-view.component.html',
  styleUrls: ['./localization-view.component.scss']
})
export class LocalizationViewComponent {
  @Input() localization!: ArticleLocalizationModel;
}
