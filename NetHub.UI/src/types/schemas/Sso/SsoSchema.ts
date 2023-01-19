import {z as u} from "zod";
import {userApi} from "../../../api/api";
import {onlyLettersRegex, usernameRegex} from "../../../utils/regex";
import {ProviderType} from "../../ProviderType";

export type SsoRequest = u.infer<typeof SsoRequestSchema>;

export const SsoRequestSchema = u.object({
  username: u.string()
    .min(3, 'Довжина імені користувача повинна бути від 3 до 12')
    .max(12, 'Довжина імені користувача повинна бути від 3 до 12')
    .regex(usernameRegex, 'Невірно введене ім\'я користувача')
    .refine(async (username) => {
      return await userApi.checkUsername(username);
    }, 'Ім\'я користувача вже використовується'),
  email: u.string()
    .email('Невірна поштова скринька')
    .min(7, 'Невірна поштова скринька'),
  firstName: u.string().min(2, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введене'),
  lastName: u.string().min(2, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введене'),
  middleName: u.string().min(5, 'Занадто коротке')
    .regex(onlyLettersRegex, 'Невірно введено')
    .optional(),
  profilePhotoUrl: u.string()
    .nullable(),
  providerMetadata: u.any(),
  provider: u.nativeEnum(ProviderType),
  providerKey: u.string(),
  type: u.enum(['register', 'login']).optional()
})