import React, { FC } from 'react';
import cl from "./ArticleSavingActions.module.sass";
import IconButton from "../../UI/IconButton/IconButton";
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import { useQueryClient } from "react-query";
import Actions from "../../UI/Action/Actions";
import { QueryClientKeysHelper } from "../../../utils/QueryClientKeysHelper";

interface ISavingActionsProps {
  articleSetId: number,
  articleLanguage: string,
  isSavedDefault: boolean,
  onSave: () => Promise<void>,
  saveLink?: string,
  disabled?: boolean
}

const ArticleSavingActions: FC<ISavingActionsProps> = ({articleSetId, articleLanguage, isSavedDefault, onSave, saveLink}) => {

  const {enqueueSnackBar: enqueueSuccess, enqueueError} = useCustomSnackbar('success');
  const queryClient = useQueryClient();

  async function handleOnSave(e: React.MouseEvent) {
    e.stopPropagation()
    await onSave();
    await queryClient.invalidateQueries(QueryClientKeysHelper.Keys.articles);
    await queryClient.invalidateQueries(QueryClientKeysHelper.ArticlesByYou());
    await queryClient.invalidateQueries(QueryClientKeysHelper.SavedArticles());
    await queryClient.invalidateQueries(QueryClientKeysHelper.Article(articleSetId, articleLanguage))
  }

  function copyToClipboard(e: React.MouseEvent) {
    e.stopPropagation()
    navigator.clipboard.writeText(saveLink ?? window.location.href)
      .then(() => enqueueSuccess('Посилання скопійовано'))
      .catch(() => enqueueError('Помилка копіювання'))
  }

  return (
    <Actions className={cl.actionsRight}>
      <IconButton
        iconId={'ExternalLink'}
        checkAuth={false}
        onClick={copyToClipboard}/>
      <IconButton
        iconId={'SavedOutlined'}
        filledIconId={'SavedOutlinedFilled'}
        defaultState={isSavedDefault}
        onClick={handleOnSave}
      />
    </Actions>
  );
};

export default ArticleSavingActions;
