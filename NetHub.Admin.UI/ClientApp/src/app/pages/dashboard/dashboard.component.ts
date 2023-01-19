import { Component } from '@angular/core';
import { SecuredStorage } from '../../services/storage';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  username: string;

  constructor(storage: SecuredStorage) {
    this.username = storage.userName!;
  }
}
