'use client'
import Link from "next/link"
import { Button } from "@/components/ui/button"
import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { useState } from "react"
import { useRouter } from 'next/navigation'

enum RegisterError {
    FirstNameError = "First name is required",
    LastNameError = "Last name is required",
    EmailError = "Email is required",
    EmailInvalid = "Email is invalid",
    PasswordError = "Password is required",
    PasswordInvalid = "Password must be at least 6 characters.",
    RegisterFail = "An error occurred during registration. Please try again later."
}

type RegisterFormModel = {
    firstName?: string,
    lastName?: string,
    email?: string,
    password?: string,
    errorMessage?: {
        firstName?: string,
        lastName?: string,
        email?: string,
        password?: string,
        register?: string,
    }
}

const Register = () => {
    const router = useRouter()
    const [registerForm, setRegisterFrom] = useState<RegisterFormModel>({})
    const validateFirstName = (): boolean => {
        let firstNameError = "";
        if (!registerForm.firstName) {
            firstNameError = RegisterError.FirstNameError
        }
        setRegisterFrom((registerForm) => ({
            ...registerForm,
            errorMessage: {
                ...registerForm.errorMessage,
                firstName: firstNameError
            }
        }))
        return !registerForm.errorMessage?.firstName;
    }
    const validateLastName = (): boolean => {
        let lastNameError = "";
        if (!registerForm.lastName) {
            lastNameError = RegisterError.LastNameError
        }
        setRegisterFrom((registerForm) => ({
            ...registerForm,
            errorMessage: {
                ...registerForm.errorMessage,
                lastName: lastNameError
            }
        }))
        return !registerForm.errorMessage?.lastName;
    }
    const validateEmail = (): boolean => {
        let emailError = "";
        if (!registerForm.email) {
            emailError = RegisterError.EmailError;
        }
        else if (!/\S+@\S+\.\S+/.test(registerForm.email)) {
            emailError = RegisterError.EmailInvalid;
        }
        setRegisterFrom((registerForm) => ({
            ...registerForm,
            errorMessage: {
                ...registerForm.errorMessage,
                email: emailError
            }
        }))
        return !registerForm.errorMessage?.email;
    }
    const validatePassword = (): boolean => {
        let passwordError = "";
        if (!registerForm.password) {

            passwordError = RegisterError.PasswordError;
        }
        else if (registerForm.password.length < 6) {
            passwordError = RegisterError.PasswordInvalid;
        }
        setRegisterFrom((registerForm) => ({
            ...registerForm,
            errorMessage: {
                ...registerForm.errorMessage,
                password: passwordError
            }
        }))
        return !registerForm.errorMessage?.password;
    }
    const handleRegister = async () => {
        const validate = {
            isValidateFirstName: validateFirstName(),
            isValidateLastName: validateLastName(),
            isValidateEmail: validateEmail(),
            isValidatePassword: validatePassword(),
        }
        if (!validate.isValidateLastName || !validate.isValidateFirstName || !validate.isValidatePassword || !validate.isValidateEmail) {
            return;
        }
        const email = registerForm.email;
        const password = registerForm.password;
        const response = await fetch('http://localhost:5274/register', {
            method: 'POST',
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password }),
        })
        if (response.ok) {
            const loginRespone = await login(email, password)
            if(loginRespone){
            router.push("/");
            }
        }
        else {
            //InvalidEmail
            //PasswordRequiresNonAlphanumeric
            //PasswordRequiresDigit
            //PasswordRequiresUpper
            const error = await response.json();
            const passwordError = Object.values(error.errors).flat().join(" ");
            console.log(passwordError)
            setRegisterFrom((registerForm) => ({
                ...registerForm,
                errorMessage: {
                    ...registerForm.errorMessage,
                    register: RegisterError.RegisterFail,
                    email: error.errors.InvalidEmail,
                    password: passwordError,
                }
            }))
        }
    }

    const validateMsError = (error?: string) => error && <p className="text-red-500">{error}</p>
    const hoverInputError = (error?: string) => `border ${error ? 'border-red-500' : ''}`;

    return (
        <div className="flex items-center">
            <Card className="mx-auto">
                <CardHeader>
                    <CardTitle className="text-xl">Sign Up</CardTitle>
                    <CardDescription>
                        Enter your information to create an account
                    </CardDescription>
                    {validateMsError(registerForm.errorMessage?.register)}
                </CardHeader>
                <CardContent>
                    <div className="grid gap-4">
                        <div className="grid grid-cols-2 gap-4">
                            <div className="grid gap-2">
                                <Label htmlFor="first-name">First name</Label>
                                <Input id="first-name" placeholder="Max" required value={registerForm.firstName} onChange={(e) =>
                                    setRegisterFrom({
                                        ...registerForm,
                                        firstName: e.target.value
                                    })}
                                    onBlur={validateFirstName}
                                    className={hoverInputError(registerForm.errorMessage?.firstName)}
                                />
                                {validateMsError(registerForm.errorMessage?.firstName)}
                            </div>
                            <div className="grid gap-2">
                                <Label htmlFor="last-name">Last name</Label>
                                <Input id="last-name" placeholder="Robinson" required value={registerForm.lastName}
                                    onChange={(e) =>
                                        setRegisterFrom({
                                            ...registerForm,
                                            lastName: e.target.value
                                        })}
                                    onBlur={validateLastName}
                                    className={hoverInputError(registerForm.errorMessage?.lastName)}
                                />
                                {validateMsError(registerForm.errorMessage?.lastName)}
                            </div>
                        </div>
                        <div className="grid gap-2">
                            <Label htmlFor="email">Email</Label>
                            <Input
                                id="email"
                                type="email"
                                placeholder="m@example.com"
                                required
                                value={registerForm.email}
                                onChange={(e) =>
                                    setRegisterFrom({
                                        ...registerForm,
                                        email: e.target.value
                                    })}
                                onBlur={validateEmail}
                                className={hoverInputError(registerForm.errorMessage?.email)}
                            />
                            {validateMsError(registerForm.errorMessage?.email)}

                        </div>
                        <div className="grid gap-2">
                            <Label htmlFor="password">Password</Label>
                            <Input id="password" type="password"
                                value={registerForm.password}
                                onChange={(e) =>
                                    setRegisterFrom({
                                        ...registerForm,
                                        password: e.target.value
                                    })}
                                onBlur={validatePassword}
                                className={hoverInputError(registerForm.errorMessage?.password)}
                            />
                            {validateMsError(registerForm.errorMessage?.password)}

                        </div>
                        <Button type="submit" className="w-full"
                            onClick={handleRegister}
                        >
                            Create an account
                        </Button>
                        <Button variant="outline" className="w-full">
                            Sign up with GitHub
                        </Button>
                    </div>
                    <div className="mt-4 text-center text-sm">
                        Already have an account?{" "}
                        <Link href="login" className="underline">
                            Sign in
                        </Link>
                    </div>
                </CardContent>
            </Card>
        </div>
    )
}
export default Register;