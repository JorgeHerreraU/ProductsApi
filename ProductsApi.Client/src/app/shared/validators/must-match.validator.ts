import { AbstractControl, ValidationErrors } from '@angular/forms';

export function MustMatch(controlName: string, matchingControlName: string) {
    return (group: AbstractControl): ValidationErrors | null => {
        const control = group.get(controlName);
        const matchingControl = group.get(matchingControlName);
        if (matchingControl?.errors && !matchingControl.errors['mustMatch']) {
            return null;
        }
        if (control?.value !== matchingControl?.value) {
            matchingControl?.setErrors({ mustMatch: true });
            return ({ mustMatch: true });
        } else {
            matchingControl?.setErrors(null);
        }
        return null;
    };
}
