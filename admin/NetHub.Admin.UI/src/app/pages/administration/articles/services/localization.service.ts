import {Injectable} from "@angular/core";
import {FormBuilder} from "@angular/forms";
import {Router} from "@angular/router";
import {ArticleLocalizationModel, ErrorDto, LocalizationsApi} from "../../../../api";
import {ModalsService} from "../../../../services/modals.service";
import {LoaderService, ToasterService} from "../../../../services/viewport";

@Injectable({providedIn: 'root'})
export class LocalizationsService{
  constructor(
    formBuilder: FormBuilder,
    private router: Router,
    private localizationsApi: LocalizationsApi,
    private modals: ModalsService,
    private loader: LoaderService,
    private toaster: ToasterService
  ) {}


  getById(id: number): void {
    this.onRequestStart();
    this.localizationsApi.getById(id).subscribe({
      next: (localization: ArticleLocalizationModel | any) => {
        localization.ready = true;
        this.onRequestSuccess();
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
      },
    });
  }

  // update(id: number): void {
  //   const request = this.form.value as ArticleUpdateRequest;
  //   request.id = id;
  //   this.onRequestStart();
  //   this.articlesApi.update(request).subscribe({
  //     next: () => {
  //       this.toaster.showSuccess('User successfully updated');
  //       this.onRequestSuccess();
  //     },
  //     error: (error: ErrorDto) => {
  //       this.onRequestError(error);
  //     },
  //   });
  // }

  delete(id: number): void {
    this.modals.showConfirmDelete({
      confirmed: () => {
        this.onRequestStart();
        this.localizationsApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('Localization successfully deleted');
            this.router.navigateByUrl('articles');
            this.onRequestSuccess();
          },
          error: (error: ErrorDto) => {
            this.onRequestError(error);
          },
        });
      },
    });
  }

  private onRequestStart() {
    // console.log('request started');
    this.loader.show();
  }

  private onRequestSuccess() {
    // console.log('request succeed');
    this.loader.hide();
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.loader.hide();
    this.toaster.showFail(error.message);
  }
}