using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAudioManager : MonoBehaviour
{
    public static InGameAudioManager singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }
    public enum AudioType
    {
        pistolShot,
        pistolRecoil,
        meleeAttack,
        hit,
        death
    }

    [SerializeField]
    public AudioClip pistolShot;

    [SerializeField]
    public AudioClip pistolRecoil;

    [SerializeField]
    public AudioClip meleeAttack;


    [SerializeField]
    public AudioClip getHit1;
    //[SerializeField]
    //public AudioClip getHit2;

    [SerializeField]
    public AudioClip deathSound;

    [SerializeField]
    public AudioSource aud;

    public void PlayAudio(AudioType audType)
    {
        if (audType == AudioType.pistolShot)
        {
            aud.PlayOneShot(pistolShot);
        }
        else if (audType == AudioType.pistolRecoil)
        {
            aud.PlayOneShot(pistolRecoil);
        }
        else if (audType == AudioType.meleeAttack)
        {
            aud.PlayOneShot(meleeAttack);
        }
        else if (audType == AudioType.hit)
        {
            //aud.PlayOneShot(Random.value < 0.5 ? getHit1: getHit2);
            aud.PlayOneShot(getHit1);
        }
        else if (audType == AudioType.death)
        {
            aud.PlayOneShot(deathSound);
        }
    }
}
