using Character.Attack;
using System.Collections;
using System.Collections.Generic;
using Systems.EntityState;
using Systems.Pooling;
using Systems.Spawn;
using Systems.UI;
using UnityEngine;

namespace BehaviourTree
{
    public class BossBT : TreeBT
    {
        [SerializeField] Transform _shootPos;
        private GameObject _player;

        private Color _originalColor;
        private Color _damagedColor = Color.red;
        private SpriteRenderer _spriteRenderer;
        private Transform _myTransform;

        private float _shakeTime = 4f;
        private float _shakeAmount = 15f;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _originalColor = _spriteRenderer.material.color;
            _myTransform = transform;
        }

        private void OnEnable()
        {
            _player = GameObject.FindWithTag("Player");

            //if (_player != null)
            //    Debug.Log("Found player!");
            //else
            //    Debug.Log("No player found!");
        }

        protected override Node SetupTree()
        {
            Node root = new Sequence(new List<Node>
            {
                new BossPhase1(gameObject),
                new BossPhase2(gameObject),
                new BossPhase3(gameObject, _player, this, _shootPos)
            });

            return root;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("PlayerBullet"))
                {
                    GetComponentInParent<Health>().TakeDamage(1);
                    collision.GetComponent<PlayerProjectile>().InstantiateParticles();
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);

                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(ChangeColor());
                    }
                }
            }
        }

        public void DeathAnimation()
        {
            StartCoroutine(BossShake(_shakeTime, _shakeAmount));
        }

        public IEnumerator ChangeColor()
        {
            _spriteRenderer.material.color = _damagedColor;

            yield return new WaitForSeconds(0.05f);

            _spriteRenderer.material.color = _originalColor;
        }

        public IEnumerator BossShake(float shakeTime, float shakeAmount)
        {
            float time = 0;

            Vector3 currentPos = _myTransform.position;
            Debug.Log(currentPos);

            while (time < shakeTime)
            {
                _myTransform.position = new Vector3(currentPos.x + Mathf.PerlinNoise(shakeAmount * Time.time, 0), currentPos.y + Mathf.PerlinNoise(0, shakeAmount * Time.time), _myTransform.position.z);
                time += Time.deltaTime;

                yield return null;
            }

            string gameOverTxt = "Congratulation !";
            UIManager.Instance.OpenGameOverMenu(gameOverTxt);
            UIManager.Instance.AddScore(GetComponent<Health>().GetScoreAmount());
            ObjectPoolManager.ReturnObjectPool(gameObject);
        }
    }
}