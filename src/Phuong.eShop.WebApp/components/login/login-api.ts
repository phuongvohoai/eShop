'use server'
import { useAuth } from '@/app/context/user-context-provider'
import { cookies } from 'next/headers'

type AccessTokenResponse = {
    accessToken: string
}

export type UserSession = {
    email: string,
    accessToken: string
}

const userSessionCookieName = "userSession";

export async function login(email: string, password: string) {
    const cookieStore = await cookies();
    const response = await fetch('http://localhost:5274/login', {
        method: 'POST',
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ email, password }),
    });

    if(response.ok){
    const accessTokenResponse: AccessTokenResponse = await response.json();
    const user: UserSession = {
        email: email,
        accessToken: accessTokenResponse.accessToken
    }
    console.log(user);
    cookieStore.set(userSessionCookieName, JSON.stringify(user));
    return user;
    }
    return false;
}

export async function getCurrentUser(): Promise<UserSession | null> {
    const cookieStore = await cookies();
    const userSessionStr = cookieStore.get(userSessionCookieName);
    if (!userSessionStr || !userSessionStr.value) {
        return null;
    }

    return JSON.parse(userSessionStr.value) as UserSession;
}
