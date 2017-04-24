using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;

        public float Speed = 2.0f;
        public float SmoothTime = 0.3f;
        public bool Limit = false;
        public Vector3 LimitMin = new Vector3();
        public Vector3 LimitMax = new Vector3();
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _pos = Vector3.zero;

        public void Follow(Transform target)
        {
            _target = target;
        }

        private void Start()
        {
            //Target = FindObjectOfType<>()
            _offset = transform.position - _target.transform.position;
        }

        private void Update()
        {
            /*
            transform.position = _target.transform.position; 
            float interpolation = speed * Time.deltaTime;

            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, _target.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, _target.transform.position.x, interpolation);

            this.transform.position = position + _offset; ;
            */
            //var dist = Vector3.Distance(transform.position, _target.transform.position + _offset)*Time.deltaTime;
            //if (dist > 1f)
            {
                _pos = Vector3.SmoothDamp(transform.position, _target.transform.position + _offset,
                    ref _velocity, SmoothTime, 5f);
                if (Limit)
                {
                    _pos.x = Mathf.Clamp(_pos.x, LimitMin.x, LimitMax.x);
                    _pos.y = Mathf.Clamp(_pos.y, LimitMin.y, LimitMax.y);
                    _pos.z = Mathf.Clamp(_pos.z, LimitMin.z, LimitMax.z);
                }

                transform.position = _pos;
            }
        }
    }
}


