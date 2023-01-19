import React, {FC} from 'react';
import {Box} from "@chakra-ui/react";
import cl from './Currency.module.sass';

type Nodes = [React.ReactNode, React.ReactNode, React.ReactNode, React.ReactNode, React.ReactNode];

interface ICurrencyRowProps {
  nodes: Nodes
}

const CurrencyRow: FC<ICurrencyRowProps> = ({nodes}) => {
  return (
    <Box className={cl.currencyRow}>
      <Box>{nodes[0]}</Box>
      <Box>{nodes[1]}</Box>
      <Box>{nodes[2]}</Box>
      <Box>{nodes[3]}</Box>
      <Box>{nodes[4]}</Box>
    </Box>
  );
};

export default CurrencyRow;
