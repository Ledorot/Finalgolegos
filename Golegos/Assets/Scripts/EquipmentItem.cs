using UnityEngine;
using System;
using Golegos.Enums;
using System.Collections.Generic;

namespace Golegos
{
	[CreateAssetMenu (fileName = "New Equipment Item", menuName = "Golegos/Equipment Item", order = 2)]
	public class EquipmentItem : ScriptableObject
	{
		public String equipmentName;
		public String description;
		public SpecialAttack attack;
		public EquipmentType type;
		public List<Effect> effects;
	}
}