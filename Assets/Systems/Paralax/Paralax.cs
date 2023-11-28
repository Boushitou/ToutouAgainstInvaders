using UnityEngine;

namespace Systems.Paralax
{
    public class Paralax : MonoBehaviour
    {
        public float _speed;

        private Transform _myTransform;
        private Vector3 _startPos;
        private Vector3 _endPos;

        // Start is called before the first frame update
        void Start()
        {
            _myTransform = transform;
            _startPos = new Vector3(0.13f, 56.85f, 5f);
            _endPos = new Vector3(0.13f, -46.37f, 5f);
        }

        // Update is called once per frame
        void Update()
        {
            _myTransform.position += Vector3.down * _speed * Time.deltaTime;

            if (_myTransform.position.y < _endPos.y)
            {
                _myTransform.position = _startPos;
            }
        }
    }

}