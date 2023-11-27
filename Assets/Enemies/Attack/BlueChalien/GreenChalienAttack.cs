namespace Enemies.Attack
{
    public class GreenChalienAttack : EnemyAttack
    {
        private void Start()
        {
            _patternType = PatternType.None;
            _atkSpeed = 0.3f;
        }
    }
}
