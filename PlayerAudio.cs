using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip electricSound;
    public AudioSource audioS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tower"))
        {
            audioS.PlayOneShot(electricSound);
        }
    }


    public AudioClip[] hardSteps;

    public void Footsteps()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int r = Random.Range(0, 3);
        if(Physics.Raycast(ray, out hit, 1f))
        {
            audioS.PlayOneShot(hardSteps[r]);
        }
    }

}
