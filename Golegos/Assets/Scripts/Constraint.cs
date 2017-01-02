using UnityEngine;
using System.Collections;
using System;
using Golegos.Enums;

namespace Golegos
{
	/// <summary>
	/// Rules to follow for special attacks.
	/// </summary>
	[Serializable]
	public class Constraint
	{
		public ConstraintType type;
		public Int32 value;
	}
}
