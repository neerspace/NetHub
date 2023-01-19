import React from 'react';
import PrivateProfile from "../../components/Profile/Private/PrivateProfile";
import {Skeleton} from '@chakra-ui/react';
import {useParams} from "react-router-dom";
import PublicProfile from "../../components/Profile/Public/PublicProfile";
import ProfileSpaceProvider, {useProfileContext} from "./ProfileSpace.Provider";
import ProfileTitle from "../../components/Profile/ProfileTitle";
import Layout, {Page} from "../../components/Layout/Layout";
import ErrorBlock from "../../components/Layout/ErrorBlock";
import {ErrorsHandler} from "../../utils/ErrorsHandler";

const ProfileSpace: Page = () => {
  const {username} = useParams();
  const {userAccessor, dashboardAccessor} = useProfileContext();

  const isSuccess = userAccessor.isSuccess && dashboardAccessor.isSuccess;
  const isError = userAccessor.isError || dashboardAccessor.isError;
  const errorStatusCode = userAccessor.error?.statusCode ?? dashboardAccessor.error?.statusCode;

  const titles = {
    Center: <ProfileTitle/>
  }

  return <Layout Titles={titles}>
    {
      isError
        ? <ErrorBlock>{ErrorsHandler.default(errorStatusCode!)}</ErrorBlock>
        : !isSuccess
          ? <Skeleton height={200}/>
          : username
            ? <PublicProfile/>
            : <PrivateProfile/>
    }
  </Layout>
};

ProfileSpace.Provider = ProfileSpaceProvider;
export default ProfileSpace;