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
            Debug.Log(gameObject.name + " Initialize " + _tagger.name);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Tagging>(out var enemyTagger) && enemyTagger != _tagger)
            {
                enemyTagger.Catched();
                _tagger.OnTagSucces();
                GetComponent<Collider>().enabled = false;
                
            }
        }
    }
}
