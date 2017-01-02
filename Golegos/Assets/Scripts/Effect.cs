using UnityEngine;
using System.Collections.Generic;
using Golegos.Enums;
using System;

namespace Golegos
{
	[Serializable]
	public class Effect
	{
		public EffectType type;
		public Int32 magnitude;

		public Effect(EffectType type, Int32 magnitude)
		{
			this.magnitude = magnitude;
			this.type = type;
		}
	}
}
