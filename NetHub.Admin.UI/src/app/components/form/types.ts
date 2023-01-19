import { FormControl, FormGroup } from '@angular/forms';

export type InputType =
  | 'text'
  | 'password'
  | 'email'
  | 'phone'
  | 'number'
  | 'date'
  | 'time'
  | 'datetime'
  | 'month';

export type Size = 'small' | 'medium' | 'large';

export type NumberPattern = 'integer' | 'float' | 'float(2)' | string;

export type DateTimeMode = 'date' | 'date-range' | 'date-time' | 'time';
export type PickrMode = 'time' | 'single' | 'multiple' | 'range' | undefined;

export type FormReady = null | 'ready' | 'loading' | '404';

export type FormGroupReady = FormGroup<any> & { ready: FormControl<FormReady> };

export type FormId<T = number> = 'create' | T;
