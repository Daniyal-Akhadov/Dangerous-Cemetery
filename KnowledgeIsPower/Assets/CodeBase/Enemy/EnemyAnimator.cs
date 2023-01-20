using System;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack1 = Animator.StringToHash("Attack_1");

        private static readonly int _idleStateHash = Animator.StringToHash("idle");
        private static readonly int _attackStateHash = Animator.StringToHash("attack 01");
        private static readonly int _walkingStateHash = Animator.StringToHash("walk");
        private static readonly int _deathStateHash = Animator.StringToHash("death");

        private Animator _animator;
        public AnimatorState State { get; private set; }

        private Action<AnimatorState> StateExited;
        private Action<AnimatorState> StateEntered;

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack1);

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving() =>
            _animator.SetBool(IsMoving, false);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            State = StateFor(stateHash);
            StateExited?.Invoke(State);
        }

        private static AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}