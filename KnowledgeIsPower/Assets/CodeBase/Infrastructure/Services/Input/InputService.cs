using Plugins.SimpleInput.Scripts;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Attack = "Attack";

        public abstract Vector2 Axis { get; }

        public abstract bool IsAttackButtonDown();

        protected static Vector2 SimpleInputAxis =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}