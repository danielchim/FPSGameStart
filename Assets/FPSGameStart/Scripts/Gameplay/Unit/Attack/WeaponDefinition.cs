using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class WeaponDefinition
{
    [SerializeField]
    private int ammo;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float range;
    [SerializeField]
    private int damage;

    public WeaponDefinition(int _ammo, float _cooldown, float _range, int _damage)
    {
        this.ammo = _ammo;
        this.cooldown = _cooldown;
        this.range = _range;
        this.damage = _damage;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public float GetCoolDown()
    {
        return cooldown;
    }

    public void SpentAmmo()
    {
        //remove a bala que foi atirada
        this.ammo--;
    }

    public float GetRange()
    {
        return range;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void AddAmmo(int ammoAmount)
    {
        ammo += ammoAmount;
    }
}
