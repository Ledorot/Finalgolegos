using UnityEngine;
using System.Collections.Generic;
using Golegos.Enums;

namespace Golegos
{
	[CreateAssetMenu (fileName = "New Encounter", menuName = "Golegos/Encounter", order = 4)]
	public class Encounter : ScriptableObject {
		public const int MAX_CHARACTERS = 3;

		public List<Character> enemies;
		public int coinReward;
		public EquipmentItem itemReward;
	}

}
