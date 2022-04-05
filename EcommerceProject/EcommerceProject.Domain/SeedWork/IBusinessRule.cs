﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.Domain.SeedWork
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}