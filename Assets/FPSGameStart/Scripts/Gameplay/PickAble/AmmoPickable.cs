using UnityEngine;
using System.Collections;
using Gameplay.Unit;
using System;

namespace Gameplay
{
    public class AmmoPickable : BasePickAble
    {
        private int ammoAmount = 30;

        protected override void OnPicked(PlayerUnit playerUnit)
        {
            playerUnit.GetAimController.CurrentWeapon.AddAmmo(ammoAmount);
        }
    }
}
