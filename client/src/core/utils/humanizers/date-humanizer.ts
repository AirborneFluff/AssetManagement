export const humanizeDate = (value: string) => {
  return new Date(value).toDateString();
}