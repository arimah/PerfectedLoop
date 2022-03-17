using System;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RoR2;
using R2API.Utils;

namespace Arimah
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.arimah.PerfectedLoop", "PerfectedLoop", "1.2.1")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class PerfectedLoop : BaseUnityPlugin
    {
        /// <summary>
        /// The vanilla game's elite tier for post-loop elites (Malachite and Celestine).
        /// </summary>
        private const int VanillaLoopEliteTier = 3;

        private static bool hasRun = false;

        private static ManualLogSource instanceLogger;

        public void Awake()
        {
            // This is ugly
            instanceLogger = Logger;

            new Harmony("com.arimah.PerfectedLoop").PatchAll(typeof(PerfectedLoop));
        }

        [HarmonyPatch(typeof(CombatDirector), "ResetEliteType")]
        [HarmonyPrefix]
        public static void CombatDirector_ResetEliteType(CombatDirector.EliteTierDef[] ___eliteTiers)
        {
            if (hasRun)
            {
                return;
            }
            hasRun = true;

            try
            {
                var malachite = RoR2Content.Elites.Poison.eliteIndex;
                var celestine = RoR2Content.Elites.Haunted.eliteIndex;

                // Find the elite tier that contains Poison (Malachite) and Haunted (Celestine).
                // This tier is used after looping.
                var loopEliteTier = ___eliteTiers.FirstOrDefault(tier =>
                    2 == tier.eliteTypes.Count(t =>
                        t != null && (
                            t.eliteIndex == malachite ||
                            t.eliteIndex == celestine
                        )
                    )
                );

                if (loopEliteTier == null)
                {
                    instanceLogger?.LogDebug("Loop elites not found through search; falling back to vanilla index");
                    loopEliteTier = ___eliteTiers[VanillaLoopEliteTier];
                }

                loopEliteTier.eliteTypes = loopEliteTier.eliteTypes.Concat(new[] {
                    RoR2Content.Elites.Lunar
                }).ToArray();
                instanceLogger?.LogMessage("Perfected elites should now appear after looping");
            }
            catch (Exception e)
            {
                instanceLogger?.LogError(e);
            }
        }
    }
}
