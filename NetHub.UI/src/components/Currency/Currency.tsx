import React, {useState} from 'react';
import {useQuery} from "react-query";
import {infoApi} from "../../api/api";
import FilledDiv from "../UI/FilledDiv";
import ExchangeRate from "./ExchangeRate";
import cl from './Currency.module.sass';
import CryptoRate from "./CryptoRate";
import {Text, Skeleton, useColorMode, useColorModeValue, Box, Button, ChakraProvider} from "@chakra-ui/react";
import ICurrencyResponse from "../../types/api/Currency/ICurrencyResponse";
import {DateTime} from "luxon";
import {ApiError} from "../../types/ApiError";
import {useArticlesThreadContext} from "../../pages/Articles/Thread/ArticlesThreadSpace.Provider";
import theme from "../../constants/themes";


const Currency = () => {
  const currencies = useQuery<ICurrencyResponse, ApiError>('currency', () => infoApi.getCurrenciesRate(), {
    onSuccess: (result) => setDate(DateTime.fromISO(result.updated))
  });
  const blockColor = useColorModeValue('whiteLight', '#333439')
  const {colorMode} = useColorMode();
  const [date, setDate] = useState<DateTime>(DateTime.now());
  const updatedColor = useColorModeValue('#D4D4D8', 'whiteDark');

  if (currencies.isLoading) return <Skeleton height='270px'/>;


  return (
      <FilledDiv
        bg={blockColor} border={colorMode === 'light' ? '1px solid #EFEFEF' : ''}
        height={'fit-content'}
      >
        <ExchangeRate rate={currencies.data!.exchanges!}/>
        <hr className={cl.line}/>
        <CryptoRate rate={currencies.data!.crypto!}/>
        <Box display={'flex'}>
          <Text as={'p'} fontSize={14} color={updatedColor}>оновлено: </Text>
          <Text
            fontSize={14} color={updatedColor}
            as={'p'} fontWeight={'bold'}
          >{date.hour > 9 ? date.hour : `0${date.hour}`}:{date.minute > 9 ? date.minute : `0${date.minute}`}</Text>
        </Box>
      </FilledDiv>
  );
};

export default Currency;
