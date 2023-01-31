import React, {FC} from 'react';
import {IExchangesResponse} from "../../types/api/Currency/ICurrencyResponse";
import {Box, Text, useColorModeValue} from "@chakra-ui/react";
import CurrencyRow from "./CurrencyRow";
import CurrencyUsd from "../UI/Icons/CurrencyUsd";
import CurrencyEuro from "../UI/Icons/CurrencyEuro";

interface IExchangeProps {
  rate: IExchangesResponse
}

const ExchangeRate: FC<IExchangeProps> = ({rate}) => {
  const headersColor = useColorModeValue('#D4D4D8', 'whiteDark');
  const formatNumber = (number: number) => Math.round((number + Number.EPSILON) * 100) / 100

  return (
    <Box>
      <CurrencyRow
        nodes={[
          null, <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Валюта</Text>,
          <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Купівля</Text>, null,
          <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Продаж</Text>
        ]}
      />
      <CurrencyRow
        nodes={[
          <Box display={'flex'} alignItems={'center'} height={'100%'}>
            <CurrencyUsd/>
          </Box>,
          <Text as={'p'} fontWeight={'bold'} color={useColorModeValue('purpleLight', 'purpleDark')}>USD</Text>,
          <Text as={'p'} fontWeight={'bold'}>{formatNumber(rate.usd.rateBuy)}</Text>,
          <Text as={'p'} fontWeight={'bold'} color={headersColor}>/</Text>,
          <Text as={'p'} fontWeight={'bold'}>{formatNumber(rate.usd.rateSell)}</Text>
        ]}
      />
      <CurrencyRow
        nodes={[
          <Box display={'flex'} alignItems={'center'} height={'100%'}>
            <CurrencyEuro/>
          </Box>,
          <Text as={'p'} fontWeight={'bold'} color={useColorModeValue('purpleLight', 'purpleDark')}>EUR</Text>,
          <Text as={'p'} fontWeight={'bold'}>{formatNumber(rate.euro.rateBuy)}</Text>,
          <Text as={'p'} fontWeight={'bold'} color={headersColor}>/</Text>,
          <Text as={'p'} fontWeight={'bold'}>{formatNumber(rate.euro.rateSell)}</Text>
        ]}
      />
    </Box>
  );
};

export default ExchangeRate;
