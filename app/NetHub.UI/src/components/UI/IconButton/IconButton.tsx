import React, {FC, useEffect, useState} from 'react';
import SvgSelector from "../SvgSelector/SvgSelector";
import cl from '../UiComps.module.sass';
import useCustomSnackbar from "../../../hooks/useCustomSnackbar";
import { JwtHelper } from "../../../utils/JwtHelper";

interface IIconButtonProps {
  iconId: string,
  onClick: (e: React.MouseEvent) => void,
  filledIconId?: string,
  defaultState?: boolean,
  checkAuth?: boolean
}

const IconButton: FC<IIconButtonProps> = ({iconId, onClick, filledIconId, defaultState, checkAuth = true}) => {
  const [isActive, setIsActive] = useState<boolean>(defaultState ?? false);

  useEffect(() => {
    setIsActive(defaultState ?? false)
  }, [defaultState])

  const {enqueueError} = useCustomSnackbar();

  function isAuthorized() {
    const tokenValid = JwtHelper.isAccessTokenValid();
    if (!tokenValid) {
      enqueueError('Будь ласка, авторизуйтесь');
    }

    return tokenValid
  }

  async function onClickHandle(e: React.MouseEvent) {
    if(checkAuth)
      if (!isAuthorized())
        return;

    if (filledIconId)
      setIsActive(prev => !prev);

    await onClick(e)
  }

  return (
    <div onClick={onClickHandle} className={cl.iconButton}>
      {filledIconId ? <SvgSelector id={isActive ? filledIconId : iconId}/>
        : <SvgSelector id={iconId}/>
      }
    </div>
  );
};

export default IconButton;
