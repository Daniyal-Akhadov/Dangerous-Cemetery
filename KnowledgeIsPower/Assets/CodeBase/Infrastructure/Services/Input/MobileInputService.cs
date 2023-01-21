using Plugins.SimpleInput.Scripts;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis =>
            SimpleInputAxis;

        public override bool IsAttackButtonDown() =>
            SimpleInput.GetButtonDown(Attack);
    }
}