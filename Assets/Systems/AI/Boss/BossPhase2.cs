using UnityEngine;
using Systems;
using Enemies.Attack;
using Systems.EntityState;
using Sound;

namespace BehaviourTree
{
    public class BossPhase2 : Node
    {
        private Vector2 _center;
        private bool _isInCenter;
        private GameObject _boss;
        private float _atkSpeed = 0.03f;

        public BossPhase2(GameObject boss)
        {
            _center = CameraManager.Instance.GetCamera().ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            _boss = boss;
        }

        public override NodeState Evaluate()
        {
            state = NodeState.FAILURE;

            if (!HealthUnderTwentyFive())
            {
                GetIntoCenter();
            }

            return state;
        }

        private void GetIntoCenter()
        {
            if (!_isInCenter)
            {
                BossAttack bossAttack = _boss.GetComponent<BossAttack>();
                bossAttack.SetCanShoot(false);

                Vector3 direction = (_center - (Vector2)_boss.transform.position).normalized;
                _boss.transform.position += direction * 5f * Time.deltaTime;

                if (Vector3.Distance(_center, _boss.transform.position) < 0.2f)
                {
                    _boss.transform.position = _center;

                    bossAttack.SetCanShoot(true);
                    bossAttack.SetPattern(EnemyAttack.PatternType.Spiral);
                    bossAttack.SetAtkSpeed(_atkSpeed);

                    SoundManager.Instance.PlaySound("Boss Yelp");

                    _isInCenter = true;
                }
            }
        }

        private bool HealthUnderTwentyFive()
        {
            if (_boss.GetComponent<Health>().GetHealthPercentage() <= 25)
            {
                _boss.GetComponent<BossAttack>().SetCanShoot(false);

                state = NodeState.SUCCESS;
                return true;
            }
            else
            {
                state = NodeState.FAILURE;
                return false;
            }
        }
    }
}
