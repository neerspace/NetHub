import React, {FC} from 'react';
import cl from "../ArticleShort.module.sass";
import ArticlesRateCounter, {RateVariants} from "../ArticlesRateCounter";
import ArticleSavingActions from "../ArticleSavingActions";
import IExtendedArticle from "../../../../types/IExtendedArticle";
import {Box, Text} from "@chakra-ui/react";
import Actions from "../../../UI/Action/Actions";

interface IArticleShortFooterProps {
  localization: IExtendedArticle,
  save: { actual: boolean, handle: () => Promise<void> },

  updateCounter: (rate: number, vote?: RateVariants) => void,

  afterCounterRequest: () => Promise<void>,

  variant?: 'default' | 'created'
}

const ArticleShortFooter: FC<IArticleShortFooterProps> =
  ({localization, save, updateCounter, afterCounterRequest, variant}) => {

    const addLocalizationHandle = (e: React.MouseEvent<HTMLDivElement>) => {
      e.stopPropagation()
      alert('add article localization')
    }

    return (
      <div className={cl.actions}>
        <Box display={'flex'}>
          <ArticlesRateCounter
            rate={localization.rate}
            articleId={localization.articleId}
            vote={localization.vote ?? undefined}
            updateCounter={updateCounter}
            afterRequest={afterCounterRequest}
          />
          {(variant ?? 'default') === 'created'
            ? <Actions onClick={addLocalizationHandle}>
              <Text as={'p'} color={'black'}>Додати переклад +</Text>
            </Actions>
            : null
          }
        </Box>
        <ArticleSavingActions
          isSavedDefault={save.actual}
          onSave={save.handle}
          saveLink={`${window.location.href}article/${localization.articleId}/${localization.languageCode}`}
        />
      </div>
    );
  };

export default ArticleShortFooter;