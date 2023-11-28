using Systems.Pooling;
using UnityEngine;

namespace Systems.Particles
{
    public class Particles : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_particleSystem.isStopped)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
            }
        }
    }
}
