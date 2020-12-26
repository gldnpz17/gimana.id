using DomainModel.Services;
using System;

namespace SimpleDomainServiceImplementation
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
