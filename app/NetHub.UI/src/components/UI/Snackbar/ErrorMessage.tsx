import React, {FC} from 'react';
import {Box, Text, useColorModeValue} from "@chakra-ui/react";

export interface ISnackbarMessage {
  title?: string
  message: string
}

const ErrorMessage: FC<ISnackbarMessage> = ({title, message}) => {

  const textColor = useColorModeValue('#FFFFFF', '#EFEFEF');

  return (
    <div>
      <Box display={'flex'} flexDirection={'column'}>
        {title
          ? <>
            <Text as={'b'}>{title}</Text>
            <br/>
          </>
          : null}
        <Text as={'p'} color={textColor}>
          {message}
        </Text>
      </Box>
    </div>
  );
};

export default ErrorMessage;
