import { Component, Input } from '@angular/core';
import { ArticleLocalizationModel } from 'src/app/api';

@Component({
  selector: 'app-localization-form',
  templateUrl: './localization-form.component.html',
  styleUrls: ['./localization-form.component.scss'],
})
export class LocalizationFormComponent {
  @Input() localization!: ArticleLocalizationModel;
}
