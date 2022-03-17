# PerfectedLoop

This mod makes [Perfected elites][perfected] spawn in the game after [looping][]. This elite type normally only spawns on the Moon. Perfected elites will spawn under the same conditions as Malachite and Celestine elites. As a result, Perfected, Malachite and Celestine elites will be approximately evenly distributed once they start appearing.

![Perfected wisps facing the player](https://github.com/arimah/PerfectedLoop/blob/master/perfected_wisps.png?raw=true "Perfected wisps")

In addition, Perfected elites will very occasionally drop [Shared Design][] when killed.

![Shared Design item as a pickup](https://github.com/arimah/PerfectedLoop/blob/master/shared_design.png?raw=true "Shared Design")

Perfected elites continue to spawn on the Moon.

**Network compatibility:** In multiplayer, only the host needs the mod.

## Limitations & known issues

The mod looks for the elite tier that contains Malachite _and_ Celestine elites. If it can’t find it, it falls back to the vanilla game’s elite tier. Mods that alter the combat director’s elite tiers may cause this mod to behave unexpectedly.

## Patch notes

v1.2.1
* Fix elites not spawning at all with this mod enabled ([issue](https://github.com/arimah/PerfectedLoop/issues/1)). See [the pull request](https://github.com/arimah/PerfectedLoop/issues/2) for details.

v1.2.0 - *Survivors of the Void*

* Depend on `RoR2.dll` instead of `Assembly-CSharp.dll`.
* Remove unnecessary dependency on `UnityEngine.Networking.dll`, which seems to be gone.
* Update dependency versions in manifest.

v1.1.0

* Small rewrite to use R2API's `EliteAPI` instead of directly patching the combat director. This _should_ make the mod more compatible with others.

v1.0.0

* Initial version.

## Support

If the mod fails to work, you can [open a GitHub issue][issue]. You can also contact me on Discord at Arimah#0001. I am _not_ in any Risk of Rain modding community.

## Installation

Use a mod manager such as [Thunderstore Mod Manager][r2mm] or copy `PerfectedLoop.dll` to your plugins folder.

## License

The mod is licensed under the MIT license. Please see the license text for the full terms.

[perfected]: https://riskofrain2.fandom.com/wiki/Monsters#Perfected
[looping]: https://riskofrain2.fandom.com/wiki/Environments#Looping
[Shared Design]: https://riskofrain2.fandom.com/wiki/Shared_Design
[issue]: https://github.com/arimah/PerfectedLoop/issues/new
[r2mm]: https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager
