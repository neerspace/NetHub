import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { capitalizeFirstLetter } from 'app/shared/utilities';
import { LanguageService } from 'app/pages/administration/languages/language.service';

// TODO: Add validation if language is already exists

@Component({
  selector: 'app-langs-form',
  templateUrl: './langs-form.component.html',
  styleUrls: ['./langs-form.component.scss'],
})
export class LangsFormComponent implements OnInit {
  readonly code: string;
  readonly isCreating: boolean;
  suggestedLanguageName?: string;
  languageNames: Intl.DisplayNames;

  constructor(
    route: ActivatedRoute,
    private router: Router,
    public languageService: LanguageService,
  ) {
    const routeCode = route.snapshot.params['code'];
    this.isCreating = routeCode === 'create';
    this.code = this.isCreating ? '' : routeCode;
    this.languageNames = new Intl.DisplayNames(['en'], { type: 'language' });
  }

  ngOnInit(): void {
    this.languageService.init(this.isCreating);

    if (this.isCreating) {
      // Add listener on 'code' changes
      this.languageService.form
        .get('code')!
        .valueChanges.pipe(debounceTime(300))
        .pipe(distinctUntilChanged())
        .subscribe(code => {
          this.tryGetLanguageNameByCode(code);
        });
    } else {
      this.languageService.getByCode(this.code);
    }
  }

  backToTable(): void {
    this.router.navigateByUrl('/languages');
  }

  submit(): void {
    if (this.isCreating) {
      this.languageService.create();
    } else {
      this.languageService.update(this.code);
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
    const code = this.languageService.form.get('code')!.value!;
    const localLanguageNames = new Intl.DisplayNames([code], { type: 'language' });
    this.languageService.form.patchValue({
      name: this.suggestedLanguageName,
      nameLocal: capitalizeFirstLetter(localLanguageNames.of(code)),
    });
  }

  handleFileChange(event: Event) {
    const inputTarget = event.target as HTMLInputElement;
    const file = inputTarget.files![0];
    if (!file) {
      return;
    }

    this.languageService.flagFile = file;
    const reader = new FileReader();

    reader.onload = e => {
      this.languageService.form.get('flagUrl')!.setValue(e.target!.result as string);
    };

    reader.readAsDataURL(file);
  }
}
