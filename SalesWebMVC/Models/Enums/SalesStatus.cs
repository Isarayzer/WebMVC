using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.Enums
{
    public enum SalesStatus : int
    {
        Pending = 0,
        Billed = 1,
        Canceled = 2
    }
}
