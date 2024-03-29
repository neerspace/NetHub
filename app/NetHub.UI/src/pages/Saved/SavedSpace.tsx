import React from 'react';
import UserLibrary, {ILibraryItem} from "../../components/UserLibrary/UserLibrary";
import SvgSelector from "../../components/UI/SvgSelector/SvgSelector";
import SavedArticles from "../../components/Article/Saved/SavedArticles";
import cl from './SavedSpace.module.sass';
import {Text, useColorModeValue} from "@chakra-ui/react";
import SavedSpaceProvider, {useSavedArticlesContext} from "./SavedSpace.Provider";
import ErrorBlock from "../../components/UI/Error/ErrorBlock";
import {ErrorsHandler} from "../../utils/ErrorsHandler";
import Currency from "../../components/Currency/Currency";
import Dynamic, { IPage } from "../../components/Dynamic/Dynamic";
import Feedback from "../../components/Feedback/Feedback";

const SavedSpace: IPage = () => {

  const {savedArticles} = useSavedArticlesContext();
  const className = useColorModeValue(cl.black, cl.white);


  const items: ILibraryItem[] = [
    {
      name: 'Статті',
      component:
        savedArticles.isError
          ? <ErrorBlock>{ErrorsHandler.default(savedArticles.error.statusCode)}</ErrorBlock>
          : <SavedArticles/>
    },
    {
      name: 'Курс валют',
      component: <Currency/>
    }
  ]

  const titles = {
    Center: <Text
      as={'h2'}
      fontWeight={700}
      display={'flex'}
      alignItems={'center'}
    >
      Збережено вами
      <SvgSelector id={'SavedOutlinedFilled'} className={`${cl.titleIcon} ${className}`}/>
    </Text>
  }

  return <Dynamic Titles={titles}>
    <UserLibrary
      items={items}
      radioGroupConfig={{
        name: 'saved',
        defaultValue: 'Статті',
      }}
    />
    <Feedback/>
  </Dynamic>
};

SavedSpace.Provider = SavedSpaceProvider;

export default SavedSpace;
