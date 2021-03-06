﻿using Microsoft.Xna.Framework;
using Monofoxe.Engine;
using Monofoxe.Engine.Drawing;
using Monofoxe.Engine.EC;
using Monofoxe.Engine.Resources;
using Monofoxe.Engine.SceneSystem;

namespace Monofoxe.Playground.ECDemo
{
	public class Player : Entity
	{
		public const Buttons UpButton = Buttons.W;
		public const Buttons DownButton = Buttons.S;
		public const Buttons LeftButton = Buttons.A;
		public const Buttons RightButton = Buttons.D;

		Sprite _playerSprite;

		// The player uses hybrid EC - it's a derived entity with components inside.
		// You also can ditch components entirely and only use entities. 
		
		// I recommend useng hybrid entities in places, where EC is not entirely needed.
		// For example, if you know that this entity's code will not be reused anywhere else.

		public Player(Layer layer, Vector2 position) : base(layer)
		{
			_playerSprite = ResourceHub.GetResource<Sprite>("DefaultSprites", "Player");
			
			// You can add components right in the constructor.
			AddComponent(new PositionComponent(position));
			AddComponent(new ActorComponent(_playerSprite));
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		public override void Update()
		{
			base.Update();

			// Very basic controls.
			var movement = new Vector2(
				Input.CheckButton(RightButton).ToInt() - Input.CheckButton(LeftButton).ToInt(),
				Input.CheckButton(DownButton).ToInt() - Input.CheckButton(UpButton).ToInt()
			);

			// Telling our actor component to move in a specific direction.
			var actor = GetComponent<ActorComponent>();	
			actor.Move = movement != Vector2.Zero;
			actor.Direction = movement.ToAngle();
		}

		public override void Draw()
		{
			base.Draw();

			var position = GetComponent<PositionComponent>();

			// Layers and scenes have methods for searching entities/components.
			foreach(BotComponent bot in Layer.GetComponentList<BotComponent>())
			{
				var botPosition = bot.Owner.GetComponent<PositionComponent>();

				LineShape.Draw(position.Position, botPosition.Position, Color.Transparent, Color.White * 0.2f);
			}
			
		}
	}
}
