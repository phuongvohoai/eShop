"use client"
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
import { use, useEffect, useState } from "react"
import { useRouter } from 'next/navigation'
import { login } from "./login-api"
import { error } from "console"

type LoginFormModel = {
    email?: string,
    emailError?: string,
    password?: string,
    passwordError?: string,
    loginError?: string
}

const Login = () => {
    const router = useRouter();
    const [loginForm, setLoginForm] = useState<LoginFormModel>({});
    const validatePassword = (): boolean => {
        let passwordError = "";
        if (!loginForm.password) {
            passwordError = 'Password is required.'
        }
        else if (loginForm.password.length < 6) {
            passwordError = 'Password must be at least 6 characters.'
        }
        //Update state object nhieu lan nen su dung callback.
        setLoginForm((loginForm) => ({
            ...loginForm,
            passwordError: passwordError
        }))

        return !passwordError;
    }
    const validateEmail = (): boolean => {
        let emailError = "";
        if (!loginForm.email) {
            emailError = 'Email is required.'
        }
        else if (!/\S+@\S+\.\S+/.test(loginForm.email)) {
            emailError = 'Email is invalid.'
        }
        //Update state object nhieu lan nen su dung callback.
        setLoginForm((loginForm) => ({
            ...loginForm,
            emailError: emailError
        }))
        return !emailError;
    }

    const handleLogin = async () => {
        const isEmailValid = validateEmail();
        const isPasswordValid = validatePassword();
        if (!isPasswordValid || !isEmailValid) {
            return;
        }

        const loginSuccess = await login(loginForm.email!, loginForm.password!);
        if (loginSuccess) {
            router.push('/');
        }
        else {
            setLoginForm({
                ...loginForm,
                loginError: 'Login Failed!, Please check your email and password and try again.'
            })
        }
    }

    const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
          e.preventDefault();
          handleLogin();
        }}


    const errorMessage = (error?: string) => error && <p className="text-red-500">{error}</p>;
    const getInputClassName = (error?: string) => `border ${error ? 'border-red-500' : ''}`;

    return (
        <div className="flex items-center">
            <Card className="mx-auto">
                <CardHeader>
                    <CardTitle className="text-2xl">Login</CardTitle>
                    <CardDescription>
                        Enter your email below to login to your account
                    </CardDescription>
                    {errorMessage(loginForm.loginError)}
                </CardHeader>
                <CardContent>
                    <div className="grid gap-4">
                        <div className="grid gap-2">
                            <Label htmlFor="email">Email</Label>
                            <Input
                                id="email"
                                type="email"
                                placeholder="m@example.com"
                                required
                                value={loginForm.email}
                                onChange={(e) => setLoginForm({
                                    ...loginForm,
                                    email: e.target.value
                                })}
                                onBlur={validateEmail}
                                className={getInputClassName(loginForm.emailError)}
                                onKeyDown={handleKeyDown}
                            />
                            {errorMessage(loginForm.emailError)}
                        </div>
                        <div className="grid gap-2">
                            <div className="flex items-center">
                                <Label htmlFor="password">Password</Label>
                                <Link href="#" className="ml-auto inline-block text-sm underline">
                                    Forgot your password?
                                </Link>
                            </div>
                            <Input id="password" type="password" required
                                value={loginForm.password}
                                onChange={(e) => setLoginForm({
                                    ...loginForm,
                                    password: e.target.value
                                })}
                                onBlur={validatePassword}
                                className={getInputClassName(loginForm.passwordError)}
                                onKeyDown={handleKeyDown}
                            />
                            {errorMessage(loginForm.passwordError)}
                        </div>
                        <Button type="submit" className="w-full" onClick={handleLogin}>
                            Login
                        </Button>
                        <Button variant="outline" className="w-full">
                            Login with Google
                        </Button>
                    </div>
                    <div className="mt-4 text-center text-sm">
                        Don&apos;t have an account?{" "}
                        <Link href="register" className="underline">
                            Sign up
                        </Link>
                    </div>
                </CardContent>
            </Card>
        </div>
    )
}
export default Login;

