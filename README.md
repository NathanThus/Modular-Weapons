# Modular Weapons

## Importing

Please make sure you have the [Dependencies](https://github.com/NathanThus/Modular-Weapons/blob/develop/DEPENDENCIES.md) installed, before attempting to load the framework. This system comes with [UniTask](https://github.com/Cysharp/UniTask) and [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) integrated, though feel free to disable them if they are already in your project.

## Features

![image](https://github.com/NathanThus/Modular-Weapons/assets/99728206/8250fbb3-be82-478c-ad61-5542e207b370)

_Extensive list of stats to play with_

![image](https://github.com/NathanThus/Modular-Weapons/assets/99728206/21a888a2-7ac7-48c6-9271-1758dcf45134)

_Different Spread patterns_

![image](https://github.com/NathanThus/Modular-Weapons/assets/99728206/7c5db9e1-78cf-4958-841c-a3359228fe45)
_Range and spread indicator!_

## Use
Create an object, which will manage the weapons. Add the `Weapon Manager` compoment.

Add the `Gun` Component to any object that will act as the firearm. A seperate `Muzzle` Object will be where the bullets originate from. Add this to the `WeaponManager`'s weapon list.

Once `Gun` has been added:
If you are using the `Hitscan` bullets, simply add the `HitscanBullet` component to the `Gun` GameObject and assign it to the bullet slot.
If you are using the `ProjectileBullet` bullets, create a gameobject to act as the bullet and assign the `ProjectileBullet` component. Disable the object, and assign it to the bullet slot.

Pick the type of spread you want the firearm to have, and adjust the parameters to your liking.

Add the `Health` component (which requires a `Collider` and `Rigidbody`) to any enemies, and see the [README](https://github.com/NathanThus/Modular-Weapons/tree/develop/Assets/Modular%20Weapon%20Framework/Scripts/Health) for additional information.

A simple Demo scene is provided.
