using UnityEngine;

namespace Assets.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        public float speed = 2.0f;
        public float smoothTime = 0.3f;
        private Vector3 velocity = Vector3.zero;
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
                transform.position = Vector3.SmoothDamp(transform.position, _target.transform.position + _offset,
                    ref velocity, smoothTime, 5f);
            }
        }
    }
}


