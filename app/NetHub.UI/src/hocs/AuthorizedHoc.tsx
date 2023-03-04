import jwtDecode from 'jwt-decode';
import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import Layout, { Page } from '../components/Layout/Layout';
import IJwtPayload from '../types/IJwtPayload';
import { getOrRefreshAccessToken } from '../utils/JwtService';
import { JWTStorage } from '../utils/localStorageProvider';
import { useAppStore } from "../store/store";

interface IAuthorizedProps {
  children: Page,
  requireAuthorization: boolean
}


const AuthorizedHoc = ({ children: Children, requireAuthorization }: IAuthorizedProps) => {
  const login = useAppStore(state => state.login);

  const [authResult, setAuthResult] = useState<boolean | null>(null);

  useEffect(() => {
    isUserSignedIn().then(setAuthResult);
  }, [window.location.pathname]);

  // const authResult = useQuery<boolean, string>([],
  //   async () => await isUserSignedIn());

  async function isUserSignedIn(): Promise<boolean> {
    const accessToken = await getOrRefreshAccessToken();
    if (accessToken) {
      const jwt = jwtDecode<IJwtPayload>(JWTStorage.getAccessToken()!);
      login({
        username: jwt.username,
        profilePhotoUrl: jwt.image,
        firstName: jwt.firstname,
        lastName: null
      });
      return true;
    }

    return false;
  }

  return <Children.Provider>
    {authResult === null
      ? <Layout>
        <></>
      </Layout>
      : authResult ?
        <Children />
        // doesn't signed in
        : requireAuthorization
          ? <Navigate to={'/login'} />
          : <Children />
    }
  </Children.Provider>;
};
export default AuthorizedHoc;
