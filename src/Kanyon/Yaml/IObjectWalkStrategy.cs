﻿using System;
using System.Reflection;

namespace Kanyon.Yaml
{
    public interface IObjectWalkStrategy
    {
        bool CanWalk(PropertyInfo info);
        void Walk(object o, PropertyInfo pi);
    }
}
