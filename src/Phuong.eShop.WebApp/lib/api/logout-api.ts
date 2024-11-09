'use server'
import { UserSession } from '@/components/login/login-api';
import { cookies } from 'next/headers'

const userSessionCookieName = "userSession"; 
 
export async function deleteCurrentUser() {
  (await cookies()).delete(userSessionCookieName)
}