using System.Linq;
using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;

namespace Arimah
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.arimah.PerfectedLoop", "PerfectedLoop", "1.0.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    [R2APISubmoduleDependency(nameof(EliteAPI))]
    public class PerfectedLoop : BaseUnityPlugin
    {
        /// <summary>
        /// The vanilla game's elite tier for post-loop elites (Malachite and Celestine).
        /// </summary>
        private const int VanillaLoopEliteTier = 3;

        public void Awake()
		{
            var eliteTiers = EliteAPI.GetCombatDirectorEliteTiers();
            // Find the elite tier that contains Poison (Malachite) and Haunted (Celestine).
            // This tier is used after looping.
            var loopEliteTier = eliteTiers.FirstOrDefault(tier =>
                tier.eliteTypes.Contains(RoR2Content.Elites.Poison) &&
                tier.eliteTypes.Contains(RoR2Content.Elites.Haunted)
            );

            if (loopEliteTier == null)
			{
                Logger.LogDebug("Loop elites not found through search; falling back to vanilla index");
                loopEliteTier = eliteTiers[VanillaLoopEliteTier];
			}

            loopEliteTier.eliteTypes = loopEliteTier.eliteTypes.Concat(new[] {
                RoR2Content.Elites.Lunar
            }).ToArray();
            Logger.LogMessage("Perfected elites should now appear after looping");
        }
    }
}
