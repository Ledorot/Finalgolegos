using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Golegos;

namespace Golegos {
    public class Battle : MonoBehaviour {

        //List of allies in the battle
        public List<CharacterStatSet> allies;
        //List of enemies in the battle
        public List<CharacterStatSet> defenders;
    }
}