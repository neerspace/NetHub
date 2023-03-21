import React, { useState } from 'react';
import { useQuery } from "react-query";
import FilledDiv from "../UI/FilledDiv";
import ExchangeRate from "./Exchange/ExchangeRate";
import cl from './Currency.module.sass';
import CryptoRate from "./Crypto/CryptoRate";
import { Box, Skeleton, Text, useColorMode, useColorModeValue } from "@chakra-ui/react";
import { DateTime } from "luxon";
import { ApiError } from "../../types/ApiError";
import { _currenciesApi } from "../../api";
import { CurrenciesResponse } from "../../api/_api";


const Currency = () => {
  const currencies = useQuery<CurrenciesResponse, ApiError>('currency', () => _currenciesApi.get(), {
    onSuccess: (result) => setDate(result.updated)
  });
  const blockColor = useColorModeValue('whiteLight', '#333439')
  const {colorMode} = useColorMode();
  const [date, setDate] = useState<DateTime>(DateTime.now());

  const updatedColor = useColorModeValue('#D4D4D8', 'whiteDark');
  const updatedDateFormatted = `${date.hour > 9 ? date.hour: '0'+date.hour.toString()}:${date.minute > 9 ? date.minute : '0'+date.minute.toString()}`;

  if (currencies.isLoading) return <Skeleton height='270px' mb={'15px'}/>;

  return (
      <FilledDiv
        bg={blockColor} border={colorMode === 'light' ? '1px solid #EFEFEF' : ''}
        height={'fit-content'} mb={'15px'}
      >
        <ExchangeRate rate={currencies.data!.exchanges!}/>
        <hr className={cl.line}/>
        <CryptoRate rate={currencies.data!.crypto!}/>
        <Box display={'flex'}>
          <Text as={'p'} fontSize={14} color={updatedColor}>оновлено: </Text>
          <Text
            fontSize={14} color={updatedColor}
            as={'p'} fontWeight={'bold'}
          >{updatedDateFormatted}</Text>
        </Box>
      </FilledDiv>
  );
};

export default Currency;
