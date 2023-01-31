import {literal, z as u} from 'zod';
import {onlyLettersRegex, usernameRegex} from "../../../utils/regex";

export type ProfileRequest = u.infer<typeof ProfileSchema>;

export const ProfileSchema = u.object({
  username: u.string()
    .min(3, 'Довжина імені користувача повинна бути від 3 до 12')
    .max(12, 'Довжина імені користувача повинна бути від 3 до 12')
    .regex(usernameRegex, 'Невірно введене ім\'я користувача'),
  // .refine(async (username) => {
  //   return await userApi.checkUsername(username);
  // }, 'Ім\'я користувача вже використовується'),
  email: u.string()
    .email('Невірна поштова скринька')
    .min(7, 'Невірна поштова скринька'),
  firstName: u.string().min(2, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введене'),
  lastName: u.string().min(2, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введене'),
  middleName: u.string().min(5, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введено')
    .or(literal('')),
  description: u.string().max(600, 'Занадто довгий').or(literal('')),
})