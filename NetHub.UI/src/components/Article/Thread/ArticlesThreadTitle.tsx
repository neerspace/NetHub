import React, {FC} from 'react';
import cl from './ArticlesThread.module.sass';
import {Box, Select, Text} from "@chakra-ui/react";

interface IArticleThreadTitleProps {
  title: string | JSX.Element,
  options: { title: string, value: string }[],
  setArticlesLanguage: (value: string) => void,
  articlesLanguage: string
}

const ArticlesThreadTitle: FC<IArticleThreadTitleProps> =
  ({title, options, articlesLanguage, setArticlesLanguage,}) => {

    return (
      <Box className={cl.threadTitle}>
        <Text as={'h2'}>{title}</Text>

        <Select
          border={'1px solid'}
          borderColor={'gray.200'}
          width={'fit-content'} defaultValue={articlesLanguage}
          onChange={(e) => {
            setArticlesLanguage(e.target.value)
          }}
          aria-label={'Мова'}
        >
          {options.map(option =>
            <option key={option.value} value={option.value}>{option.title}</option>
          )}
        </Select>
      </Box>
    )
  };

export default ArticlesThreadTitle;
