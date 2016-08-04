using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu (fileName = "New Stat Set", menuName = "Golegos/CharacterStatSet", order = 1)]
public class CharacterStatSet : ScriptableObject {
	public string Weapon;
	public List<int> HealthBars;
	public int RollsPerTurn;
	public int Dice;
	public string SpecialAttack;
}
