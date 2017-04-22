using UnityEngine;
using UnityEngine.InputNew;

namespace Assets.Scripts
{
    public class PlayerActionHandler : MonoBehaviour
    {
        private PlayerControlls _playerControlls;
        private SpringJoint _springJoint;
        private bool isFoodHeld;
        
        void Start()
        {
            var payerInput = GetComponent<PlayerInput>();
            if (payerInput)
            {
                _playerControlls = payerInput.GetActions<PlayerControlls>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            isFoodHeld = _playerControlls.action.isHeld;
        
            if (isFoodHeld)
            {
                GrabFood();
            }
            else
            {
                FreeItem();
            }

        }

        private void FreeItem()
        {
            if(_springJoint)
                Destroy(_springJoint);
        }

        private void GrabFood()
        {
            var foodItem = GetFoodItem();
            if (foodItem)
            {
                if(!_springJoint)
                    _springJoint = gameObject.AddComponent<SpringJoint>();
                _springJoint.connectedBody = foodItem.GetComponent<Rigidbody>();
                //todo: set params
                _springJoint.spring = 500;
            }

        }

        private GameObject GetFoodItem()
        {
            var distance = 1f;
            var start = this.gameObject.transform.position;
            var to = transform.forward * distance;
            Ray ray = new Ray(start, to);
            RaycastHit hit;
            //Debug.DrawLine(start, to);
            if (!Physics.Raycast(ray, out hit, distance))
            {
                return null;
            }
            return hit.transform.gameObject;
            //todo: add layer mask for food
            print(hit.transform.gameObject);
        }
    }
}
