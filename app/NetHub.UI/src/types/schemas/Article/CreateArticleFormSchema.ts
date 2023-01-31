import {z as u} from 'zod';
import {tagRegex, urlRegex} from '../../../utils/regex';

export const CreateArticleFormSchema = u.object({
  title: u.string()
    .min(10, 'Можлива кількість символів від 10 до 120')
    .max(120, 'Можлива кількість символів від 10 до 120'),
  description: u.string()
    .min(10, 'Можлива кількість символів від 10 до 300')
    .max(300, 'Можлива кількість символів від 10 до 300'),
  html: u.string()
    .min(1, 'Мінімільна кількість символів - 1000'),
  tags: u.string()
    .regex(tagRegex)
    .array()
    .min(3, 'Можлива кількість тегів - від 3 до 20')
    .max(20, 'Можлива кількість тегів - від 3 до 20'),
  originalLink: u.string()
    .regex(urlRegex, 'Не вірне посилання')
    .or(u.literal(''))
})