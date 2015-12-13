using UnityEngine;
using System.Collections;
using Gameplay.Unit.Attack;
namespace Gameplay.Unit
{
    [RequireComponent(typeof(AimController))]
    public class PlayerUnit : NewBaseUnit
    {
        private AimController aimController;

        public AimController GetAimController { get { return aimController; } }

        protected override void Awake()
        {
            base.Awake();
            aimController = GetComponent<AimController>();
        }

        protected override void Start()
        {
            base.Start();
        }


    }
}
