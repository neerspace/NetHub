import React from 'react';
import Layout, { Page } from "../../components/Layout/Layout";
import UserLibrary, { ILibraryItem } from "../../components/Library/UserLibrary";
import SavedArticles from "../../components/Article/Saved/SavedArticles";
import { Text, useColorModeValue } from "@chakra-ui/react";
import SvgSelector from "../../components/UI/SvgSelector/SvgSelector";
import cl from "./ByYouSpace.module.sass";

const ByYouSpace: Page = () => {
  const className = useColorModeValue(cl.black, cl.white);

  const items: ILibraryItem[] = [
    {
      name: 'Статті',
      component: <SavedArticles/>
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
        items={items} title={<p>Hello World!</p>}
        radioGroupConfig={{
          name: 'byYou',
          defaultValue: 'Статті',
        }}/>
    </Layout>
  );
};

ByYouSpace.Provider = React.Fragment;

export default ByYouSpace;
