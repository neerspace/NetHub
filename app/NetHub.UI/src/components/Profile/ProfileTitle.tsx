import React from 'react';
import {Skeleton, Text} from "@chakra-ui/react";
import {useProfileContext} from "../../pages/Profile/ProfileSpace.Provider";
import {useParams} from "react-router-dom";
import { useAppStore } from "../../store/store";

const ProfileTitle = () => {
  const {username} = useParams();
  const {userAccessor} = useProfileContext();
  const reduxUser = useAppStore(state => state.user);

  const title = username
    ? userAccessor.data?.userName
      ? userAccessor.data.userName
      : <Skeleton width={'30%'}>height</Skeleton> //'height' - to declare font height to skeleton
    : `Вітаю, ${reduxUser.username}`;

  return (
    <Text as={'h2'} style={{display: 'flex'}}>{title}</Text>
  );
};

export default ProfileTitle;