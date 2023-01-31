import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IModalHandlers, IModalInfo } from '../components/core/modal-confirm/types';

@Injectable({ providedIn: 'root' })
export class ModalsService {
  current: BehaviorSubject<IModalInfo> = new BehaviorSubject({} as IModalInfo);

  private deleteModal: IModalInfo = {
    title: 'Confirm Delete',
    text: 'Are you sure that you want to delete this?',
    confirmVariant: 'danger',
  };

  showConfirmDelete(handlers: IModalHandlers): void {
    console.log('showing popup <><><>');
    this.deleteModal.confirmed = handlers.confirmed;
    this.deleteModal.closed = handlers.closed;
    this.current.next(this.deleteModal);
  }
}
