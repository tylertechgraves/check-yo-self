export class Parameters {
  constructor(
      public datesParameters: DatesParameters,
      public monthsParameters: MonthsParameters
  ) {}
}

export class DatesParameters {
  constructor(
    public startDate: Date,
    public numberOfYearsToCheck: number,
    public payPeriodDays: number
  ) {}
}

export class MonthsParameters {
  constructor(
    public startDate: Date,
    public numberOfChecksInAGivenMonth: number,
    public payPeriodDays: number
  ) {}
}

export class Month {
  constructor(
    public description: string
  ) {}
}

export class Paycheck {
  constructor(
    public paymentDate: Date,
    public monthSubordinal: number
  ) {}
}
