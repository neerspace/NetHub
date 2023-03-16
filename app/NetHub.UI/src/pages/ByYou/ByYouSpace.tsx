import React from 'react';
import Layout, { Page } from "../../components/Layout/Layout";
import UserLibrary, { ILibraryItem } from "../../components/Library/UserLibrary";
import { Text, useColorModeValue } from "@chakra-ui/react";
import SvgSelector from "../../components/UI/SvgSelector/SvgSelector";
import cl from "./ByYouSpace.module.sass";
import ByYouSpaceProvider, { useByYouContext } from "./ByYouSpace.Provider";
import ErrorBlock from "../../components/Layout/ErrorBlock";
import { ErrorsHandler } from "../../utils/ErrorsHandler";
import Currency from "../../components/Currency/Currency";
import ByYouArticles from "../../components/Article/ByYou/ByYouArticles";

const ByYouSpace: Page = () => {
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
    <Layout Titles={titles}>
      <UserLibrary
        items={items}
        radioGroupConfig={{
          name: 'byYou',
          defaultValue: 'Статті',
        }}/>
    </Layout>
  );
};

ByYouSpace.Provider = ByYouSpaceProvider;

export default ByYouSpace;
