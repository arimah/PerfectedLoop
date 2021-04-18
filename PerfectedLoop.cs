using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using R2API.Utils;
using RoR2;

#pragma warning disable IDE0051 // Remove unused private members

namespace Arimah
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.arimah.PerfectedLoop", "PerfectedLoop", "1.0.0")]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    public class PerfectedLoop : BaseUnityPlugin
    {
        public void Awake()
		{
            new Harmony("com.arimah.PerfectedLoop").PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(CombatDirector))]
    internal class CombatDirectorPatch
	{
        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        private static void Init()
        {
            using var logger = Logger.CreateLogSource("PerfectedLoop");
            var combatDirectorType = typeof(CombatDirector);
            var eliteTiers = combatDirectorType.GetFieldValue<CombatDirector.EliteTierDef[]>("eliteTiers");
            // Find the elite tier that contains Poison (Malachite) and Haunted (Celestine).
            // This tier is used after looping.
            var loopEliteTier = eliteTiers.FirstOrDefault(tier =>
                tier.eliteTypes.Contains(RoR2Content.Elites.Poison) &&
                tier.eliteTypes.Contains(RoR2Content.Elites.Haunted)
            );
            if (loopEliteTier != null)
            {
                loopEliteTier.eliteTypes = loopEliteTier.eliteTypes.Concat(new[] {
                    RoR2Content.Elites.Lunar
                }).ToArray();
                logger.LogMessage("Perfected elites will now appear after looping");
            }
            else
            {
                logger.LogWarning("Could not find loop elite tiers");
            }
        }
    }
}
