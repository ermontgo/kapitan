﻿using Kapitan.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kapitan.Filters
{
    public class CrdsOnlyManifestFilter : IManifestFilter
    {
        public bool Filter(IManifestObject manifestObject)
        {
            return manifestObject.Kind.Equals("CustomResourceDefinition", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}