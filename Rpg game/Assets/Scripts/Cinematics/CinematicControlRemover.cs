using RPG.Control;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    private void EnableControl(PlayableDirector obj)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void DisableControl(PlayableDirector playableDirector)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
        print("Disable Control");
    }

}
