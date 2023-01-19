import React from 'react';
import cl from '../Profile.module.sass'
import PrivateDashboard from "./PrivateDashboard";
import SvgSelector from "../../UI/SvgSelector/SvgSelector";
import {useNavigate} from "react-router-dom";
import ProfileSettings from "../ProfileSettings";
import FilledDiv from '../../UI/FilledDiv';
import {Button, Text} from '@chakra-ui/react';


const PrivateProfile = () => {
  const navigate = useNavigate();

  return (
    <div className={cl.profileWrapper}>
      <PrivateDashboard/>
      <FilledDiv
        className={cl.profileButton}
        width={'100%'}
      >
        <div className={cl.buttonInside}>
          <div>
            <div>
              <SvgSelector id={'ProfileCreated'}/>
            </div>
            <Text as={'p'}>
              Створені вами статті
            </Text>
          </div>
          <Button
            fontSize={'14px'}
            onClick={() => navigate('/by-you')}
            minW={'120px'}
          >
            Показати
          </Button>
        </div>
      </FilledDiv>
      <FilledDiv
        className={cl.profileButton}
        width={'100%'}
      >
        <div className={cl.buttonInside}>
          <div>
            <div>
              <SvgSelector id={'ProfileBookmark'}/>
            </div>
            <Text as={'p'}>
              Збережені вами статті
            </Text>
          </div>
          <Button
            fontSize={'14px'}
            minW={'120px'}
            onClick={() => navigate('/saved')}
          >
            Показати
          </Button>
        </div>
      </FilledDiv>
      <ProfileSettings/>
    </div>
  );
};

export default PrivateProfile;
