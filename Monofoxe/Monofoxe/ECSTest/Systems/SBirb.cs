﻿using System.Collections.Generic;
using Monofoxe.ECSTest.Components;
using Monofoxe.Engine;
using Monofoxe.Engine.ECS;

namespace Monofoxe.ECSTest.Systems
{
	public class SBirb : BaseSystem
	{
		public override string Tag => "birb";

		public override void Create(Component component) {}

		public override void Destroy(Component component) {}

		public override void Update(List<Component> components) {}
		
		public override void Draw(Component component)
		{
			var birb = (CBirb)component;

			birb.Owner.Depth = -(int)birb.Position.Y;
			DrawMgr.DrawSprite(birb.Spr, birb.Position);
		}
	}
}