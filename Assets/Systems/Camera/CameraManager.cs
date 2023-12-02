using System.Collections;
using UnityEngine;

namespace Systems
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;

        private Vector2 _minBound;
        private Vector2 _maxBound;

        private Transform _myTransform;

        private float _shakeAmount = 50f;

        private Camera _cam;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            _cam = GetComponent<Camera>();

            _myTransform = transform;

            _minBound = _cam.ViewportToWorldPoint(new Vector2(0, 0));
            _maxBound = _cam.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public IEnumerator ShakeScreen(float shakeDuration)
        {
            float time = 0;

            while (time < shakeDuration)
            {
                _myTransform.position = new Vector3(Mathf.PerlinNoise(_shakeAmount * Time.time, 0), Mathf.PerlinNoise(0, _shakeAmount * Time.time), _myTransform.position.z);
                time += Time.deltaTime;

                yield return null;
            }
        }

        public Vector2 GetMinBound() { return _minBound; }
        public Vector2 GetMaxBound() { return _maxBound; }

        public Camera GetCamera() { return _cam; }
    }
}