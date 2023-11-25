namespace Enemies.Attack
{
    public class BossAttack : EnemyAttack
    {
        private void Start()
        {
            _patternType = PatternType.Circle;
            _atkSpeed = 0.3f;
        }
    }
}