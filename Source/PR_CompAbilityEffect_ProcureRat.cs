using System;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse;

namespace ProcureRat
{
    public class PR_CompAbilityEffect_ProcureRat : CompAbilityEffect
    {
		public new PR_CompProperties_AbilityProcureRat Props
		{
			get
			{
				return (PR_CompProperties_AbilityProcureRat)this.props;
			}
		}
        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            return true;
            //return base.Valid(target, throwMessages);
        }
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);       
            Map map = this.parent.pawn.Map;

            IntVec3 intVec;
			RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
		
			PawnKindDef pawnKindDef = DefDatabase<PawnKindDef>.GetNamed("Rat");
			int num = this.Props.numRat;
			if (num >= 2)
			{
				this.SpawnAnimal(intVec, map, pawnKindDef, new Gender?(Gender.Female));
				this.SpawnAnimal(intVec, map, pawnKindDef, new Gender?(Gender.Male));
				num -= 2;
			}
			for (int i = 0; i < num; i++)
			{
				this.SpawnAnimal(intVec, map, pawnKindDef, null);
			}

            //Messages.Message("Success", null, MessageTypeDefOf.PositiveEvent);
            Find.LetterStack.ReceiveLetter("Success", PR_LetterDefOf.success_letter.description, PR_LetterDefOf.success_letter, null);
        }

        private void SpawnAnimal(IntVec3 location, Map map, PawnKindDef pawnKind, Gender? gender = null)
		{
			IntVec3 loc = CellFinder.RandomClosewalkCellNear(location, map, 12, null);
			Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(pawnKind, null, PawnGenerationContext.NonPlayer, -1, false, false, false, false, true, false, 1f, false, true, true, true, false, false, false, false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, gender, null, null, null, null, null, false, false, false));
			GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
			pawn.SetFaction(Faction.OfPlayer, null);
		}

		// private bool TryFindRandomPawnKind(Map map, out PawnKindDef kind)
		// {
		// 	return (from x in DefDatabase<PawnKindDef>.AllDefs
		// 	where x.RaceProps.Animal && x.RaceProps.wildness < 0.35f && map.mapTemperature.SeasonAndOutdoorTemperatureAcceptableFor(x.race) && x.race.tradeTags.Contains("AnimalFarm") && !x.RaceProps.Dryad
		// 	select x).TryRandomElementByWeight((PawnKindDef k) => this.SelectionChance(k), out kind);
		// }

		// private float SelectionChance(PawnKindDef pawnKind)
		// {
		// 	float num = 0.42000002f - pawnKind.RaceProps.wildness;
		// 	if (PawnUtility.PlayerHasReproductivePair(pawnKind))
		// 	{
		// 		num *= 0.5f;
		// 	}
		// 	return num;
		// }

    }
}