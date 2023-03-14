import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'app/pages/app/users/user.service';
import { getRandomInt } from 'neercms/shared/utilities';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
})
export class UserFormComponent implements OnInit {
  readonly id: number;
  readonly isCreating: boolean;

  constructor(
    route: ActivatedRoute,
    private readonly router: Router,
    public readonly userService: UserService,
  ) {
    const routeId = route.snapshot.params['id'];
    this.isCreating = routeId === 'create';
    this.id = this.isCreating ? -1 : +routeId;
  }

  ngOnInit(): void {
    this.userService.init(this.isCreating);

    if (!this.isCreating) {
      this.userService.getById(this.id);
    }
  }

  backToTable(): void {
    this.router.navigateByUrl('/users');
  }

  submit(): void {
    if (this.isCreating) {
      this.userService.create();
    } else {
      this.userService.update(this.id);
    }
  }

  setRandomPassword() {
    const password = this.generatePassword();
    this.userService.form.get('password')?.setValue(password);
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
