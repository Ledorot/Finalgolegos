using UnityEngine;
using System;
using Golegos.Enums;
using System.Collections.Generic;

namespace Golegos
{
	/// <summary>
	/// A piece of gear or a weapon a character can wear/use.
	/// </summary>
	[CreateAssetMenu (fileName = "New Equipment Item", menuName = "Golegos/Equipment Item", order = 2)]
	public class EquipmentItem : ScriptableObject
	{
		// The in-game display name for the item.
		// This does not have to be a unique value.
		public String equipmentName;

		// The in-game description for the item.
		public String description;

		// A special attack granted by having the item equipped.
		public SpecialAttack attack;

		// The equipment slot taken up when this is equipped.
		public EquipmentType type;

		// A list of the effects equipping that item gives the character.
		public List<Effect> effects;
	}
}