using Character.Attack;
using Sound;
using System.Collections;
using Systems;
using Systems.EntityState;
using Systems.Pooling;
using Systems.Spawn;
using UnityEngine;

namespace Enemies.Movement
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        protected Transform _myTransform;
        protected float _speed = 0f;

        private float _offset = 5f;

        private Color _originalColor;
        private Color _damagedColor = Color.red;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _myTransform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.material.color;
        }

        private void Update()
        {
            DestroyEnemy();
            Movement();
        }

        public abstract void Movement();

        public void DestroyEnemy()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y - _offset)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
                SpawnerManager.Instance.RemoveEnemy();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("PlayerBullet"))
                {
                    SoundManager.Instance.PlaySound("Enemy Shot", 0.5f);
                    GetComponent<Health>().TakeDamage(1);

                    collision.GetComponent<PlayerProjectile>().InstantiateParticles();
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);

                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(ChangeColor());
                    }
                }
            }
        }

        public IEnumerator ChangeColor()
        {
            _spriteRenderer.material.color = _damagedColor;

            yield return new WaitForSeconds(0.05f);

            _spriteRenderer.material.color = _originalColor;
        }
    }
}
