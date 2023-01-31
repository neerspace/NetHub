import React, {createContext, FC, PropsWithChildren, useContext, useMemo, useState} from 'react';
import {useQuery, UseQueryResult} from "react-query";
import {ApiError} from "../../types/ApiError";
import IUserInfoResponse from "../../types/api/User/IUserInfoResponse";
import {getUserDashboard, getUserInfo} from "./ProfileSpace.functions";
import IDashboardResponse from "../../types/api/Dashboard/IDashboardResponse";
import {useParams} from "react-router-dom";
import IUpdateProfileRequest from "../../types/api/Profile/IUpdateProfileRequest";
import {QueryClientConstants} from "../../constants/queryClientConstants";
import {useAppStore} from "../../store/config";

export type ProfileChangesType = 'profile' | 'photo' | 'username';

export interface ExtendedRequest extends IUpdateProfileRequest {
  username: string,
  image: string | File,
  email: string
}

type ContextType = {
  userAccessor: UseQueryResult<IUserInfoResponse, ApiError>,
  dashboardAccessor: UseQueryResult<IDashboardResponse, ApiError>,
  changeRequest: ExtendedRequest,
  setChangeRequest: (request: ExtendedRequest) => void,
  changes: ProfileChangesType[],
  setChanges: (changes: ProfileChangesType[]) => void,
  addChanges: (change: ProfileChangesType) => void,
  removeChanges: (change: ProfileChangesType) => void,
}

const InitialContextValue: ContextType = {
  userAccessor: {} as UseQueryResult<IUserInfoResponse, ApiError>,
  dashboardAccessor: {} as UseQueryResult<IDashboardResponse, ApiError>,
  changeRequest: {} as ExtendedRequest,
  setChangeRequest: () => {
  },
  changes: [],
  setChanges: () => {
  },
  addChanges: () => {
  },
  removeChanges: () => {
  }
}
const ProfileContext = createContext<ContextType>(InitialContextValue);

export const useProfileContext = (): ContextType => useContext<ContextType>(ProfileContext);

const ProfileSpaceProvider: FC<PropsWithChildren> = ({children}) => {
  const {username} = useParams();

  const [request, setRequest] = useState<ExtendedRequest>({} as ExtendedRequest);
  const reduxUser = useAppStore(store => store.user);

  const userAccessor = useQuery<IUserInfoResponse, ApiError>([QueryClientConstants.user, username ?? reduxUser.username], () => getUserInfo(username), {
    onSuccess: (user) => {
      setRequest(
        {
          username: user.userName,
          email: user.email,
          image: '',
          firstName: user.firstName,
          lastName: user.lastName,
          middleName: user.middleName,
          description: user.description ?? ''
        }
      )
    },
    refetchIntervalInBackground: true
  })
  const dashboardAccessor = useQuery<IDashboardResponse, ApiError>(['dashboard', username], () => getUserDashboard(username));
  const [changes, setChanges] = useState<ProfileChangesType[]>([]);

  const handleAddChanges = (change: ProfileChangesType) => {
    if (!changes.includes(change)) setChanges(prev => [...prev, change]);
  }

  const handleRemoveChanges = (change: ProfileChangesType) => {
    setChanges(changes.filter(item => item !== change));
  }

  const value: ContextType = useMemo(() => {
    return {
      userAccessor,
      dashboardAccessor,
      changeRequest: request,
      setChangeRequest: setRequest,
      changes,
      setChanges,
      addChanges: handleAddChanges,
      removeChanges: handleRemoveChanges
    }
  }, [userAccessor, dashboardAccessor, changes, setChanges, handleAddChanges, handleRemoveChanges])

  return <ProfileContext.Provider value={value}>
    {children}
  </ProfileContext.Provider>
};

export default ProfileSpaceProvider;