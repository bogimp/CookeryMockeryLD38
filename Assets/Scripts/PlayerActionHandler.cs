using System.Collections.Generic;
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
        private Shader _shader;
        private readonly List<GameObject> _itemsList = new List<GameObject>();
        private int _curItem = 0;

        public void Start()
        {
            var trigger = gameObject.AddComponent<CapsuleCollider>();
            trigger.isTrigger = true;
            trigger.center = new Vector3(0f, 0.7f, 0.6f);
            trigger.radius = 0.2f;
            trigger.height = 1.2f;

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
                FreeOrSelectNext();
                //FreeItem();
            }
            else
            {
                GrabFood();
            }

            _lastClicked = 0.5f;
        }

        private void FreeOrSelectNext()
        {
            if (_itemsList.Count == 0)
            {
                FreeItem();
            }
            else
            {
                FreeItem();
                _curItem++;
                GrabFood();
            }
        }

        private void FreeItem()
        {
            if (!_foodItem) return;
            var renderrer = _foodItem.GetComponent<Renderer>();
            if (!renderrer)
            {
                renderrer = _foodItem.GetComponentInParent<Renderer>();
                if (!renderrer)
                {
                    renderrer = _foodItem.transform.parent.GetComponentInChildren<Renderer>();
                }
            }
            if (renderrer)
            {
                //foreach (var material in renderrer.materials)
                var material = renderrer.material;
                {
                    material.shader = _shader;
                    material.SetFloat("_Outline", 0);
                }
            }

            if (_springJoint)
                Destroy(_springJoint);
            _springJoint = null;
            _foodItem = null;
        }

        private void GrabFood()
        {
            _foodItem = GetFoodItem();
            if (_foodItem)
            {
                if (!_springJoint) _springJoint = gameObject.AddComponent<SpringJoint>();

                _springJoint.connectedBody = _foodItem.GetComponent<Rigidbody>();
                var renderrer = _foodItem.GetComponent<Renderer>();
                if (!renderrer)
                {
                    renderrer = _foodItem.GetComponentInParent<Renderer>();
                    if (!renderrer)
                    {
                        renderrer = _foodItem.transform.parent.GetComponentInChildren<Renderer>();
                    }
                }

                if (renderrer)
                {
                    //foreach (var material in renderrer.materials)
                    var material = renderrer.material;
                    {
                        _shader = material.shader;
                        material.shader = Shader.Find("Standard (Specular setup) (Outlined)");
                        material.SetColor("_OutlineColor", Color.white);
                        material.SetFloat("_Outline", 0.1f);
                    }
                }

                //todo: set params
                _springJoint.anchor = new Vector3(0f, 1f, 0f);
                _springJoint.spring = 500f;
                _springJoint.damper = 0.1f;
                _springJoint.tolerance = 0.05f;
                _springJoint.enableCollision = true;
            }

        }

        private GameObject GetFoodItem()
        {
            if (_itemsList.Count == 0) return null;
            if (_curItem >= _itemsList.Count)
            {
                _curItem = 0;
            }
            return _itemsList[_curItem];

            var start = this.gameObject.transform.position + (Vector3.up*RayGroundDy);
            var to = transform.forward*RayDistance;

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


        public void OnTriggerEnter(Collider other)
        {
            var go = other.gameObject;
            if (_itemsList.Contains(go)) return;
            _itemsList.Add(go);
        }

        public void OnTriggerExit(Collider other)
        {
            var go = other.gameObject;
            _itemsList.Remove(go);
        }

    }
}
