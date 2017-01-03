using UnityEngine;
using System;
using Golegos.Enums;

namespace Golegos
{
	[Serializable]
	public class Equipment
	{
		public EquipmentSlot hand;
		public EquipmentSlot torso;
		public EquipmentSlot neck;
		public EquipmentSlot head;
		public EquipmentSlot back;
		public EquipmentSlot legs;
		public EquipmentSlot offHand;

		public Equipment()
		{
			hand = new EquipmentSlot (EquipmentType.Hand);
			torso = new EquipmentSlot (EquipmentType.Torso);
			neck = new EquipmentSlot (EquipmentType.Neck);
			head = new EquipmentSlot (EquipmentType.Head);
			back = new EquipmentSlot (EquipmentType.Back);
			legs = new EquipmentSlot (EquipmentType.Legs);
			offHand = new EquipmentSlot (EquipmentType.OffHand);
		}

		public void Clear()
		{
			hand.item = null;
			torso.item = null;
			neck.item = null;
			head.item = null;
			back.item = null;
			legs.item = null;
			offHand.item = null;
		}

		public void CopyFrom(Equipment eqFrom)
		{
			if (eqFrom == null)
				return;

			EquipmentSlot slotFrom, slotTo;

			for (int i = 0; i < 7; i++) {
				switch (i) {
				case 0:
					slotFrom = eqFrom.hand;
					slotTo = hand;
					break;
				case 1:
					slotFrom = eqFrom.torso;
					slotTo = torso;
					break;
				case 2:
					slotFrom = eqFrom.neck;
					slotTo = neck;
					break;
				case 3:
					slotFrom = eqFrom.head;
					slotTo = head;
					break;
				case 4:
					slotFrom = eqFrom.back;
					slotTo = back;
					break;
				case 5:
					slotFrom = eqFrom.legs;
					slotTo = legs;
					break;
				case 6:
					slotFrom = eqFrom.offHand;
					slotTo = offHand;
					break;
				default:
					slotFrom = eqFrom.hand;
					slotTo = hand;
					break;
				}

				if (slotFrom.enabled) {
					slotTo.item = slotFrom.item;
				} else {
					slotTo.item = null;
				}
			}
		}
	}
}