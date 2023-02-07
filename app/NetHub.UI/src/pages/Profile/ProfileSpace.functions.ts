import React, { useCallback } from 'react';
import { useQueryClient } from 'react-query';
import { jwtApi, userApi } from '../../api/api';
import { QueryClientConstants } from '../../constants/queryClientConstants';
import useCustomSnackbar from '../../hooks/useCustomSnackbar';
import { useDebounce } from '../../hooks/useDebounce';
import { useAppStore } from '../../store/config';
import { ProfileSchema } from '../../types/schemas/Profile/ProfileSchema';
import { usernameDebounce } from '../../utils/debounceHelper';
import { JWTStorage } from '../../utils/localStorageProvider';
import { ExtendedRequest, useProfileContext } from './ProfileSpace.Provider';
import { _currentUserApi, _usersApi } from "../../api";
import { FileParameter } from "../../api/_api";

export async function getUserDashboard(username?: string) {
  return username ?
    await _usersApi.dashboard(username)
    : await _currentUserApi.dashboard();
}

export async function getUserInfo(username?: string) {
  return username ? (await userApi.getUsersInfo([username]))[0] : await userApi.me();
}

export const useProfileUpdateFunctions = (errors: any, setErrors: any, handleSettingsButton: () => void) => {

  const {enqueueError, enqueueSuccess, enqueueSnackBar} = useCustomSnackbar('info');
  const debounceLogic = async (username: string | null) => await usernameDebounce(username, setErrors, errors);
  const debounce = useDebounce(debounceLogic, 1000);
  const {
    changeRequest,
    setChangeRequest,
    changes,
    setChanges,
    addChanges,
    removeChanges,
    userAccessor
  } = useProfileContext();
  const queryClient = useQueryClient();
  const {updateProfile: updateProfileAction, user: reduxUser} = useAppStore();
  const oldUserInfo = userAccessor.data!;

  const handleUpdateUsername = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newUsername = event.target.value.toLowerCase();

    setChangeRequest({...changeRequest, username: newUsername});

    if (newUsername === reduxUser.username) {
      removeChanges('username');
      setErrors({...errors, username: undefined});
      return;
    }

    addChanges('username');
    debounce(newUsername, true);
  };

  const handleUpdateProfileInfo = useCallback((newUserInfo: ExtendedRequest) => {
    setChangeRequest(newUserInfo);
    if (oldUserInfo.firstName === newUserInfo.firstName &&
      oldUserInfo.lastName === newUserInfo.lastName &&
      oldUserInfo.middleName === newUserInfo.middleName &&
      oldUserInfo.description === newUserInfo.description) {
      removeChanges('profile');
      return;
    }
    addChanges('profile');
  }, [oldUserInfo, addChanges, removeChanges, setChangeRequest]);

  const handleValidateUpdate = async (): Promise<boolean> => {
    const validationResult = await ProfileSchema.safeParseAsync(changeRequest);

    if (!validationResult.success) {
      const errors = validationResult.error.format();
      setErrors(errors);
      return validationResult.success;
    }

    if (reduxUser.username !== changeRequest.username) {
      const isUsernameValid = await debounceLogic(changeRequest.username);
      if (!isUsernameValid) {
        setErrors({...errors, username: {_errors: ['Ім\'я користувача вже використовується']}});
        return isUsernameValid;
      }
    }

    setErrors({_errors: []});

    return validationResult.success;
  };

  const updateProfile = async () => {
      if (changes.length === 0) return;

      enqueueSnackBar('Завантаження...');
      const isProfileValid = await handleValidateUpdate();

      if (!isProfileValid) {
        enqueueError('Перевірте дані та спробуйте ще раз');
        return;
      }

      let newProfileImage = '';
      try {
        for (const change of changes) {
          switch (change) {
            case 'profile':
              await _currentUserApi.updateProfile(...changeRequest);
              break;
            case 'photo':
              if (typeof (changeRequest.image) === 'string') {
                newProfileImage = _currentUserApi.updateProfilePhoto(changeRequest.image);
              } else {
                const request: FileParameter = {
                  data: changeRequest.image,
                  name: `${oldUserInfo.userName}-ProfilePhoto`
                }

                newProfileImage = _currentUserApi.updateProfilePhoto(request);
              }
              break;
            case 'username':
              await _currentUserApi.updateProfile(changeRequest.username);
              break;
          }
        }
        updateProfileAction({
          ...reduxUser,
          firstName: changes.includes('profile') ? changeRequest.firstName : reduxUser.firstName,
          username: changes.includes('username') ? changeRequest.username : reduxUser.username,
          profilePhotoUrl: newProfileImage === '' ? reduxUser.profilePhotoUrl : newProfileImage
        });

        const jwt = await jwtApi.refresh();
        JWTStorage.setTokensData(jwt);
      } catch
        (e) {
        enqueueError('Помилка оновлення');
        return;
      }

      await queryClient.invalidateQueries([QueryClientConstants.user, oldUserInfo.userName]);
      setChanges([]);
      handleSettingsButton();
      enqueueSuccess('Зміни застосовані');
    }
  ;

  return {
    handleUpdateUsername,
    handleUpdateProfileInfo,
    updateProfile
  };
};