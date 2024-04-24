# Health System

Feel free to adjust, replace or substitute this with a variety of heal scripts. The Modular Weapons package relies on the following parts of the code:

```C#

public event Action OnDeath;
public event Action OnHeal;
public event Action<float> OnDamage;

...

public virtual void Hit(float damage)
public virtual void Heal(float hitpoints)

```

With those elements kept in place (or substituted), the rest of the package should remain in perfect working order.
