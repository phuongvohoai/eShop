'use client'

import { getCurrentUser, UserSession } from '@/components/login/login-api';
import { createContext, useContext, useEffect, useState } from 'react'

interface AuthState {
  email: string;
  accessToken: string;
}

const UserContext = createContext<[AuthState, React.Dispatch<React.SetStateAction<AuthState>>]>([
  { email: "", accessToken: "" },
  () => {}
]);

export default function UserProvider({
  children,
}: {
  children: React.ReactNode
}) {
  const [auth, setAuth] = useState({
    email: "",
    accessToken: ""
  })

  useEffect(() => {
    const getUserSession = async () => {
      const getSession = await getCurrentUser();
      if (getSession?.accessToken) {
        setAuth({
          ...auth,
          email: getSession.email,
          accessToken: getSession.accessToken
        })
      }
    };
    getUserSession();
  }, [])

  return <UserContext.Provider value={[auth, setAuth]}>{children}</UserContext.Provider>
}

export const useAuth = () => useContext(UserContext)
