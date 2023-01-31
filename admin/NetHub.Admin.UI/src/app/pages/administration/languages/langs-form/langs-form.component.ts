import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { FormReady } from 'src/app/components/form/types';
import { capitalizeFirstLetter } from 'src/app/shared/utilities';
import { LanguageService } from '../language.service';

@Component({
  selector: 'app-langs-form',
  templateUrl: './langs-form.component.html',
  styleUrls: ['./langs-form.component.scss'],
})
export class LangsFormComponent implements OnInit {
  readonly code: string;
  readonly isCreating: boolean;
  ready: FormReady = null;
  suggestedLanguageName?: string;
  languageNames: Intl.DisplayNames;

  constructor(
    route: ActivatedRoute,
    private router: Router,
    public languagesService: LanguageService,
  ) {
    const routeCode = route.snapshot.params['code'];
    this.isCreating = routeCode === 'create';
    this.code = this.isCreating ? '' : routeCode;
    this.languageNames = new Intl.DisplayNames(['en'], { type: 'language' });
  }

  ngOnInit(): void {
    this.languagesService.onReadyChanges.subscribe(x => (this.ready = x));
    if (this.isCreating) {
      this.languagesService.form.ready.setValue('ready');
      this.languagesService.form
        .get('code')!
        .valueChanges.pipe(debounceTime(300))
        .pipe(distinctUntilChanged())
        .subscribe(code => {
          this.tryGetLanguageNameByCode(code);
        });
    } else {
      this.languagesService.getByCode(this.code);
    }
  }

  backToTable(): void {
    this.router.navigateByUrl('/languages');
  }

  submit(): void {
    if (this.isCreating) {
      this.languagesService.create();
    } else {
      this.languagesService.update(this.code);
    }
  }

  tryGetLanguageNameByCode(code: string): void {
    if (code && code.length === 2) {
      try {
        this.suggestedLanguageName = this.languageNames.of(code);
        if (this.suggestedLanguageName !== code) {
          return;
        }
      } catch (e) {
        // ignore
      }
    }
    this.suggestedLanguageName = '';
  }

  applySuggestedLanguage() {
    const code = this.languagesService.form.get('code')!.value!;
    const localLanguageNames = new Intl.DisplayNames([code], { type: 'language' });
    this.languagesService.form.patchValue({
      name: this.suggestedLanguageName,
      nameLocal: capitalizeFirstLetter(localLanguageNames.of(code)),
    });
  }
}
