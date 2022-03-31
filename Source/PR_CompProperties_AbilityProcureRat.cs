using System;
using RimWorld;

namespace ProcureRat
{
	public class PR_CompProperties_AbilityProcureRat : CompProperties_AbilityEffect
	{
		public PR_CompProperties_AbilityProcureRat()
		{
			this.compClass = typeof(PR_CompAbilityEffect_ProcureRat);
		}

		public int numRat = 3;
	}
}
