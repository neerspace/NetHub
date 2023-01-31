import ILocalization, {IArticleFormErrors} from "../../../types/ILocalization";
import {arrayMinLength, isNotNullOrWhiteSpace, maxLength, minLength} from "../../../utils/validators";
import {IValidator} from "../../../hooks/useValidator";
import React from 'react';
import ErrorMessage from "../../../components/UI/Snackbar/ErrorMessage";

export function getArticleValidators(article: ILocalization):
  { value: any, field: keyof IArticleFormErrors, validators: IValidator[], message?: any }[] {
  return [
    {
      value: article.title,
      field: 'title',
      validators: [isNotNullOrWhiteSpace, minLength(10), maxLength(120)],
      message: <ErrorMessage title={'Заголовок'} message={'Можлива кількість символів від 10 до 120'}/>,
    },
    {
      value: article.description,
      field: 'description',
      validators: [isNotNullOrWhiteSpace, maxLength(300), minLength(10)],
      message: <ErrorMessage title={'Опис'} message={'Можлива кількість символів від 10 до 300'}/>
    },
    {
      value: article.html,
      field: 'html',
      validators: [isNotNullOrWhiteSpace, minLength(1)],
      message: <ErrorMessage title={'Тіло статті'} message={'Мінімільна кількість символів - 1000'}/>
    },
    {
      value: article.tags,
      field: 'tags',
      validators: [arrayMinLength(3)],
      message: 'Мінімальна кількість тегів - 3'
    }
  ]
}
