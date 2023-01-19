import React, {FC} from "react";
import facebookLogo from '../../../assets/images/facebook.png';
import {Button, Text, useColorModeValue} from "@chakra-ui/react";

interface IFacebookAuthProps {
  onClick: (e: React.MouseEvent) => void
}

const FacebookAuthButton: FC<IFacebookAuthProps> = ({onClick}) => {
  return (
    <Button
      padding={'25px 20px'}
      bg={useColorModeValue('whiteLight', 'whiteDark')} onClick={onClick}
    >
      <img
        style={{height: '25px', width: '25px', marginRight: '10px'}}
        src={facebookLogo}
        alt="Facebook Login"
      />
      <Text as={'p'} color='#000000' fontWeight={'semibold'}>
        Facebook
      </Text>
    </Button>
  );
};

export default FacebookAuthButton;
