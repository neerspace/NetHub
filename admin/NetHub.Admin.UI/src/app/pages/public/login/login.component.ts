import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRequest, JWTApi } from 'app/api';
import { UserService } from 'app/services/user.service';
import { ToasterService } from 'neercms/services/viewport';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  private readonly logout: boolean;

  constructor(
    fb: FormBuilder,
    private router: Router,
    private jwtApi: JWTApi,
    private toaster: ToasterService,
    private userService: UserService,
  ) {
    this.logout = this.router.getCurrentNavigation()?.extras.state?.['logout'] === true;
    this.form = fb.group({
      login: new FormControl('jurilents'),
      password: new FormControl('Test1234'),
    });
  }

  ngOnInit(): void {
    if (this.logout) {
      this.jwtApi.revokeToken().subscribe({
        next: () => {
          this.userService.setUserData(null);
          this.toaster.showInfo('Logout succeeded');
        },
        error: error => {
          this.toaster.showFail('Logout failed');
        },
      });
    }
  }

  submit() {
    this.jwtApi.authenticate(this.form.value as AuthRequest).subscribe({
      next: result => {
        this.userService.setUserData(result);
        this.router.navigateByUrl('');
      },
      error: error => {
        if (error.status === 400) {
          this.form.setErrors({
            login: { 'api-error': error['errors'].login },
          });
        } else {
          this.toaster.showFail('Unknown error handled: ' + (error.message || error.status));
        }
      },
    });
  }
}
