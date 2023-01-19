import { Text } from '@chakra-ui/react';
import React, { FC } from 'react';
import { articlesApi } from '../../../api/api';
import useCustomSnackbar from '../../../hooks/useCustomSnackbar';
import { isAccessTokenValid } from '../../../utils/JwtHelper';
import Actions from '../../UI/Action/Actions';
import SvgSelector from '../../UI/SvgSelector/SvgSelector';
import cl from './ArticleRateCounter.module.sass';

export type RateVariants = 'Up' | 'Down';

interface IArticleRateCounterProps {
  vote?: RateVariants;
  rate: number;
  updateCounter: (rate: number, vote?: RateVariants) => void;
  afterRequest: () => Promise<void>;
  articleId: number;
}

const ArticlesRateCounter: FC<IArticleRateCounterProps> = ({
  vote,
  rate,
  updateCounter,
  afterRequest,
  articleId,
}) => {
  const { enqueueError } = useCustomSnackbar();

  function checkAuth() {
    if (!isAccessTokenValid()) {
      enqueueError('Будь ласка, авторизуйтесь');
      return false;
    }
    return true;
  }

  const ratingCountColor = () =>
    rate === 0 ? 'black' : rate > 0 ? '#09A552' : '#DF2638';

  async function handleUpVote(e: React.MouseEvent) {
    e.stopPropagation();
    if (!checkAuth()) return;

    const prevState = vote;
    const newState: { rate: number; vote?: RateVariants } = {
      rate: 0,
      vote: undefined,
    };

    if (prevState === 'Up') newState.vote = undefined;
    else newState.vote = 'Up';

    if (prevState === undefined) {
      newState.rate = rate + 1;
    }

    if (prevState === 'Down') {
      newState.rate = rate + 2;
    }

    if (prevState === 'Up') {
      newState.rate = rate - 1;
    }

    updateCounter(newState.rate, newState.vote);
    await afterRequest();
    await articlesApi.setRate(articleId, 'Up');
  }

  async function handleDownVote(e: React.MouseEvent) {
    e.stopPropagation();
    if (!checkAuth()) return;

    const prevState = vote;
    const newState: { rate: number; vote?: RateVariants } = {
      rate: 0,
      vote: undefined,
    };

    if (prevState === 'Down') newState.vote = undefined;
    else newState.vote = 'Down';

    if (prevState === undefined) {
      newState.rate = rate - 1;
    }

    if (prevState === 'Down') {
      newState.rate = rate + 1;
    }

    if (prevState === 'Up') {
      newState.rate = rate - 2;
    }
    console.log({
      vote,
      rate,
      newState: newState.rate,
      newStateVote: newState.vote,
    });
    updateCounter(newState.rate, newState.vote);
    await afterRequest();
    await articlesApi.setRate(articleId, 'Down');
  }

  return (
    <Actions className={cl.rating}>
      <div onClick={handleDownVote}>
        <SvgSelector
          id={'ArrowDown'}
          className={vote === 'Down' ? cl.ratingDown : ''}
        />
      </div>
      <Text
        as={'p'}
        className={cl.ratingCount}
        style={{ color: ratingCountColor() }}
      >
        {rate}
      </Text>
      <div onClick={handleUpVote}>
        <SvgSelector
          id={'ArrowUp'}
          className={vote === 'Up' ? cl.ratingUp : ''}
        />
      </div>
    </Actions>
  );
};

export default ArticlesRateCounter;
