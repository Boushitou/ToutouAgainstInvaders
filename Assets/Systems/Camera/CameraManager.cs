using UnityEngine;

namespace Systems
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance;

        private Vector2 _minBound;
        private Vector2 _maxBound;

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

            _minBound = _cam.ViewportToWorldPoint(new Vector2(0, 0));
            _maxBound = _cam.ViewportToWorldPoint(new Vector2(1, 1));
        }

        public Vector2 GetMinBound() { return _minBound; }
        public Vector2 GetMaxBound() { return _maxBound; }

        public Camera GetCamera() { return _cam; }
    }
}