import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SignUpService } from '../../../../core/services/sign-up/sign-up.service';
import { Router } from '@angular/router';
import { AuthFacade } from '../../../../core/facades/auth/auth-facade.service';
import { switchMap } from 'rxjs';
import { CustomValidators } from '../../../../shared/validators/custom-validators';
import { ErrorTitles, ErrorMessages } from '../../../../shared/constants/errors.const';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  form: FormGroup;
  isHidden: boolean = true;
  ErrorTitles = ErrorTitles;
  ErrorMessages = ErrorMessages;

  constructor(
    private fb: FormBuilder,
    private signupService: SignUpService,
    private authFacade: AuthFacade,
    private router: Router
  ) {
    this.form = this.fb.group({
      firstName: ['', {
        asyncValidators: [CustomValidators.required]
      }],
      lastName: ['', {
        asyncValidators: [CustomValidators.required]
      }],
      email: ['', {
        asyncValidators: [CustomValidators.required, CustomValidators.email]
      }],
      password: ['', {
        validators: [Validators.minLength(8)],
        asyncValidators: [CustomValidators.required]
      }]
    })
  }

  public submit() {
    this.signupService.signUp(this.form.value)
      .pipe(
        switchMap(() => this.authFacade.login(this.form.value.email, this.form.value.password))
      )
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/');
        },
        error: (err: any) => {
          console.log(err)
        }
      });
  }

  public containsError = (controlName: string, errorName: string) => {
    return this.form.controls[controlName].touched && this.form.controls[controlName].hasError(errorName);
  }
}
