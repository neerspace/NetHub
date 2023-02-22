import { DateTime } from 'luxon';

export function getTimeFrom(date: DateTime) {
  const diff = DateTime.utc().diff(date, ['years', 'months', 'weeks', 'days', 'hours', 'minutes', 'seconds']).toObject();

  if (diff.years! > 0)
    return diff.months! > 0
      ? getGateString(diff.years!, 'year') + ' and ' + getGateString(diff.months!, 'month')
      : getGateString(diff.years!, 'year');

  if (diff.months! > 0)
    return diff.weeks! > 0
      ? getGateString(diff.months!, 'month') + ' and ' + getGateString(diff.weeks!, 'week')
      : getGateString(diff.months!, 'month');

  if (diff.weeks! > 0)
    return diff.days! > 0
      ? getGateString(diff.weeks!, 'week') + ' and ' + getGateString(diff.days!, 'day')
      : getGateString(diff.weeks!, 'week');

  if (diff.days! > 0)
    return getGateString(diff.days!, 'day');

  if (diff.hours! > 0)
    return getGateString(diff.hours!, 'hour');

  if (diff.minutes! > 0)
    return getGateString(diff.minutes!, 'minute');

  return getGateString(Math.round(diff.seconds!), 'second');
}

function getGateString(value: number, period: string) {
  if (value !== 1)
    period += 's';

  return `${value} ${period}`;
}