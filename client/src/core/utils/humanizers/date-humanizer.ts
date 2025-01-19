import { DateTime } from 'luxon';

export const humanizeDate = (value: string): string => {
  return DateTime.fromISO(value).toFormat('dd-MM-yy');
}