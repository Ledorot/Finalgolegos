namespace Golegos.Enums
{
	public enum EffectType
	{
		extraAttackDice,	// Add more dice for attack based on magnitude value.
		extraDefenseDice,	// Add more dice for defense based on magnitude value.
		extraRolls,			// Add another roll per turn based on magnitude value.
		healing,			// Use dice total for healing.  Magnitude value does not matter.
		hitEveryone,		// Hits every character on the opposing team.  Magnitude value does not matter.
		bonusDamage,		// Extra damage factored in to the roll based on magnitude value.
		extraHealth,		// More health on the primary health bar based on magnitude value.
		extraHealthBar,		// Adds a whole extra health bar.  Extra bar health is based on magnitude value.
	}
}
