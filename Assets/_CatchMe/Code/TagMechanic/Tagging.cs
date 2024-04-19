using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexDev.CatchMe
{


    public class Tagging : MonoBehaviour
    {
        [SerializeField] private Transform _rightArmPrototype;
        [SerializeField] private Collider _hitBox;

        [SerializeField] private float _taggingpeed = 3f;

        private UnitController _controller;

        private bool isTagging;

        private void Awake()
        {
            _controller = GetComponent<UnitController>();
        }

        private void Start()
        {
            _hitBox.GetComponent<TaggingTrigger>().Initialize(this);
            _rightArmPrototype.gameObject.SetActive(false);
        }

        public void OnTagging(InputValue value)
        {
            Tag();
        }

        public bool Tag()
        {
            Debug.Log("Tag!");
            if (!isTagging)
            {
                isTagging = true;
                GetComponent<Animator>().SetTrigger("Tag");
                return true;
            }
            return false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GetComponent<Animator>().SetTrigger("Tag");
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

        public void ShowHitBox()
        {
            _hitBox.enabled = true;
        }

        public void HideHitBox()
        {
            _hitBox.enabled = false;
            isTagging = false;
        }

    }

}