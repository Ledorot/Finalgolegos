using UnityEngine;
using System.Collections;

namespace Golegos.Enums
{
	public enum ConstraintType
	{
		None,
		TotalEven,		// The die total has to be even.
		TotalOdd,		// The die total has to be odd.
		GreaterThan,	// The die total has to be greater than the value, not equal to.
		LessThan,		// The die total has to be less than the value, not equal to.
		EqualTo			// The die total has to be equal to the value.
	}
}
