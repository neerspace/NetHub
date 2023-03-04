import React, { createContext, FC, PropsWithChildren, useContext, useMemo, useState } from 'react';
import { useQuery, UseQueryResult } from "react-query";
import { ApiError } from "../../types/ApiError";
import { getUserDashboard, getUserInfo } from "./ProfileSpace.functions";
import { useParams } from "react-router-dom";
import IUpdateProfileRequest from "../../types/api/Profile/IUpdateProfileRequest";
import { useAppStore } from "../../store/config";
import { DashboardResult, PrivateUserResult, UserResult } from "../../api/_api";
import { QueryClientKeysHelper } from "../../utils/QueryClientKeysHelper";

export type ProfileChangesType = 'profile' | 'photo' | 'username';

export interface ExtendedRequest extends IUpdateProfileRequest {
  username: string,
  image: string | File,
  email: string
}

type ContextType = {
  userAccessor: UseQueryResult<UserResult | PrivateUserResult, ApiError>,
  dashboardAccessor: UseQueryResult<DashboardResult, ApiError>,
  changeRequest: ExtendedRequest,
  setChangeRequest: (request: ExtendedRequest) => void,
  changes: ProfileChangesType[],
  setChanges: (changes: ProfileChangesType[]) => void,
  addChanges: (change: ProfileChangesType) => void,
  removeChanges: (change: ProfileChangesType) => void,
}

const InitialContextValue: ContextType = {
  userAccessor: {} as UseQueryResult<UserResult | PrivateUserResult, ApiError>,
  dashboardAccessor: {} as UseQueryResult<DashboardResult, ApiError>,
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

  const userAccessor = useQuery<UserResult | PrivateUserResult, ApiError>(QueryClientKeysHelper.Profile(username ?? reduxUser.username),
    () => getUserInfo(setRequest, username),
  {refetchIntervalInBackground: true}
  )
  const dashboardAccessor = useQuery<DashboardResult, ApiError>(QueryClientKeysHelper.Dashboard(username), () => getUserDashboard(username));
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