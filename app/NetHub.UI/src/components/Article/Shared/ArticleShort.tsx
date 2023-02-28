import { Text, useColorModeValue } from '@chakra-ui/react';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';
import { useArticleContext } from '../../../pages/Articles/One/ArticleSpace.Provider';
import FilledDiv from '../../UI/FilledDiv';
import cl from './ArticleShort.module.sass';
import ArticleShortFooter from './Related/ArticleShortFooter';
import ArticleShortHeader from './Related/ArticleShortHeader';
import { Vote } from "../../../api/_api";
import { ISimpleLocalization } from "../../../types/api/ISimpleLocalization";

interface IArticleItemProps {
  localization: ISimpleLocalization;
  setLocalization: (localization: ISimpleLocalization) => void;
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

  function updateCounter(rate: number, vote: Vote | null) {
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
