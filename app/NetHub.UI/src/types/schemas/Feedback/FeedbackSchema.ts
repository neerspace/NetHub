import {z as u} from 'zod';

export const FeedbackSchema = u.object({
  name: u.string().max(50, 'Максимальна довжина - 50'),
  email: u.string().email('Не вірна поштова скринька').max(50, 'Максимальна довжина - 50'),
  message: u.string().min(5, 'Мінімальна довжина - 5')
    .max(700, 'Максимальна довжина - 700')
})

export type FeedbackType = u.infer<typeof FeedbackSchema>;