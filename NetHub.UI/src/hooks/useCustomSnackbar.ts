import {useSnackbar, VariantType} from "notistack";


const useCustomSnackbar = (variant?: VariantType) => {
  const {enqueueSnackbar} = useSnackbar();

  function enqueueError(data: any) {
    enqueueSnackbar(data, {variant: 'error'})
  }

  function enqueueSuccess(data: any) {
    enqueueSnackbar(data, {variant: 'success'})
  }

  function enqueueSnackBar(data: any) {
    enqueueSnackbar(data, {variant})
  }

  return {enqueueError, enqueueSuccess, enqueueSnackBar}
}

export default useCustomSnackbar;
