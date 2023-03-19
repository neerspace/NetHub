import jwtDecode from 'jwt-decode';
import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import IJwtPayload from '../types/IJwtPayload';
import { getOrRefreshAccessToken } from '../utils/JwtService';
import { JWTStorage } from '../utils/localStorageProvider';
import { useAppStore } from "../store/store";
import Dynamic, { IPage } from "../components/Layout/Dynamic";

interface IAuthorizedProps {
  children: IPage,
  requireAuthorization: boolean
}


const AuthorizedHoc = ({children: Children, requireAuthorization}: IAuthorizedProps) => {
  const {isLogin, login} = useAppStore(state => state);

  const [authResult, setAuthResult] = useState<boolean | null>(null);

  useEffect(() => {
    isUserSignedIn().then(setAuthResult);
  }, [window.location.pathname]);

  async function isUserSignedIn(): Promise<boolean> {
    const accessToken = await getOrRefreshAccessToken();
    //if user authorized and data doesn't set in store
    if (accessToken && !isLogin) {
      const jwt = jwtDecode<IJwtPayload>(accessToken);
      login({
        username: jwt.username,
        profilePhotoUrl: jwt.image,
        firstName: jwt.firstname,
        lastName: null
      });
    }

    return !!accessToken;
  }

  return <Children.Provider>
    {authResult === null
      ? <Dynamic>
        <></>
      </Dynamic>
      : authResult ?
        <Children/>
        // doesn't signed in
        : requireAuthorization
          ? <Navigate to={'/login'}/>
          : <Children/>
    }
  </Children.Provider>;
};
export default AuthorizedHoc;
