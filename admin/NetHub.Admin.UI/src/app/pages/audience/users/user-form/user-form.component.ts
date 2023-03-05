import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { getRandomInt } from 'src/app/shared/utilities';
import { UserService } from '../user.service';

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
    public readonly usersService: UserService,
  ) {
    const routeId = route.snapshot.params['id'];
    this.isCreating = routeId === 'create';
    this.id = this.isCreating ? -1 : +routeId;
  }

  ngOnInit(): void {
    this.usersService.init(this.isCreating);

    if (!this.isCreating) {
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
