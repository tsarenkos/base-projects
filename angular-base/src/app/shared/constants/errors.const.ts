export class ErrorTitles {
    public static emailInvalid = 'emailInvalid';
    public static required = 'required';
    public static minlength = 'minlength';
    public static invalidCredentials = 'invalidCredentials';
}

export class ErrorMessages {
    public static fieldRequired = (field: string) => `'${field}' field is required`;
    public static emailInvalid = 'Email address is invalid';
    public static passwordMinlength = `'Password' must contain at least 8 characters`;
    public static invalidCredentials = 'Your credentials are incorrect. Please try again';
}