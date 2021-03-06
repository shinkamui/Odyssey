﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Odyssey.Graphics.Shaders;
using Odyssey.Daedalus.Shaders.Nodes;
using Odyssey.Serialization;

namespace Odyssey.Daedalus.Shaders.Methods
{
    public class IntrinsicFunction : MethodBase
    {
        private int arguments;
        public int Arguments { get { return arguments; } }

        public IntrinsicFunction() : base()
        {
            arguments = 0;
            Name = "Undefined";
        }

        public IntrinsicFunction(string methodName, int arguments) : base(true)
        {
            Name = methodName;
            this.arguments = arguments;
        }
        
        public override string Body
        {
            get { return string.Format("Intrinsic Functions do not have a body"); }
        }

        public override void Serialize(BinarySerializer serializer)
        {
            base.Serialize(serializer);
            serializer.Serialize(ref arguments);
        }
    }
}
