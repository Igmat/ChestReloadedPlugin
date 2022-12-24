# Chest Reloaded

This mods aims to provide you with better options when you make your base and warehouse for it.

> **NOTE:** while mod can be added and removed safely mid-game. Removing it will destroy all existing custom lockers and their content.

## Features

1. New custom furniture pieces based on original Valheim aesthetics
   - **Big Wooden Locker** (costs 20 wood, 4 bronze nails, 4 coal)
   - **Signed Locker** (costs 10 wood, 2 core wood, 4 fine wood, 4 iron nails, 4 coal)
   - **Signed Iron Locker** (costs 12 fine wood, 3 iron, 4 iron nails, 4 coal)
   - **Hidden Stone Locker** (costs 4 stone, 4 iron, 4 chains)
2. Each locker adapts to sizes of vanilla chests
   - **Big Wooden Locker** twice bigger than usual Chest (20 slots by current vanilla sizes)
   - **Signed Locker** is between usual Chest and Iron Chest (16 slots by current vanilla sizes)
   - **Signed Iron Locker** is a little bit bigger than Iron Chest (28 slots by current vanilla sizes)
   - **Hidden Stone Locker** is much bigger than Iron Chest (36 slots by current vanilla sizes)
3. Each locker size can be configured in BepInEx menu (by default press **F1** to open it)
4. Each locker perfectly snaps to the floor, walls, or even other locker pieces
5. Lockers can be stacked on top of each other
6. You can assign a name to any locker using **ALT+E** hotkey (except Hidden Stone Locker)
7. You can add item name to your locker using **ALT+[1-8]** hotkey (this also applies to vanilla signs, but not to Hidden Stone Locker)
8. Hidden Stone Locker is different. Its purpose to be your secret personal storage.
   - It looks almost identical to usual stone wall (a little bit lighter)
   - It shows hover text (e.g. _"Press [E] to open"_) only to player who placed it
   - It can be opened only by player who placed it
   - The only way to discover it by another player is to try to **Use** each stone wall, because message "You can't open it" is left in place. Treat it as different sound on wall tapping when somebody looks for secret passage.

## Plans

- [x] Compatibility with [**Craft Build Smelt Cook Fuel Pull From Containers**](https://www.nexusmods.com/valheim/mods/40)
- [x] Compatibility with [**Smarter containers**](https://www.nexusmods.com/valheim/mods/332)
- [x] Big Wooden Locker
- [x] Hidden Stone Locker
- [x] Signed Iron Locker
- [x] Custom icons
- [ ] Animation for transition between open and closed states
- [ ] Calculating title by common types of assigned items (i.e. Ore, Leather Armor, Weapon)
- [ ] Add stuff (like bottles, boxes, wood, etc.) inside lockers for more immersive experience
- [ ] API for other mods to understand what should be stored in a particular locker

## Installation

Just use Vortex or manually download it from [Nexus](https://www.nexusmods.com/valheim/mods/653?tab=files) or [Github](https://github.com/Igmat/ChestReloadedPlugin/releases) and unzip it to your `Valheim\BepInEx` folder replacing files if it asks.

## Bugs

I'll really appreciate if you inform me about any problems you encounter with my mod. There are two options to help the project:

1. Fill bug report [on nexus mod page](https://www.nexusmods.com/valheim/mods/653?tab=bugs)
2. Create issue [in github repo](https://github.com/Igmat/ChestReloadedPlugin/issues/new)

## FAQ

1. I installed this mod but don't see new furniture in the build menu. What to do?
   - Check that you already discovered all required resources.
   - Check that you have the latest versions of [**Jötunn**](https://www.nexusmods.com/valheim/mods/1138)
2. There is new furniture in the build menu, but after placing it on the ground it looks weirdly transparent. What to do?
   - Check that you're NOT using Vulkan render (you might have chosen it while launching Valheim from Steam). I'm still trying to fix a problem with Vulkan render.

## Compatibility

1. ~~[**AutoPickupSelector**](https://www.nexusmods.com/valheim/mods/868?tab=description) causes lockers to disappear. Don't use both mods for now.~~ Should work with [**AutoPickupSelector Version 0.4**](https://www.nexusmods.com/valheim/mods/868?tab=description)
2. Compatible with [**Craft Build Smelt Cook Fuel Pull From Containers**](https://www.nexusmods.com/valheim/mods/40)
3. ~~Working on compatibility with [**Smarter containers**](https://www.nexusmods.com/valheim/mods/332). It doesn't cause any issues, but they aren't interacting with each other.~~ Should work with [**Smarter containers Version 1.6.0**](https://www.nexusmods.com/valheim/mods/332)
4. Works great with [**Valheim Plus**](https://www.nexusmods.com/valheim/mods/4), but after resolving of [**[FEATURE REQUEST] Refactor `GameClasses/Container.cs`**](https://github.com/valheimPlus/ValheimPlus/issues/478) it'll be even better since sizes of lockers will adjust to reconfigured sizes of vanilla chests. Its crafting from containers should work with lockers from this mod from next release of [**Valheim+**](https://www.nexusmods.com/valheim/mods/4) (more than 0.9.7).

## Your Contribution

Just endorse [me](https://www.nexusmods.com/valheim/users/17810659) and/or [this mod](https://www.nexusmods.com/valheim/mods/653?tab=description) on NexusMods.  
Or [star mod](https://github.com/Igmat/ChestReloadedPlugin) and [follow me](https://github.com/Igmat) on Github.  
Or even join to development to development proccess in [my repo](https://github.com/Igmat/ChestReloadedPlugin). If you are familiar with C# programming or with modeling in Unity you can be really helpfull:)

## Special thanks

Great thanks to [iDeathHD](https://github.com/xiaoxiao921) (a.k.a [xiaoxiao921](https://www.nexusmods.com/valheim/users/3635028)) for [his mods](https://www.nexusmods.com/valheim/users/3635028?tab=user+files) that were used to create this one and for his help answering my, sometimes dumb, questions:)
