﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBD.API
{
    public interface IPasswordSupplier
    {
        string GetPassword();
    }
}
