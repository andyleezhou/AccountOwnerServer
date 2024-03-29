﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class OwnerExtensions
    {
        public static void Map(this Owner dbOwner, Owner owner)
        {
            dbOwner.Name = owner.Name;
            dbOwner.DateOfBirth = owner.DateOfBirth;
            dbOwner.Address = owner.Address;
        }
    }
}
