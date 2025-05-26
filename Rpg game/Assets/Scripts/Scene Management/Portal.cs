using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Sandbox,
    Sandbox2
}
public enum DestinationIdentifier
{
    A, B, C, D, E, F
}
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 2f, fadeInTime = 2f, fadeWaitTime = 0.5f;
        [SerializeField] SceneName sceneName;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine("LoadOtherScene", sceneName);
            }
        }
        IEnumerator LoadOtherScene(SceneName scene)
        {
            DontDestroyOnLoad(this);
            
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            // save
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync((int)scene);
            print("Scene Loaded");
            
            // load
            wrapper.Load();

            Portal portal = GetOtherPortal(destination);
            UpdatePlayer(portal);

            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            
            Destroy(gameObject);
        }
        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal(DestinationIdentifier destination)
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    } 
}
