﻿using Odyssey.Engine;
using Odyssey.Organization.Commands;
using Odyssey.Talos.Messages;
using SharpDX;

namespace Odyssey.Talos.Systems
{
    public class RenderSystem : UpdateableSystemBase, IRenderableSystem
    {
        private readonly CommandManager commandManager;

        public RenderSystem() : base(Selector.None())
        {
            commandManager = new CommandManager();
        }

        public override void Start()
        {
            Messenger.Register<CommandUpdateMessage>(this);
        }

        public override void Stop()
        {
            Messenger.Unregister<CommandUpdateMessage>(this);
        }

        public bool BeginRender()
        {
            return true;
        }

        public void Render()
        {
            var deviceService = Scene.Services.GetService<IGraphicsDeviceService>();
            deviceService.DirectXDevice.Clear(Color.Black);
            foreach (Command command in commandManager)
                command.Execute();
        }

        public override void Unload()
        {
            commandManager.Unload();
        }

        public override bool BeforeUpdate()
        {
            if (MessageQueue.HasItems<CommandUpdateMessage>())
            {
                var mUpdate = MessageQueue.Dequeue<CommandUpdateMessage>();

                switch (mUpdate.UpdateType)
                {
                    case UpdateType.Add:
                        commandManager.AddLast(mUpdate.Commands);
                        commandManager.Initialize();
                        break;
                }
            }
            return base.BeforeUpdate();
        }

        public override void Process(ITimeService time)
        {
        }
    }
}
