using UnityEngine;
using System.Collections.Generic;

namespace Golegos
{
    //[CreateAssetMenu (fileName = "New Stat Set", menuName = "Golegos/CharacterStatSet", order = 1)]
    //public class CharacterStatSet : ScriptableObject
    public class CharacterStatSet : MonoBehaviour
    {
		public string characterName;
        public RectTransform battleSprite;
		public int RollsPerTurn;
		public int AttackDice;
		public int DefenseDice;
		public List<int> HealthBars;
        public List<Attack> Attacks;
        public List<SpecialAttack> SpecialAttacks;
		public Equipment defaultEquipment;

        //Return the name of the attack at the selected index
        public string GetAttackText(int index, bool isSpecial) {
            if (!isSpecial) {
                //Debug.Log("New attack!");
                if (Attacks.Count > index) {
                    return Attacks[index].attackName;
                }
                return null;
            }
            else {
                //Debug.Log("New special attack!");
                if (SpecialAttacks.Count > index) {
                    return SpecialAttacks[index].attackName;
                }
                return null;
            }
        }
    }
}