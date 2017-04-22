using UnityEngine;
using UnityEngine.InputNew;

namespace Assets.Scripts
{
    public class PlayerActionHandler : MonoBehaviour
    {
        public float RayGroundDy = 0.3f;
        public float RayDistance = 1f;

        private PlayerControlls _playerControlls;
        private SpringJoint _springJoint;
        private bool isFoodHeld;
        private GameObject _foodItem;
        private float _lastClicked;

        public void Start()
        {
            var payerInput = GetComponent<PlayerInput>();
            if (payerInput)
            {
                _playerControlls = payerInput.GetActions<PlayerControlls>();
            }
        }

        // Update is called once per frame
        public void Update()
        {

            _lastClicked -= Time.deltaTime;            
            if (_lastClicked > 0f) return;

            isFoodHeld = _playerControlls.action.wasJustPressed;
            if (!isFoodHeld) return;

            if (_foodItem)
            {
                FreeItem();
            }
            else
            {
                GrabFood();
            }

            _lastClicked = 0.5f;
        }

        private void FreeItem()
        {
            if (_springJoint)
                Destroy(_springJoint);
            _foodItem = null;
        }

        private void GrabFood()
        {
            _foodItem = GetFoodItem();
            if (_foodItem)
            {
                if (!_springJoint)
                    _springJoint = gameObject.AddComponent<SpringJoint>();
                _springJoint.connectedBody = _foodItem.GetComponent<Rigidbody>();

                //todo: set params
                _springJoint.spring = 500f;
                _springJoint.damper = 0.1f;
                _springJoint.tolerance = 0.05f;
                _springJoint.enableCollision = true;
            }

        }

        private GameObject GetFoodItem()
        {
            var start = this.gameObject.transform.position + (Vector3.up * RayGroundDy);
            var to = transform.forward * RayDistance;

            Ray ray = new Ray(start, to);
            RaycastHit hit;

#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawRay(start, to, Color.cyan);
#endif

            if (!Physics.Raycast(ray, out hit, RayDistance))
            {
                return null;
            }
            return hit.transform.gameObject;
            //todo: add layer mask for food
            print(hit.transform.gameObject);
        }
    }
}
