# Udon Scripts

This is a set of small scripts I've made for my maps as I wanted to have simple scripts, made for me, instead of using 1/4 of some overengineered stuff, they are clearly not pretty, very opinionated, but they were made for me so \*shrug*.

They should works ootb with minor adjustments, and some prefabs are provided for some scripts like settings or togglers etc.

Feel free to provide me improvements if wanted or something.

Nearly all scripts have logging with `[OTR_something]` to easily debug stuff from the logs.

I hope you find them useful for you.

## Separate projects
- [Camera System](https://github.com/rhaamo/CameraSystem) For worlds: light and full camera systems, fixed and handeld cameras with FOV adjustment
- [OttLogger](https://github.com/rhaamo/OttLogger) In-world console logger with class/category filtering

## Scripts

- `World Settings Manager`: Made to have a synced set of parameters for a world (toggle pens, polaroids, colliders, clocks, join alerts, etc.) usable from various panels or udon events, see the related prefab for uses
- `MirrorToggler`: Use Toggle buttons between off/hq/lq/transparent, also change the button color depending on which mirror is activated
- `QuickTP`: Small script used to have a quick TP list of buttons and... TP the user. On your `Button` set the `On Click ()` to the udon behavior that has the `QuickTP` script and set the `UdonBehaviour.SendCustomEvent` to one of the functions (`_tpToSpawn` etc.).
- `OttSendEventOnTrigger`: send an U# event when an user enter/stay/exit a collider (script needs to be on same GameObject as a box collider in trigger mode)
- `RespawnObject`: as the name implies
- `TVSwitcher`: used to switch a global Tv vs local one, in local tv elements put your local player stuff (main screen, controls and queue), in local tv manager your ProTV manager, button.. well.. and global tv elements your global tv screen. See the prefab `Settings panel 1`
- `Playerlist`, `PlayerItem`, `HiddenPlayerList`: The playerlist with teleport users function, hiding is handled through the world settings with a button that calls the `UdonBehaviour.SendCustomEvent`: `_ToggleHideState` on the `HiddenPlayerList` script
- `AdminManager`: Hide/show items on join if you are admin or no, requires [Varneon Groups](https://github.com/Varneon/UdonEssentials/tree/main/Assets/Varneon/Udon%20Prefabs/Essentials/Groups)
- `LadderTeleporter`: simple teleporter
- `ControlsMover`: used to move from one position to another a GameObject

## Prefabs

- `World Settings`: Panel that uses the `Settings Manager` U# script
- `Settings panel 1`: A local settings panel using `MirrorToggler`, `TVSwitcher`, a global player controls (ProTV) and some controls from the `Settings Manager` U# scripts
- `Settings panel 2`: A local settings panel using `TVScreenPosition` and `MirrorToggler` U# scripts
- `VRCMirror HQ Variant` and `VRCMirror LQ Variant`: Prefabs so my prefabs can reuse them
- `Playerlist management`: a Player list with teleport functions and admin/mods indications, based on [Varneon Playerlist](https://github.com/Varneon/UdonEssentials/tree/main/Assets/Varneon/Udon%20Prefabs/Essentials/Playerlist), requires [Varneon Groups](https://github.com/Varneon/UdonEssentials/tree/main/Assets/Varneon/Udon%20Prefabs/Essentials/Groups)
