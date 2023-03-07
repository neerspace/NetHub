import { Component, Input, OnInit } from '@angular/core';
import { ArticleLocalizationModel } from 'src/app/api';
import { LocalizationService } from '../localization.service';

@Component({
  selector: 'app-localization-form',
  templateUrl: './localization-form.component.html',
  styleUrls: ['./localization-form.component.scss'],
  providers: [LocalizationService],
})
export class LocalizationFormComponent implements OnInit {
  @Input() model!: ArticleLocalizationModel;

  locked: boolean = true;

  constructor(public readonly localizationsService: LocalizationService) {}

  ngOnInit(): void {
    this.localizationsService.form.setValue(this.model);
    this.localizationsService.setReady('ready');
  }

  submit() {}
}
