import { Text, useColorModeValue } from '@chakra-ui/react';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';
import { useArticleContext } from '../../../pages/Articles/One/ArticleSpace.Provider';
import IExtendedArticle from '../../../types/IExtendedArticle';
import FilledDiv from '../../UI/FilledDiv';
import cl from './ArticleShort.module.sass';
import { RateVariants } from './ArticlesRateCounter';
import ArticleShortFooter from './Related/ArticleShortFooter';
import ArticleShortHeader from './Related/ArticleShortHeader';

interface IArticleItemProps {
  localization: IExtendedArticle;
  setLocalization: (localization: IExtendedArticle) => void;
  save: { actual: boolean; handle: () => Promise<void> };
  time?: { before?: string; show?: 'default' | 'saved' };
  afterCounterRequest: () => Promise<void>;
  footerVariant?: 'default' | 'created';
}

const ArticleShort: FC<IArticleItemProps> = ({
  localization,
  setLocalization,
  save,
  time,
  afterCounterRequest,
  footerVariant,
}) => {
  const { articleAccessor, setArticle } = useArticleContext();
  const navigate = useNavigate();
  const article = articleAccessor.data!;

  function updateCounter(rate: number, vote?: RateVariants) {
    setArticle({ ...article, rate });
    setLocalization({ ...localization, rate, vote });
  }

  return (
    <FilledDiv
      className={cl.articleItem}
      onClick={() =>
        navigate(
          `/article/${localization.articleId}/${localization.languageCode}`
        )
      }
      cursor={'pointer'}
    >
      <ArticleShortHeader localization={localization} time={time} />
      <Text
        as={'p'}
        className={cl.description}
        color={useColorModeValue('#4F5B67', '#EFEFEF')}
      >
        {localization.description}
      </Text>
      <ArticleShortFooter
        localization={localization}
        save={save}
        variant={footerVariant}
        updateCounter={updateCounter}
        afterCounterRequest={afterCounterRequest}
      />
    </FilledDiv>
  );
};

export default ArticleShort;
