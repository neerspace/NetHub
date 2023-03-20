import React from 'react';
import UserLibrary, { ILibraryItem } from "../../components/UserLibrary/UserLibrary";
import { Text, useColorModeValue } from "@chakra-ui/react";
import SvgSelector from "../../components/UI/SvgSelector/SvgSelector";
import cl from "./ByYouSpace.module.sass";
import ByYouSpaceProvider, { useByYouContext } from "./ByYouSpace.Provider";
import ErrorBlock from "../../components/UI/Error/ErrorBlock";
import { ErrorsHandler } from "../../utils/ErrorsHandler";
import Currency from "../../components/Currency/Currency";
import ByYouArticles from "../../components/Article/ByYou/ByYouArticles";
import Dynamic, { IPage } from "../../components/Dynamic/Dynamic";

const ByYouSpace: IPage = () => {
  const {articlesAccessor} = useByYouContext();

  const className = useColorModeValue(cl.black, cl.white);

  const items: ILibraryItem[] = [
    {
      name: 'Статті',
      component: articlesAccessor.isError
          ? <ErrorBlock>{ErrorsHandler.default(articlesAccessor.error.statusCode)}</ErrorBlock>
          : <ByYouArticles/>
    },
    {
      name: 'Курс',
      component: <Currency/>
    },
  ]

  const titles = {
    Center: <Text
      as={'h2'}
      fontWeight={700}
      display={'flex'}
      alignItems={'center'}
    >
      Створено вами
      <SvgSelector id={'ByYouTitle'} className={`${cl.titleIcon} ${className}`}/>
    </Text>
  }

  return (
    <Dynamic Titles={titles}>
      <UserLibrary
        items={items}
        radioGroupConfig={{
          name: 'byYou',
          defaultValue: 'Статті',
        }}/>
    </Dynamic>
  );
};

ByYouSpace.Provider = ByYouSpaceProvider;

export default ByYouSpace;
