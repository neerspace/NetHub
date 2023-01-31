import React, {FC} from 'react';
import googleLogo from '../../../assets/images/google.png';
import {Button, Text, useColorModeValue} from "@chakra-ui/react";

interface IGoogleAuthProps {
  onClick: (e: React.MouseEvent) => void
}

const GoogleAuthButton: FC<IGoogleAuthProps> = ({onClick}) => {
  return (
    <Button
      padding={'25px 20px'}
      bg={useColorModeValue('whiteLight', 'whiteDark')} onClick={onClick}>
      <img
        style={{height: '25px', width: '25px', marginRight: '10px'}}
        src={googleLogo}
        alt="Google Login"
      />
      <Text as={'p'} color='#000000' fontWeight={'semibold'}>
        Google
      </Text>
    </Button>
  );
};

export default GoogleAuthButton;
