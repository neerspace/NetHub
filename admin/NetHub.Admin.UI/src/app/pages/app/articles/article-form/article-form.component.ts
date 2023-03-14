import { Component, Input, OnInit } from '@angular/core';
import { ArticleLocalizationModel, ContentStatus } from 'app/api';
import { ArticleService } from 'app/pages/app/articles/article.service';
import { IModalHandlers, ModalsService } from 'neercms/services/viewport';

@Component({
  selector: 'app-article-form',
  templateUrl: './article-form.component.html',
  styleUrls: ['./article-form.component.scss'],
  providers: [ArticleService],
})
export class ArticleFormComponent implements OnInit {
  @Input() model!: ArticleLocalizationModel;

  locked: boolean = true;
  Status = ContentStatus;

  constructor(
    public readonly localizationsService: ArticleService,
    private readonly modals: ModalsService,
  ) {}

  ngOnInit(): void {
    this.localizationsService.loadMetadata();
    this.localizationsService.form.setValue(this.model);
  }

  submit() {
    this.localizationsService.update();
    this.model = this.localizationsService.form.getRawValue();
  }

  onHotButton(newStatus: ContentStatus) {
    const modelName = 'Article Localization';
    const handlers: IModalHandlers = {
      confirmed: res => {
        console.log('reason::::::::', res?.text);
        this.localizationsService.form.get('status')?.setValue(newStatus);
        this.localizationsService.form.get('banReason')?.setValue(res?.text);
        this.submit();
      },
    };

    if (newStatus === ContentStatus.Banned) {
      this.modals.showConfirmBan(modelName, handlers);
    } else if (newStatus === ContentStatus.Draft) {
      this.modals.showConfirmLiftBan(modelName, handlers);
    } else if (newStatus === ContentStatus.Published) {
      this.modals.showConfirmPublish(modelName, handlers);
    } else {
      throw new Error(`Invalid new status passed: '${newStatus}'`);
    }
  }
}
