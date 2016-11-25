using System;
using Golegos.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Golegos
{
	[CreateAssetMenu (fileName = "New Special Attack", menuName = "Golegos/Special Attack", order = 3)]
	public class SpecialAttack : ScriptableObject
	{
		public String attackName;
		public String description;
		public Int32 diceToRoll = 1;
		public Int32 maxUses;
		 
		public List<Effect> effects;
		public List<Constraint> constraints;
	}
}
