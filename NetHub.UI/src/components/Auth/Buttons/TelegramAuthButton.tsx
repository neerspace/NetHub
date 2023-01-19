import React, {FC, useEffect, useRef} from 'react';
import telegramLogo from '../../../assets/images/telegram.png';
import {Button, Text, useColorModeValue} from "@chakra-ui/react";

interface ITelegramAuthButtonProps {
  onClick: (e: React.MouseEvent) => void
}

const TelegramAuthButton: FC<ITelegramAuthButtonProps> = ({onClick}) => {
  const ref = useRef<HTMLButtonElement>(null);

  const script = document.createElement("script");
  script.src = "https://telegram.org/js/telegram-widget.js?19";
  script.async = true;

  useEffect(() => {
    ref.current?.appendChild(script)
  }, [])

  return (
    <Button
      ref={ref}
      padding={'25px 20px'}
      bg={useColorModeValue('whiteLight', 'whiteDark')} onClick={onClick}
    >
      <img
        style={{height: '25px', width: '25px', marginRight: '10px'}}
        src={telegramLogo}
        alt="Telegram Login"
      />
      <Text as={'p'} color='#000000' fontWeight={'semibold'}>
        Telegram
      </Text>
    </Button>
  );
}

export default TelegramAuthButton;

