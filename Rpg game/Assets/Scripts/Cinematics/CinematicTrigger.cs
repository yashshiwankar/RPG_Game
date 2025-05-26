using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        PlayableDirector playableDirector;
        bool hasTriggered = false;
        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (hasTriggered) return;
            if (playableDirector == null)
            {
                Debug.LogError($"Playable Director not attached. Cannot find playable Director on gameobject {gameObject.name}");
            }
            if (other.CompareTag("Player"))
            {
                hasTriggered = true;
                playableDirector.Play();
            }
        }

        public object CaptureState()
        {
            return hasTriggered;
        }

        public void RestoreState(object state)
        {
            hasTriggered = (bool)state;
        }
    }
}