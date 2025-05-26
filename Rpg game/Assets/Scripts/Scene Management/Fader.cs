using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup faderGroup;

        private void Awake()
        {
            faderGroup = GetComponent<CanvasGroup>();
            faderGroup.alpha = 0f;
        }
        public void FadeOutImmediate()
        {
            faderGroup.alpha = 1f;
        }
        public IEnumerator FadeOut(float time)
        {
            while(faderGroup.alpha < 1)
            {
                faderGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
            yield break;
        }
        public IEnumerator FadeIn(float time)
        {
            while (faderGroup.alpha > 0)
            {
                faderGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            yield break;
        }
    }
}