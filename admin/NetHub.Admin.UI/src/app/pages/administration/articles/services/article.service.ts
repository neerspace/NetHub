import {IFiltered, IFilterInfo} from "../../../../components/table/types";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {FormGroupReady, FormId, FormReady} from "../../../../components/form/types";
import {Router} from "@angular/router";
import {ArticleLocalizationModel, ArticleModel, ArticlesApi, ErrorDto} from "../../../../api";
import {FormBuilder} from "@angular/forms";
import {LoaderService, ToasterService} from "../../../../services/viewport";
import {ModalsService} from "../../../../services/modals.service";

@Injectable({providedIn: 'root'})
export class ArticlesService {
  readonly form: FormGroupReady;

  constructor(
    formBuilder: FormBuilder,
    private router: Router,
    private articlesApi: ArticlesApi,
    private modals: ModalsService,
    private loader: LoaderService,
    private toaster: ToasterService
    ) {
    this.form = formBuilder.group({
      ready: [null as FormReady, []],
      id: ['', []],
      name: ['', []],
      permissions: [[], []],
    }) as any;
  }

  public lastFormId?: FormId;

  showForm(): void {
    this.router.navigate(['articles', this.lastFormId]);
  }

  getByIdAndSetToForm(id: number): void {
    this.onRequestStart();
    this.articlesApi.getById(id).subscribe({
      next: (article: ArticleModel | any) => {
        article.ready = true;
        this.form.setValue(article);
        this.onRequestSuccess();
        this.lastFormId = id;
      },
      error: (error: ErrorDto) => {
        this.onRequestError(error);
        this.lastFormId = undefined;
      },
    });
  }

  getById(id: number): Observable<ArticleModel>{
    return this.articlesApi.getById(id);
  }

  filter(request: IFilterInfo): Observable<IFiltered<ArticleModel>> {
    return this.articlesApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  }

  getLocalizations(id: number): Observable<ArticleLocalizationModel[]>{
    return this.articlesApi.getByArticleId(id);
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
        this.articlesApi.delete(id).subscribe({
          next: () => {
            this.toaster.showSuccess('Article successfully deleted');
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
    if (this.form.ready.value === 'ready') {
      this.form.ready.setValue('loading');
    }
  }

  private onRequestSuccess() {
    // console.log('request succeed');
    this.loader.hide();
    this.form.ready.setValue('ready');
  }

  private onRequestError(error: ErrorDto) {
    // console.log('request error');
    this.loader.hide();
    this.form.ready.setValue('404');
    this.toaster.showFail(error.message);
  }
}