import React, {FC} from 'react';
import {ICryptoResponse} from "../../types/api/Currency/ICurrencyResponse";
import {Box, Text, useColorModeValue} from "@chakra-ui/react";
import SvgSelector from "../UI/SvgSelector/SvgSelector";
import CurrencyRow from "./CurrencyRow";
import CryptoChange from "./CryptoChange";

interface ICryptoRateProps {
  rate: ICryptoResponse
}

const CryptoRate: FC<ICryptoRateProps> = ({rate}) => {
  const headersColor = useColorModeValue('#D4D4D8', 'whiteDark');


  return (
    <Box>
      <CurrencyRow
        nodes={[
          null, <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Валюта</Text>,
          <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Курс</Text>, null,
          <Text as={'p'} fontSize={12} fontWeight={'semibold'} color={headersColor}>Зміна</Text>
        ]}
      />
      <CurrencyRow
        nodes={[
          <Box display={'flex'} alignItems={'center'} height={'100%'}>
            <SvgSelector id={'Btc'}/>
          </Box>,
          <Text as={'p'} fontWeight={'bold'} color={useColorModeValue('purpleLight', 'purpleDark')}>BTC</Text>,
          <Text as={'p'} fontWeight={'bold'}>{rate.btc.usd}$</Text>,
          null,
          <CryptoChange number={rate.btc.usd24Change}/>
          // <Text as={'p'} fontWeight={'bold'}>36,78</Text>
        ]}
      />
      <CurrencyRow
        nodes={[
          <Box display={'flex'} alignItems={'center'} height={'100%'}>
            <SvgSelector id={'Ton'}/>
          </Box>,
          <Text as={'p'} fontWeight={'bold'} color={useColorModeValue('purpleLight', 'purpleDark')}>TON</Text>,
          <Text as={'p'} fontWeight={'bold'}>{rate.ton.usd}$</Text>,
          null,
          <CryptoChange number={rate.ton.usd24Change}/>
        ]}
      />
    </Box>
  );
};

export default CryptoRate;
