using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDays.Domain
{
    public interface IHolidayRule
    {
        bool IsHoliday(DateTime date);
    }
}
