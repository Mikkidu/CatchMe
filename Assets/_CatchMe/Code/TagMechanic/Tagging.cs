using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexDev.CatchMe
{


    public class Tagging : MonoBehaviour
    {
        [SerializeField] private Transform _rightArmPrototype;

        [SerializeField] private float _taggingpeed = 3f;

        private Quaternion _initalArmAngle;

        private UnitController _controller;

        private bool isTagging;

        private void Awake()
        {
            _initalArmAngle = _rightArmPrototype.localRotation;
            _controller = GetComponent<UnitController>();
        }

        private void Start()
        {
            _rightArmPrototype.GetComponentInChildren<TaggingTrigger>().Initialize(this);
            _rightArmPrototype.gameObject.SetActive(false);
        }

        public void OnTagging(InputValue value)
        {
            Tag();
        }

        public bool Tag()
        {
            if (!isTagging)
            {
                isTagging = true;
                //_taggingTrigger = Time.realtimeSinceStartup + _taggingInterval;
                _rightArmPrototype.gameObject.SetActive(true);
                return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            if (isTagging)
            {
                //Debug.Log(_initalArmAngle + " " + _rightArmPrototype.localRotation + " " + Quaternion.Angle(_initalArmAngle, _rightArmPrototype.localRotation));
                _rightArmPrototype.Rotate(Vector3.right, -_taggingpeed, Space.Self);
                if (Quaternion.Angle(_initalArmAngle, _rightArmPrototype.localRotation) >= 120)
                {
                    //Debug.Log(_rightArmPrototype.localEulerAngles);
                    isTagging = false;
                    _rightArmPrototype.gameObject.SetActive(false);
                    _rightArmPrototype.localRotation = _initalArmAngle;
                }
            }
        }

        public void Catched()
        {
            Debug.Log(gameObject.name + " " + "Catched");
            _controller?.Catched();
        }

        public void OnTagSucces()
        {
            _controller?.OnTagSucces();
        }

    }

}