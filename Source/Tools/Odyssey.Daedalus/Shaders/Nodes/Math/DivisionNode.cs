﻿using Odyssey.Graphics.Shaders;

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Odyssey.Daedalus.Shaders.Nodes.Math
{
    public class DivisionNode : MathNodeBase
    {
        protected override char GetOperator()
        {
            const char div = '/';
            return div;
        }
    }
}
