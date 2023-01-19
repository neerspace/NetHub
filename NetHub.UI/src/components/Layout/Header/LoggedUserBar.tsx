import React, {FC, useEffect, useState} from 'react';
import {Avatar, Text, useColorModeValue} from '@chakra-ui/react';
import classes from './Header.module.sass';
import {createImageFromInitials} from "../../../utils/logoGenerator";
import {useNavigate} from "react-router-dom";
import {useAppStore} from "../../../store/config";
import {userApi} from "../../../api/api";

const LoggedUserBar: FC = () => {
    const {user, logout} = useAppStore();

    const navigate = useNavigate();
    const getImage = () => user.profilePhotoUrl ?? createImageFromInitials(500, user.username);
    const [image, setImage] = useState<string>(getImage());

    useEffect(() => {
      setImage(getImage())
    }, [user.profilePhotoUrl])

    function handleLogout() {
      logout();
      userApi.logout().then(() => navigate('/'));
    }

    return (
      <div className={classes.loggedBar}>
        <div className={classes.avatarBlock} onClick={() => navigate(`/profile/${user.username}`)}>
          <Avatar
            size={'md'}
            maxW={40}
            maxH={40}
            src={image}
            onError={() => setImage(createImageFromInitials(500, user.username))}
            _hover={{cursor: 'pointer'}}
          />
          <Text
            as={'b'}
            _hover={{cursor: 'pointer'}}
          >
            {user.username}
          </Text>
        </div>

        <Text
          className={classes.logOut}
          as={'b'}
          color={useColorModeValue('#838383', '#EFEFEF')}
          onClick={handleLogout}
        >
          Log out
        </Text>
      </div>
    );
  }
;

export default LoggedUserBar;
