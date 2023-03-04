import { Text } from '@chakra-ui/react';
import React, { FC } from 'react';
import useCustomSnackbar from '../../../hooks/useCustomSnackbar';
import Actions from '../../UI/Action/Actions';
import SvgSelector from '../../UI/SvgSelector/SvgSelector';
import cl from './ArticleRateCounter.module.sass';
import { _myArticlesApi } from "../../../api";
import { Vote } from "../../../api/_api";
import { JwtHelper } from "../../../utils/JwtHelper";

interface IArticleRateCounterProps {
  vote: Vote | null;
  rate: number;
  updateCounter: (rate: number, vote: Vote | null) => void;
  afterRequest: () => Promise<void>;
  articleId: number;
}

const ArticlesRateCounter: FC<IArticleRateCounterProps> = (
  {vote, rate, updateCounter, afterRequest, articleId}) => {
  const {enqueueError} = useCustomSnackbar();

  function isAuthorized() {
    const tokenValid = JwtHelper.isAccessTokenValid();
    if (!tokenValid) {
      enqueueError('Будь ласка, авторизуйтесь');
    }

    return tokenValid
  }

  const ratingCountColor = () =>
    rate === 0 ? 'black' : rate > 0 ? '#09A552' : '#DF2638';

  async function handleUpVote(e: React.MouseEvent) {
    e.stopPropagation();
    if (!isAuthorized()) return;

    const prevState = vote;
    const newState: { rate: number; vote: Vote | null } = {
      rate,
      vote: null,
    };

    if (prevState === 'Up') newState.vote = null;
    else newState.vote = Vote.Up;

    if (prevState === null) {
      newState.rate++;
    }

    if (prevState === 'Down') {
      newState.rate = rate + 2;
    }

    if (prevState === 'Up') {
      newState.rate--;
    }

    updateCounter(newState.rate, newState.vote);

    await afterRequest();
    await _myArticlesApi.updateVote(articleId, Vote.Up);
  }

  async function handleDownVote(e: React.MouseEvent) {
    e.stopPropagation();
    if (!isAuthorized()) return;

    const prevState = vote;
    const newState: { rate: number; vote: Vote | null } = {
      rate,
      vote: null,
    };

    if (prevState === 'Down') newState.vote = null;
    else newState.vote = Vote.Down;

    if (prevState === null) {
      newState.rate--;
    }

    if (prevState === 'Down') {
      newState.rate++;
    }

    if (prevState === 'Up') {
      newState.rate = rate - 2;
    }

    updateCounter(newState.rate, newState.vote);

    await afterRequest();
    await _myArticlesApi.updateVote(articleId, Vote.Down);
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
        style={{color: ratingCountColor()}}
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
