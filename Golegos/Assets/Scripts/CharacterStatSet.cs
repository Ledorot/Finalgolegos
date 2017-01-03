using UnityEngine;
using System.Collections.Generic;

namespace Golegos
{
	[CreateAssetMenu (fileName = "New Stat Set", menuName = "Golegos/CharacterStatSet", order = 1)]
	public class CharacterStatSet : ScriptableObject
	{
		public string characterName;
		public int RollsPerTurn;
		public int AttackDice;
		public int DefenseDice;
		public List<int> HealthBars;
		public List<SpecialAttack> Attacks;
		public Equipment defaultEquipment;
	}
}