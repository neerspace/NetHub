import {IFiltered, IFilterInfo} from "../../../../components/table/types";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {FormId} from "../../../../components/form/types";
import {Router} from "@angular/router";

@Injectable({providedIn: 'root'})
export class ArticlesService{

  constructor(private router: Router) {
  }
  public lastFormId?: FormId;

  // filter(request: IFilterInfo): Observable<IFiltered<{}>>
  // {
  //   const promise = new Promise(() => {});
  //   const filtered = new
    // return this.rolesApi.filter(request.filters, request.sorts, request.page, request.pageSize);
  // }

  showForm(): void {
    this.router.navigate(['articles', this.lastFormId]);
  }
}