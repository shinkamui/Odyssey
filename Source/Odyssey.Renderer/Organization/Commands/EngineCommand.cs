﻿using Odyssey.Engine;
using Odyssey.Graphics.Organization;
using SharpDX;

namespace Odyssey.Organization.Commands
{
    public abstract class EngineCommand : Command
    {
        protected IGraphicsDeviceService DeviceService { get; private set; }

        protected EngineCommand(IServiceRegistry services, CommandType type) : base(services, type)
        {
            DeviceService = Services.GetService<IGraphicsDeviceService>();
        }
    }
}
