﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class PropertyMappingValue
    { 
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; private set; }
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert=false)
        {
            DestinationProperties = destinationProperties;
            Revert = revert;
        }

    }
}
