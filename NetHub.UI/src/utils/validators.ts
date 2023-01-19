export const isNotNullOrWhiteSpace = (text: string) => {
  return text !== null && !!text.replace(/\s/g, '').length && text !== '';
}

export const isNotNull = (value: any) => {
  return value !== null && value !== undefined && Object.keys(value).length !== 0;
}

export const minLength = (length: number) => (text: string) => {
  return text.length >= length;
}

export const maxLength = (length: number) => (text: string) => {
  return text.length <= length;
}

export const arrayMinLength = (length: number) => (data: any[]) => {
  return data.length >= length;
}

export const regexTest = (regex: RegExp) => (text: string) => {
  return regex.test(text)
}
