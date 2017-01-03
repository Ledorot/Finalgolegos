using UnityEngine;
using System.Collections;

namespace Golegos
{
	// Used to track special attack usage in combat.
	[System.Serializable]
	public class BattleSpecialAttack
	{
		int currentUse;
		int maxUses;
		private SpecialAttack innerAttack;
	}
}