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
import { QueryClientKeysHelper } from '../../../../utils/QueryClientKeysHelper';
import { ArticleModel, ArticleSetModelExtended, Vote } from "../../../../api/_api";

const ArticleBody = () => {
  const { articleSetAccessor, setArticleSet, articleAccessor, setArticle } =
    useArticleContext();
  const articleSet = articleSetAccessor.data!;
  const article = articleAccessor.data!;
  const queryClient = useQueryClient();

  async function handleSave() {
    await _myArticlesApi.toggleSave(
      articleAccessor.data!.articleSetId,
      articleAccessor.data!.languageCode)
  }

  function handleUpdateCounter(rate: number, vote: Vote | null) {
    setArticleSet(new ArticleSetModelExtended({ ...articleSet, rate }));
    setArticle(new ArticleModel ({ ...article, vote, rate }));
  }

  async function afterCounter() {
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.savedArticles);
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.articles);
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticlesByYou());
  }

  const contributors = useQuery(
    QueryClientKeysHelper.Contributors(article.articleSetId, article.languageCode),
    () => getArticleContributors(article.contributors)
  );

  const navigate = useNavigate();

  const getDate = useCallback(() => {
    switch (article.status) {
      case ('Draft' || 'Pending'):
        return `Створено: ${article.created.toRelativeCalendar()}`;
      case 'Published':
        return `Опубліковано: ${article.published?.toRelativeCalendar()}`;
      case 'Banned':
        return `Забанено: ${article.banned!.toRelativeCalendar()}`;
    }
  }, [article]);

  const getBadge = useCallback(() => {
    if (article.status === 'Draft' || article.status === 'Pending')
      return <Badge ml={2} variant='outline' colorScheme='yellow'>
        Preview
      </Badge>;
    if (article.status === 'Banned')
      return <Badge ml={2} variant={'outline'} colorScheme={'red'}>
        Banned
      </Badge>
  }, [article])

  return (
    <FilledDiv className={cl.articleWrapper}>
      <Box className={cl.articleTitle} display={'flex'} alignItems={'center'}>
        <Text as={'p'} fontWeight={'bold'} fontSize={18}>
          {article.title}
        </Text>
        {getBadge()}
      </Box>

      <div className={cl.articleDescription}>
        <Text>{article.description}</Text>
      </div>

      <hr className={cl.line} />

      <div
        className={cl.articleBody}
        dangerouslySetInnerHTML={{ __html: article.html }}
      />

      <div className={cl.articleTags}>
        {articleSet.tags.map((tag) => (
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
            articleSetId={articleSet.id}
            rate={articleSet.rate}
            vote={article.vote}
            updateCounter={handleUpdateCounter}
            afterRequest={afterCounter}
          />
          <Actions className={cl.views}>
            <Text as={'b'} color={'black'} className={cl.viewsCount}>
              {article.views}
            </Text>
            <Text as={'p'} color={'black'}>
              переглядів
            </Text>
          </Actions>
        </div>
        <ArticleSavingActions
          articleSetId={article.articleSetId}
          articleLanguage={article.languageCode}
          isSavedDefault={article.isSaved}
          onSave={handleSave}
        />
      </div>

      <div className={cl.creationInfo}>
        <div className={cl.author}>
          <Box display={'flex'} alignItems={'center'}>
            <Text as={'p'}>Автор:</Text>
            {!contributors.isSuccess ? <Skeleton width={'100px'} height={15}/> :
              <Box
                onClick={() => navigate('/profile/' + getAuthor(article.contributors, contributors.data!)?.userName)}
                cursor={'pointer'}
              >
                {getAuthor(article.contributors, contributors.data!)?.userName}
              </Box>
            }
          </Box>
        </div>
        <div className={cl.dates}>
          <div className={cl.created}>{getDate()}</div>
          {article.updated ? (
            <div className={cl.updated}>
              Оновлено: {article.updated.toRelativeCalendar()}
            </div>
          ) : null}
        </div>
      </div>
    </FilledDiv>
  );
};

export default ArticleBody;