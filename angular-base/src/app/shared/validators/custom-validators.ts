import { AbstractControl, FormControl, ValidationErrors } from "@angular/forms";
import { Observable, of } from "rxjs";

export const EMAIL_REGEXP = /^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

export class CustomValidators {

    static required(control: AbstractControl): Observable<ValidationErrors | null> {
        if (!control.value
            || !control.value.toString().replace(/\s/g, '')) {
            return of({ required: true });
        }

        return of(null);
    }

    static email(control: AbstractControl): Observable<ValidationErrors | null> {
        if (!control.value) {
            return of(null);
        }

        return EMAIL_REGEXP.test(control.value) ? of(null) : of({ emailInvalid: true });
    }
}
