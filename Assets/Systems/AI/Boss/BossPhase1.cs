using Systems.EntityState;
using UnityEngine;
using Systems;
using Enemies.Attack;
using Sound;

namespace BehaviourTree
{
    public class BossPhase1 : Node
    {
        private bool _isInArena;
        private float _speed = 5f;
        private GameObject _boss;
        private Health _bossHealth;
        private Vector3 _phase1Pos;
        private Vector3 _direction;
        private float _boundOffset = 2f;
        private float _moveTiming = 0.2f;
        private float _coolDown = 0f;

        public BossPhase1(GameObject boss)
        {
            _boss = boss;
            _phase1Pos = new Vector3(0, 8f, 0);
            _bossHealth = _boss.GetComponent<Health>();
            _direction = Vector3.left;
        }

        public override NodeState Evaluate()
        {
            if (!HealthUnderFifty())
            {
                EnteringArena();
                Movement();
            }

            return state;
        }

        private void EnteringArena()
        {
            if (!_isInArena)
            {
                _boss.transform.position += Vector3.down * _speed * Time.deltaTime;

                if (Vector3.Distance(_boss.transform.position, _phase1Pos) <= 0.2f)
                {
                    _boss.transform.position = _phase1Pos;
                    SoundManager.Instance.PlaySound("Boss Yelp");

                    _isInArena = true;
                }
            }
        }

        private void Movement()
        {
            if (_isInArena)
            {
                _boss.transform.position += _direction * _speed * Time.deltaTime;

                if (_boss.transform.position.x < CameraManager.Instance.GetMinBound().x + _boundOffset|| _boss.transform.position.x > CameraManager.Instance.GetMaxBound().x - _boundOffset)
                {
                    if (Time.time > _coolDown)
                    {
                        _direction *= -1;
                    }
                    _coolDown = Time.time + _moveTiming;
                }
            }
        }

        private bool HealthUnderFifty()
        {
            if (_bossHealth.GetHealthPercentage() <= 50)
            {
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