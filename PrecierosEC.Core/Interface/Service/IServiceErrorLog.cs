﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecierosEC.Core.Interface.Service
{
    public interface IServiceErrorLog
    {
        string SaveErrorlog(Exception ex);
    }
}
