using Enemies.Attack;
using System.Collections;
using UnityEngine;

namespace BehaviourTree
{
    public class BossPhase3 : Node
    {
        private float _moveTiming = 1f;
        private float _moveCooldown = 2f;
        private float _coolDown = 0;
        private float _atkSpeed = 0.3f;

        private int _bulletNb = 3;
        private int _shootNb = 3;
        private float _angleOffset = 40f;

        private bool _canMove = true;

        private GameObject _boss;
        private GameObject _player;
        private Transform _shootPos;
        private MonoBehaviour _behaviour;

        public BossPhase3(GameObject boss, GameObject player, MonoBehaviour behaviour, Transform shootPos)
        {
            _boss = boss;
            _player = player;
            _behaviour = behaviour;
            _shootPos = shootPos;
        }

        public override NodeState Evaluate()
        {
            _boss.GetComponent<BossAttack>().SetDirection((_player.transform.position - _boss.transform.position).normalized);

            if (_canMove)
            {
                if (Time.time > _coolDown)
                {
                    _canMove = false;

                    _behaviour.StartCoroutine(MoveTowardPlayer(_boss.transform.position, _player.transform.position));

                    _coolDown = Time.time + _moveCooldown;
                }
            }

            state = NodeState.RUNNING;

            return state;
        }

        private IEnumerator MoveTowardPlayer(Vector2 startPos, Vector2 targetPos)
        {
            float time = 0;

            while (time < _moveTiming)
            {
                _boss.transform.position = Vector2.Lerp(startPos, targetPos, time / _moveTiming);
                time += Time.deltaTime;

                yield return null;
            }

            _boss.transform.position = targetPos;

            Vector2 bulletDir = (_player.transform.position - _shootPos.position).normalized;

            float angle = Vector2.SignedAngle(Vector2.up, bulletDir);

            Debug.Log(angle);
            angle = -angle;

            float startAngle = angle - _angleOffset / 2f;
            float endAngle = angle + _angleOffset / 2f;
            float angleStep = (endAngle - startAngle) / _bulletNb;

            _behaviour.StartCoroutine(_boss.GetComponent<BossAttack>().LaunchCircleAttack(_shootNb, _atkSpeed, angleStep, startAngle, _bulletNb, _shootPos));

            _canMove = true;
        }
    }
}