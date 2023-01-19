import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormReady } from 'src/app/components/form/types';
import { getRandomInt } from 'src/app/shared/utilities';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
})
export class UserFormComponent implements OnInit {
  readonly id: number;
  readonly isCreating: boolean;
  ready: FormReady = null;

  constructor(
    route: ActivatedRoute,
    private router: Router,
    public usersService: UserService, //
  ) {
    const routeId = route.snapshot.params['id'];
    this.isCreating = routeId === 'create';
    this.id = this.isCreating ? -1 : +routeId;
  }

  ngOnInit(): void {
    this.usersService.onReadyChanges.subscribe(x => (this.ready = x));
    if (this.isCreating) {
      this.usersService.form.ready.setValue('ready');
    } else {
      this.usersService.getById(this.id);
    }
  }

  backToTable(): void {
    this.router.navigateByUrl('/users');
  }

  submit(): void {
    if (this.isCreating) {
      this.usersService.create();
    } else {
      this.usersService.update(this.id);
    }
  }

  setRandomPassword() {
    const password = this.generatePassword();
    this.usersService.form.get('password')?.setValue(password);
  }

  private generatePassword() {
    const length = getRandomInt(8, 12);
    const charset = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let result = '';
    for (let i = 0; i < length; ++i) {
      result += charset.charAt(Math.floor(Math.random() * charset.length));
    }
    return result;
  }
}
