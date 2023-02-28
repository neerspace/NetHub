import React, { useCallback } from 'react';
import { useQueryClient } from 'react-query';
import { QueryClientConstants } from '../../constants/queryClientConstants';
import useCustomSnackbar from '../../hooks/useCustomSnackbar';
import { useDebounce } from '../../hooks/useDebounce';
import { useAppStore } from '../../store/config';
import { ProfileSchema } from '../../types/schemas/Profile/ProfileSchema';
import { usernameDebounce } from '../../utils/debounceHelper';
import { JWTStorage } from '../../utils/localStorageProvider';
import { ExtendedRequest, useProfileContext } from './ProfileSpace.Provider';
import { _currentUserApi, _jwtApi, _usersApi } from "../../api";
import { FileParameter, MeProfileUpdateRequest, UserResult } from "../../api/_api";
import { FilterInfo } from "../../types/api/IFilterInfo";

export async function getUserDashboard(username?: string) {
  return username ?
    await _usersApi.dashboard(username)
    : await _currentUserApi.dashboard();
}

export async function getUserInfo(setRequest: (value: ExtendedRequest) => void, username?: string) {
  if (username)
    return (await _usersApi.usersInfo([username]))[0];

  const me = await _currentUserApi.me();
  setRequest({
    username: me.userName,
    email: me.email,
    image: '',
    firstName: me.firstName,
    lastName: me.lastName,
    middleName: me.middleName,
    description: me.description ?? ''
  })

  return me;
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
  const oldUserInfo = userAccessor.data! as UserResult;

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
              await _currentUserApi.updateProfile(new MeProfileUpdateRequest(
                {
                  username: null,
                  firstName: changeRequest.firstName,
                  lastName: changeRequest.lastName,
                  middleName: changeRequest.middleName ?? null,
                  description: changeRequest.description ?? null
                })
              );
              break;
            case 'photo':
              if (typeof (changeRequest.image) === 'string') {
                newProfileImage = (await _currentUserApi.updateProfilePhoto(changeRequest.image, undefined)).link;
              } else {
                const request: FileParameter = {
                  data: changeRequest.image,
                  fileName: `${oldUserInfo.userName}-ProfilePhoto`
                }

                newProfileImage = (await _currentUserApi.updateProfilePhoto(undefined, request)).link;
              }
              break;
            case 'username':
              await _currentUserApi.updateProfile(new MeProfileUpdateRequest(
                {
                  username: changeRequest.username,
                  firstName: null,
                  lastName: null,
                  middleName: null,
                  description: null
                }
              ));
              break;
          }
        }
        updateProfileAction({
          ...reduxUser,
          firstName: changes.includes('profile') ? changeRequest.firstName : reduxUser.firstName,
          username: changes.includes('username') ? changeRequest.username : reduxUser.username,
          profilePhotoUrl: newProfileImage === '' ? reduxUser.profilePhotoUrl : newProfileImage
        });

        const jwt = await _jwtApi.refresh();
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