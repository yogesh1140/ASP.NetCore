﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<Tsource, TDestination>();
        bool ValidMappingExistsFor<TSource, TDestination>(string fields);
    }
}
