# Modular Weapons

## Importing

Please make sure you have the [Dependencies](https://github.com/NathanThus/Modular-Weapons/blob/develop/DEPENDENCIES.md) installed, before attempting to load the framework. This system comes with [UniTask](https://github.com/Cysharp/UniTask) and [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) integrated, though feel free to disable them if they are already in your project.

## Use
Add the `Gun` Component to any object that will act as the firearm. A seperate `Muzzle` Object will be where the bullets originate from.
Once `Gun` has been added, add a `Bullet` and `Spread` Component. These will determine the properties of the actual `Bullet` and `Bullet Spread`.

Add the `Health` component to any enemies, and see the [README](https://github.com/NathanThus/Modular-Weapons/tree/develop/Assets/Modular%20Weapon%20Framework/Scripts/Health) for additional information.
