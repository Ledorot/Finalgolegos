using System;
using UnityEngine;
using Golegos.Enums;

namespace Golegos
{
	[System.Serializable]
	public class EquipmentSlot
	{
		public Boolean enabled;
		public EquipmentItem item;

		[NonSerialized]
		public EquipmentType type;

		public EquipmentSlot(EquipmentType setType)
		{
			type = setType;
		}
	}
}