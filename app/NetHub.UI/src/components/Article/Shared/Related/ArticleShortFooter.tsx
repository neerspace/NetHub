import React, { FC } from 'react';
import cl from "../ArticleShared.module.sass";
import ArticlesRateCounter from "../ArticlesRateCounter";
import ArticleSaveAction from "../ArticleSaveAction";
import { Box, Text } from "@chakra-ui/react";
import Actions from "../../../UI/Action/Actions";
import { ContentStatus, Vote } from "../../../../api/_api";
import { ISimpleArticle } from "../../../../types/api/ISimpleArticle";
import { useNavigate } from "react-router-dom";

interface IArticleShortFooterProps {
  article: ISimpleArticle,
  save: { actual: boolean, handle: () => Promise<void> },
  updateCounter: (rate: number, vote: Vote | null) => void,
  afterCounterRequest: () => Promise<void>,
  variant?: 'public' | 'private'
}

const ArticleShortFooter: FC<IArticleShortFooterProps> =
  ({article, save, updateCounter, afterCounterRequest, variant}) => {
    const navigate = useNavigate();

    const addArticleHandle = (e: React.MouseEvent<HTMLDivElement>) => {
      e.stopPropagation()
      navigate(`/article/${article.articleSetId}/translate`);
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
          {(variant ?? 'public') === 'private'
            ? <Actions onClick={addArticleHandle}>
              <Text as={'p'} color={'black'}>Додати переклад +</Text>
            </Actions>
            : null
          }
        </Box>
        {
          article.status === ContentStatus.Published
            ? <ArticleSaveAction
              articleSetId={article.articleSetId}
              articleLanguage={article.languageCode}
              isSavedDefault={save.actual}
              onSave={save.handle}
              saveLink={`${window.location.href}article/${article.articleSetId}/${article.languageCode}`}
            />
            : null
        }
      </div>
    );
  };

export default ArticleShortFooter;