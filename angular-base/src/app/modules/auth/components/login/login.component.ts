import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthFacade } from '../../../../core/facades/auth/auth-facade.service';
import { CustomValidators } from '../../../../shared/validators/custom-validators';
import { ErrorTitles, ErrorMessages } from '../../../../shared/constants/errors.const';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  form: FormGroup;
  isHidden: boolean = true;
  ErrorTitles = ErrorTitles;
  ErrorMessages = ErrorMessages;

  constructor(private fb: FormBuilder, private authFacade: AuthFacade, private router: Router,) {
    this.form = this.fb.group({
      email: ['', {
        asyncValidators: [CustomValidators.required, CustomValidators.email]
      }],
      password: ['', {
        asyncValidators: [CustomValidators.required]
      }]
    })
  }

  public submit() {
    this.authFacade.login(this.form.value.email, this.form.value.password)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/');
        },
        error: (err: any) => {
          if (this.isInvalidCredentials(err)) {
            this.form.controls['password'].setErrors({
              invalidCredentials: true
            });
          }
        }
      });
  }

  public containsError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].touched && this.form.controls[controlName].hasError(errorName);
  }

  private isInvalidCredentials = (err: any) => {
    return err?.error?.error === 'invalid_grant';
  }
}