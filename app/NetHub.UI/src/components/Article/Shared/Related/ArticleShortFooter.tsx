import React, { FC } from 'react';
import cl from "../ArticleShort.module.sass";
import ArticlesRateCounter from "../ArticlesRateCounter";
import ArticleSavingActions from "../ArticleSavingActions";
import { Box, Text } from "@chakra-ui/react";
import Actions from "../../../UI/Action/Actions";
import { Vote } from "../../../../api/_api";
import { ISimpleArticle } from "../../../../types/api/ISimpleArticle";

interface IArticleShortFooterProps {
  article: ISimpleArticle,
  save: { actual: boolean, handle: () => Promise<void> },

  updateCounter: (rate: number, vote: Vote | null) => void,

  afterCounterRequest: () => Promise<void>,

  variant?: 'default' | 'created'
}

const ArticleShortFooter: FC<IArticleShortFooterProps> =
  ({article, save, updateCounter, afterCounterRequest, variant}) => {

    const addArticleHandle = (e: React.MouseEvent<HTMLDivElement>) => {
      e.stopPropagation()
      alert('add article localization')
    }

    return (
      <div className={cl.actions}>
        <Box display={'flex'}>
          <ArticlesRateCounter
            rate={article.rate}
            articleSetId={article.articleSetId}
            vote={article.vote}
            updateCounter={updateCounter}
            afterRequest={afterCounterRequest}
          />
          {(variant ?? 'default') === 'created'
            ? <Actions onClick={addArticleHandle}>
              <Text as={'p'} color={'black'}>Додати переклад +</Text>
            </Actions>
            : null
          }
        </Box>
        <ArticleSavingActions
          isSavedDefault={save.actual}
          onSave={save.handle}
          saveLink={`${window.location.href}article/${article.articleSetId}/${article.languageCode}`}
        />
      </div>
    );
  };

export default ArticleShortFooter;