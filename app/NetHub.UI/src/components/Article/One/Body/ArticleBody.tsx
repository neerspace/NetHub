import { Badge, Box, Button, Skeleton, Text, } from '@chakra-ui/react';
import React, { useCallback } from 'react';
import { useQuery, useQueryClient } from 'react-query';
import { useNavigate } from "react-router-dom";
import {
  getArticleContributors,
  getAuthor,
} from '../../../../pages/Articles/One/ArticleSpace.functions';
import { useArticleContext } from '../../../../pages/Articles/One/ArticleSpace.Provider';
import Actions from '../../../UI/Action/Actions';
import FilledDiv from '../../../UI/FilledDiv';
import ArticleSavingActions from '../../Shared/ArticleSavingActions';
import ArticlesRateCounter from '../../Shared/ArticlesRateCounter';
import cl from './ArticleBody.module.sass';
import { _myArticlesApi } from "../../../../api";
import { ArticleLocalizationModel, ArticleModelExtended, Vote } from "../../../../api/_api";
import { QueryClientKeysHelper } from '../../../../utils/QueryClientKeysHelper';

const ArticleBody = () => {
  const { articleAccessor, setArticle, localizationAccessor, setLocalization } =
    useArticleContext();
  const localization = localizationAccessor.data!;
  const article = articleAccessor.data!;
  const queryClient = useQueryClient();

  async function handleSave() {
    await _myArticlesApi.toggleSave(
      localizationAccessor.data!.articleId,
      localizationAccessor.data!.languageCode)
  }

  function handleUpdateCounter(rate: number, vote: Vote | null) {
    setArticle(new ArticleModelExtended({ ...article, rate }));
    setLocalization(new ArticleLocalizationModel ({ ...localization, vote, rate }));
  }

  async function afterCounter() {
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.savedArticles);
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.articles);
  }

  const contributors = useQuery(
    QueryClientKeysHelper.Contributors(localization.articleId, localization.languageCode),
    () => getArticleContributors(localization.contributors)
  );

  const navigate = useNavigate();

  const getDate = useCallback(() => {
    switch (localization.status) {
      case ('Draft' || 'Pending'):
        return `Створено: ${localization.created.toRelativeCalendar()}`;
      case 'Published':
        return `Опубліковано: ${localization.published?.toRelativeCalendar()}`;
      case 'Banned':
        return `Забанено: ${localization.banned!.toRelativeCalendar()}`;
    }
  }, [localization]);

  const getBadge = useCallback(() => {
    if (localization.status === 'Draft' || localization.status === 'Pending')
      return <Badge ml={2} variant='outline' colorScheme='yellow'>
        Preview
      </Badge>;
    if (localization.status === 'Banned')
      return <Badge ml={2} variant={'outline'} colorScheme={'red'}>
        Banned
      </Badge>
  }, [localization])

  return (
    <FilledDiv className={cl.articleWrapper}>
      <Box className={cl.articleTitle} display={'flex'} alignItems={'center'}>
        <Text as={'p'} fontWeight={'bold'} fontSize={18}>
          {localization.title}
        </Text>
        {getBadge()}
      </Box>

      <div className={cl.articleDescription}>
        <Text>{localization.description}</Text>
      </div>

      <hr className={cl.line} />

      <div
        className={cl.articleBody}
        dangerouslySetInnerHTML={{ __html: localization.html }}
      />

      <div className={cl.articleTags}>
        {article.tags.map((tag) => (
          <Button
            key={tag}
            className={cl.tag}
            maxH={30}
            borderRadius={'10px'}
            width={'fit-content'}
          >
            #{tag}
          </Button>
        ))}
      </div>

      <div className={cl.actions}>
        <div className={cl.actionsLeft}>
          <ArticlesRateCounter
            articleId={article.id}
            rate={article.rate}
            vote={localization.vote}
            updateCounter={handleUpdateCounter}
            afterRequest={afterCounter}
          />
          <Actions className={cl.views}>
            <Text as={'b'} color={'black'} className={cl.viewsCount}>
              {localization.views}
            </Text>
            <Text as={'p'} color={'black'}>
              переглядів
            </Text>
          </Actions>
        </div>
        <ArticleSavingActions
          isSavedDefault={localization.isSaved}
          onSave={handleSave}
        />
      </div>

      <div className={cl.creationInfo}>
        <div className={cl.author}>
          <Box display={'flex'} alignItems={'center'}>
            <Text as={'p'}>Автор:</Text>
            {!contributors.isSuccess ? <Skeleton width={'100px'} height={15}/> :
              <Box
                onClick={() => navigate('/profile/' + getAuthor(localization.contributors, contributors.data!)?.userName)}
                cursor={'pointer'}
              >
                {getAuthor(localization.contributors, contributors.data!)?.userName}
              </Box>
            }
          </Box>
        </div>
        <div className={cl.dates}>
          <div className={cl.created}>{getDate()}</div>
          {localization.updated ? (
            <div className={cl.updated}>
              Оновлено: {localization.updated.toRelativeCalendar()}
            </div>
          ) : null}
        </div>
      </div>
    </FilledDiv>
  );
};

export default ArticleBody;