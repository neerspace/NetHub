export const ErrorsHandler = {
  localization: (status: number) => {
    return localization[status] || 'Упс, щось пішло не так'
  },
  'default': (status: number) => {
    return defaultErrors[status] || 'Упс, щось пішло не так'
  }
}

const localization: Record<number, string> = {
  404: 'Упс, дана стаття ще пишеться',
  403: 'Упс, Ви не маєте доступу до цієї статті'
};

const defaultErrors: Record<number, string> = {
  400: 'Упс, помилка, спробуйте ще раз',
  404: 'Упс, такої інформації не існує',
  403: 'Упс, у Вас не достатньо прав',
}