using UnityEngine;

namespace Enemies.Attack
{
    public class BlueChalienAttack : EnemyAttack
    {
        private void Start()
        {
            _patternType = PatternType.Circle;
            _atkSpeed = 0.4f;

            _startAngle += 10f;
            _endAngle += 10f;
        }
    }
}
