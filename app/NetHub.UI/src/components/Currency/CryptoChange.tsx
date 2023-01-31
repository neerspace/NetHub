import React, {FC} from 'react';
import {Box, Text, useColorModeValue} from "@chakra-ui/react";
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import cl from './Currency.module.sass';

interface ICryptoChangeProps {
  number: number
}

const CryptoChange: FC<ICryptoChangeProps> = ({number}) => {
  const zeroColor = useColorModeValue('whiteLight', 'whiteDark');
  const color = number > 0 ? '#0CA312' : '#E92222';
  const displayNumber = number === 0 ? 0 :
    number > 0 ?
      Math.round((number + Number.EPSILON) * 100) / 100 :
      Math.round((-number + Number.EPSILON) * 100) / 100;

  return (
    <Box display={'flex'} alignItems={'center'} className={number > 0 ? cl.ratePositive : cl.rateNegative}>
      {number === 0 ? null :
        number > 0
          ? <SvgSelector id={'SmallArrowUp'}/>
          : <SvgSelector id={'SmallArrowDown'}/>
      }
      <Text color={number === 0 ? zeroColor : color} as={'p'} fontWeight={'bold'}>
        {displayNumber}%
      </Text>

    </Box>
  );
};

export default CryptoChange;
