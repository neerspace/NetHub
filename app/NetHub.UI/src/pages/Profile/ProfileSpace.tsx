import React from 'react';
import PrivateProfile from "../../components/Profile/Private/PrivateProfile";
import {Skeleton} from '@chakra-ui/react';
import {useParams} from "react-router-dom";
import PublicProfile from "../../components/Profile/Public/PublicProfile";
import ProfileSpaceProvider, {useProfileContext} from "./ProfileSpace.Provider";
import ProfileTitle from "../../components/Profile/ProfileTitle";
import ErrorBlock from "../../components/UI/Error/ErrorBlock";
import {ErrorsHandler} from "../../utils/ErrorsHandler";
import Dynamic, { IPage } from "../../components/Dynamic/Dynamic";
import Feedback from "../../components/Feedback/Feedback";

const ProfileSpace: IPage = () => {
  const {username} = useParams();
  const {userAccessor, dashboardAccessor} = useProfileContext();

  const isSuccess = userAccessor.isSuccess && dashboardAccessor.isSuccess;
  const isError = userAccessor.isError || dashboardAccessor.isError;
  const errorStatusCode = userAccessor.error?.statusCode ?? dashboardAccessor.error?.statusCode;

  const titles = {
    Center: <ProfileTitle/>
  }

  return <Dynamic Titles={titles}>
    {
      isError
        ? <ErrorBlock>{ErrorsHandler.default(errorStatusCode!)}</ErrorBlock>
        : !isSuccess
          ? <Skeleton height={200}/>
          : username
            ? <PublicProfile/>
            : <PrivateProfile/>
    }
    <Feedback/>
  </Dynamic>
};

ProfileSpace.Provider = ProfileSpaceProvider;
export default ProfileSpace;