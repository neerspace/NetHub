import {DateTime} from "luxon";

export function DateToRelativeCalendar(date: string) {
  return DateTime.fromISO(date).toRelativeCalendar();
}
