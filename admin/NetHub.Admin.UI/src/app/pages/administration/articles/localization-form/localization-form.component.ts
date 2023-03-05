import { Component, Input } from '@angular/core';
import { ArticleLocalizationModel } from 'src/app/api';
import { FormReady } from '../../../../components/form/types';
import { LocalizationsService } from '../localization.service';

@Component({
  selector: 'app-localization-form',
  templateUrl: './localization-form.component.html',
  styleUrls: ['./localization-form.component.scss'],
})
export class LocalizationFormComponent {
  @Input() model!: ArticleLocalizationModel;

  ready: FormReady = null;

  constructor(public readonly localizationsService: LocalizationsService) {}

  submit() {}
}
