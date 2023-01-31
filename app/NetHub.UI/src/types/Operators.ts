export enum Operator {
  Equals = '==',
  NotEquals = '!=',
  EqualsIns = '==*',
  NotEqualsIns = '!=*',

  GreaterThen = '>',
  LessThen = '<',
  GreaterOrEquals = '>=',
  LessOrEquals = '<=',

  Contains = '@=',
  ContainsIns = '@=*',
  NotContains = '!@=',
  NotContainsIns = '!@=*',

  StartsWith = '_=',
  NotStartsWith = '!_=',
  StartsWithIns = '_=*',
  NotStartsWithIns = '!_=*',

  EndsWith = '_-=',
  NotEndsWith = '!_-=',
  EndsWithIns = '_-=*',
  NotEndsWithIns = '!_-=*',
}
