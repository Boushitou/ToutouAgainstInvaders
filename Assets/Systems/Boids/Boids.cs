using Character.Attack;
using Systems.EntityState;
using Systems.Pooling;
using Systems.Spawn;
using UnityEngine;

namespace Systems.Boids
{
    public class Boids : MonoBehaviour
    {
        private float _speed;
        private Transform _myTransform;
        private Vector3 _direction = Vector3.up;

        private void Awake()
        {
            _myTransform = transform;
            _speed = Random.Range(FlockManager.Instance._minSpeed, FlockManager.Instance._maxSpeed);
        }

        private void Update()
        {
            if (Random.Range(0, 100) < 10)
            {
                _speed = Random.Range(FlockManager.Instance._minSpeed, FlockManager.Instance._maxSpeed);
            }

            ApplyRules();
            _myTransform.position += (_direction * _speed * Time.deltaTime);

            DestroyBoid();
        }

        public void ApplyRules()
        {
            GameObject[] boids = FlockManager.Instance._allBoids;

            Vector2 vCentre = Vector2.zero;
            Vector2 vAvoid = Vector2.zero;
            float groupSpeed = 0.01f;
            float nDistance;
            int groupSize = 0;

            foreach (GameObject boid in boids)
            {
                if (boid != gameObject)
                {
                    nDistance = Vector2.Distance(boid.transform.position, _myTransform.position);

                    if (nDistance <= FlockManager.Instance._neighbourDistance)
                    {
                        vCentre += (Vector2)boid.transform.position;
                        groupSize++;

                        if(nDistance < FlockManager.Instance._separationDistance)
                        {
                            vAvoid += ((Vector2)_myTransform.position - (Vector2)boid.transform.position).normalized / nDistance;
                        }

                        Boids anotherBoid = boid.GetComponent<Boids>();
                        groupSpeed += anotherBoid._speed;
                    }
                }
            }

            if (groupSize > 0 )
            {
                vCentre = vCentre / groupSize + ((Vector2)FlockManager.Instance._goalPos.transform.position - (Vector2)_myTransform.position);
                _speed = Mathf.Clamp(groupSpeed / groupSize, FlockManager.Instance._minSpeed, FlockManager.Instance._maxSpeed);

                _direction = (vCentre + vAvoid) - (Vector2)_myTransform.position;


                if (_direction != Vector3.zero)
                {
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

                    _myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, Quaternion.Euler(0, 0, angle - 90), FlockManager.Instance._steeringSpeed * Time.deltaTime);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("PlayerBullet"))
                {
                    GetComponent<Health>().TakeDamage(1);

                    collision.GetComponent<PlayerProjectile>().InstantiateParticles();
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);
                }
            }
        }

        public void DestroyBoid()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y - 1)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
                SpawnerManager.Instance.RemoveEnemy();
            }
        }
    }
}