using UnityEngine;


namespace AlexDev.CatchMe
{

    public class TaggingTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask _excludeLayers;

        private Tagging _tagger;

        public void Initialize(Tagging taggingScript)
        {
            _tagger = taggingScript;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _excludeLayers.value) == 0)
            {
                if (other.TryGetComponent<Tagging>(out var enemyTagger))
                {
                    enemyTagger.Catched();
                    Debug.Log(gameObject.name + " Initialize " + _tagger.name);
                    _tagger.OnTagSucces();
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}
