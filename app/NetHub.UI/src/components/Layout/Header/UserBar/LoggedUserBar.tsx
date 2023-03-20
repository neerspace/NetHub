import React, {FC, useEffect, useState} from 'react';
import {Avatar, Box, Text, useColorModeValue} from '@chakra-ui/react';
import cl from '../Header.module.sass';
import {createImageFromInitials} from '../../../../utils/logoGenerator';
import {useNavigate} from 'react-router-dom';
import {_jwtApi} from "../../../../api";
import {useAppStore} from '../../../../store/store';

const LoggedUserBar: FC = () => {
    const {user, logout} = useAppStore();

    const navigate = useNavigate();
    const getImage = () => user.profilePhotoUrl ?? createImageFromInitials(500, user.username);
    const [image, setImage] = useState<string>(getImage());

    useEffect(() => {
      setImage(getImage());
    }, [user.profilePhotoUrl]);

    async function handleLogout() {
      navigate('/')
      logout()
      await _jwtApi.revoke();
    }

    return (
      <Box className={cl.loggedBar}>
        <Box className={cl.avatarBlock} onClick={() => navigate(`/profile/${user.username}`)}>
          <Avatar size={'md'}
                  maxW={40}
                  maxH={40}
                  src={image}
                  onError={() => setImage(createImageFromInitials(500, user.username))}
                  _hover={{cursor: 'pointer'}}/>
          <Text as={'b'} _hover={{cursor: 'pointer'}}>
            {user.username}
          </Text>
        </Box>

        <Text className={cl.logOut}
              as={'b'}
              color={useColorModeValue('#838383', '#EFEFEF')}
              onClick={handleLogout}>
          Log out
        </Text>
      </Box>
    );
  }
;

export default LoggedUserBar;
