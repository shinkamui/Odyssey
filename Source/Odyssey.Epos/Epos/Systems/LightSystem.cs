﻿using System.Linq;
using Odyssey.Engine;
using Odyssey.Epos.Messages;
using Odyssey.Epos.Nodes;
using Odyssey.Epos.Components;
using System.Collections.Generic;

namespace Odyssey.Epos.Systems
{
    public abstract class LightSystem : UpdateableSystemBase
    {
        readonly LightCollection lightNodes;

        protected LightCollection LightNodes { get { return lightNodes; } }

        protected LightSystem(Selector selector) : base(selector)
        {
            lightNodes = new LightCollection();
        }

        protected void RemoveEntity(Entity entity)
        {
            var node = (from kvp in lightNodes where kvp.Value.EntityId == entity.Id select kvp.Value).First();
            lightNodes.Remove(node.Id);
            Messenger.Send(new LightMessage(entity, node, UpdateType.Remove));
        }

        public override void Start()
        {
            Messenger.Register<EntityChangeMessage>(this);
            Services.AddService(typeof(ILightService), lightNodes);
        }

        public override void Stop()
        {
            Messenger.Unregister<EntityChangeMessage>(this); 
            Services.RemoveService(typeof(ILightService));
        }

    }
}
