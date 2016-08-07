using UnityEngine;
using System.Collections;

public class Equipment : ScriptableObject {
    public EquipmentType EquipmentType;
}

public enum EquipmentType {
    Head,
    Neck,
    Chest,
    Back,
    Hand,
    OffHand,
    Legs
}