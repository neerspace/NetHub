import { Injector } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { FormReady, FormReadyWrapper } from '../../components/form/types';

export abstract class FormServiceBase {
  public readonly form: FormGroup;
  public readonly router: Router;
  private readonly formBuilder: FormBuilder;
  private readyWrap: FormReadyWrapper = new FormReadyWrapper();

  protected constructor(injector: Injector, controls: object) {
    this.router = injector.get(Router);
    this.formBuilder = injector.get(FormBuilder);

    this.router.routeReuseStrategy.shouldReuseRoute = () => false;

    this.form = this.formBuilder.group(controls) as any;
  }

  init(isCreating: boolean): void {}

  get ready(): FormReadyWrapper {
    return this.readyWrap;
  }

  get isReady(): boolean {
    return this.readyWrap.valueOf();
  }

  get isLoading(): boolean {
    return this.readyWrap.state === 'loading';
  }

  setReady(value: FormReady) {
    this.readyWrap.state = value;
  }
}
