import { ValidatorFn, AbstractControl, ValidationErrors } from "@angular/forms";

export class CustomValidators {

    static dateInPast(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            return Date.parse(control.value) > Date.now() ? {'dateInPast': {value: control.value}} : null;
        };
    }

    static isURL(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            let regexp = RegExp(`https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,}`);

            return !regexp.test(control.value) ? {'isURL': {value: control.value}} : null;
        }
    }
}
