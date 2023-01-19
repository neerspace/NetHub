import { DateTime } from 'luxon';
import { FormatterFunc } from './types';

const maxStringLength = 28;

export function formatAsDate(value: string | undefined): string {
  return value ? DateTime.fromISO(value).toFormat('dd/MM/yyyy HH:mm') : '–';
}

export function formatCheckmark(value: string | undefined): string {
  if (value) {
    return '<span class="check check-true">yes</span>';
  } else {
    return '<span class="check check-false">no</span>';
  }
}

export function formatAsText(value: string | undefined): string {
  if (!value) {
    return '–';
  }
  if (value.length > maxStringLength) {
    return value.substring(0, maxStringLength - 3) + '...';
  }
  return value;
}
