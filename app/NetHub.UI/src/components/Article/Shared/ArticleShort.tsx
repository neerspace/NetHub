import { Text, useColorModeValue } from '@chakra-ui/react';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';
import { useArticleContext } from '../../../pages/Articles/One/ArticleSpace.Provider';
import FilledDiv from '../../UI/FilledDiv';
import cl from './ArticleShort.module.sass';
import ArticleShortFooter from './Related/ArticleShortFooter';
import ArticleShortHeader from './Related/ArticleShortHeader';
import { Vote } from "../../../api/_api";
import { ISimpleArticle } from "../../../types/api/ISimpleArticle";

interface IArticleItemProps {
  article: ISimpleArticle;
  setArticle: (article: ISimpleArticle) => void;
  save: { actual: boolean; handle: () => Promise<void> };
  time?: { before?: string; show?: 'default' | 'saved' };
  afterCounterRequest: () => Promise<void>;
  variant?: 'public' | 'private';
}

const ArticleShort: FC<IArticleItemProps> = ({
  article,
  setArticle,
  save,
  time,
  afterCounterRequest,
  variant,
}) => {
  const { articleSetAccessor, setArticleSet } = useArticleContext();
  const navigate = useNavigate();
  const articleSet = articleSetAccessor.data!;

  function updateCounter(rate: number, vote: Vote | null) {
    setArticleSet({ ...articleSet, rate });
    setArticle({ ...article, rate, vote });
  }

  return (
    <FilledDiv
      className={cl.articleItem}
      onClick={() =>
        navigate(
          `/article/${article.articleSetId}/${article.languageCode}`
        )
      }
      cursor={'pointer'}
    >
      <ArticleShortHeader article={article} time={time} variant={variant}/>
      <Text
        as={'p'}
        className={cl.description}
        color={useColorModeValue('#4F5B67', '#EFEFEF')}
      >
        {article.description}
      </Text>
      <ArticleShortFooter
        article={article}
        save={save}
        variant={variant}
        updateCounter={updateCounter}
        afterCounterRequest={afterCounterRequest}
      />
    </FilledDiv>
  );
};

export default ArticleShort;
