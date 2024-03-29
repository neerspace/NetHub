import React from 'react';
import {Box, Button, Text, useColorModeValue} from "@chakra-ui/react";
import cl from './NotFoundSpace.module.sass';
import SvgSelector from "../components/UI/SvgSelector/SvgSelector";
import ErrorBlock from "../components/UI/Error/ErrorBlock";
import Dynamic, { IPage } from "../components/Dynamic/Dynamic";

const NotFoundSpace: IPage = () => {
  const textColor = useColorModeValue('whiteLight', 'whiteDark');

  const config = {
    Left: {showSidebar: false}
  }

  return <Dynamic Config={config}>
    <>
      <ErrorBlock
        className={cl.notFoundBlock} pt={'90px'} pb={'90px'}
      >
        Упс, такої сторінки не існує
      </ErrorBlock>
      <Box className={cl.actions}>
        <Button>
          <SvgSelector id={'Globe'}/>
          <Text as={'p'} color={textColor}>
            Стрічка
          </Text>
        </Button>
        <Button>
          <SvgSelector id={'StarCircle'}/>
          <Text as={'p'} color={textColor}>
            Рекомендації
          </Text>
        </Button>
        <Button>
          <SvgSelector id={'MenuSaved'}/>
          <Text as={'p'} color={textColor}>
            Збережено
          </Text>
        </Button>
        <Button>
          <SvgSelector id={'Draw'}/>
          <Text as={'p'} color={textColor}>
            Створено
            вами</Text
          ></Button>
        <Button>
          <SvgSelector id={'Send'}/>
          <Text as={'p'} color={textColor}>
            Підписки
          </Text>
        </Button>
        <Button>
          <SvgSelector id={'Profile'}/>
          <Text as={'p'} color={textColor}>
            Профіль
          </Text>
        </Button>
      </Box>
    </>
  </Dynamic>;
};

NotFoundSpace.Provider = React.Fragment;

export default NotFoundSpace;
