using System;
using System.Collections.Generic;
using UnityEngine;
using Golegos.Enums;

namespace Golegos {

    [CreateAssetMenu(fileName = "New Special Attack", menuName = "Golegos/Special Attack", order = 3)]
    public class Attack : ScriptableObject {
        public String attackName = "Attack";
        public String description = "The character's basic attack";
        public Int32 diceToRoll = 1;
        //public Int32 maxUses;

        public List<Effect> effects;
        public List<Constraint> constraints;
    }
}