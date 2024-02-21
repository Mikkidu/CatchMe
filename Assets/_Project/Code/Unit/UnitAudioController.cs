using UnityEngine;

namespace AlexDev.CatchMe
{
    class UnitAudioController
    {
        private UnitController _controller;


        public UnitAudioController(UnitController controller)
        {
            _controller = controller;
        }

        public void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (_controller.footstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, _controller.footstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(_controller.footstepAudioClips[index], _controller.transform.position);
                }
            }
        }

        public void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(_controller.landingAudioClip, _controller.transform.position);
            }
        }

    }
}
