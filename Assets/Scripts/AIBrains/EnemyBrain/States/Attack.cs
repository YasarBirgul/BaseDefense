using Controllers.Soldier;
using Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class Attack : IState
    {
        private readonly Animator _animator;
        private readonly EnemyAIBrain _enemyAIBrain;
        private static readonly int _attack = Animator.StringToHash("Attack");
        private static readonly int _run = Animator.StringToHash("Run");
        private float _attackTimer = 1f;
        private const float _refreshValue = 1f;
        private bool _attackPlayer;
        public Attack(Animator animator,EnemyAIBrain enemyAIBrain)
        {
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
        }
        public void Tick()
        {
            _attackTimer -= Time.deltaTime;
            if (!(_attackTimer <= 0)) return;
            _enemyAIBrain.HitDamage();
            _animator.SetTrigger(_attack);
            _attackTimer = _refreshValue;
        }
        public void OnEnter()
        {
           
        }
        public void OnExit()
        {
            _animator.SetTrigger(_run);
        }
        
    }
}