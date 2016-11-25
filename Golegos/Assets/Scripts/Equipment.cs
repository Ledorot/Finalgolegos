using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Golegos;

[Serializable]
public class Equipment : ScriptableObject {
	public List<Boolean> typeFlags;
	public List<EquipmentItem> items;
}
