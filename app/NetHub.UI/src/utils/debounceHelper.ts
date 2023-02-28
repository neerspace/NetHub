import { z as u } from 'zod';
import { usernameRegex } from './regex';
import { _usersApi } from "../api";

export const usernameDebounce = async (username: string | null, setErrors: any, errors: any) => {
  const isUsernameValid = u.string().regex(usernameRegex).safeParse(username);

  if (username === null || username === '' || !isUsernameValid.success) {
    setErrors({
      ...errors,
      username: { _errors: ['Невірно введене ім\'я користувача'] }
    });
    return false;
  }

  const {isAvailable} = await _usersApi.checkUsername(username);

  if (!isAvailable) {
    setErrors({ ...errors, username: { _errors: ['Ім\'я користувача вже використовується'] } });
    return false;
  }

  setErrors({ ...errors, username: undefined });
  return true;
};
