﻿using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessLayer : IBusinessLayer
    {
        IDataAccessLayer dataAccessLayer;
    }
}
