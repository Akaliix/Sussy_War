using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Transform _playerModel;
    [SerializeField]
    public Animator _playerAnimator;
    [SerializeField]
    public CharacterAttack _characterAttack;

    [SerializeField]
    public int Health = 5;
    public int MaxHealth = 5;

    [SerializeField]
    public int BulletAmount = 10;
    public int MaxBulletAmount = 10;

    public static PlayerController singleton;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Debug.LogError("Fazladan player var!");
        }
    }

    public void Reload()
    {
        BulletAmount = MaxBulletAmount;
        BulletBarController.singleton.UpdateHeartsHUD();
    }

    public void UseBullet()
    {
        --BulletAmount;
        BulletBarController.singleton.UpdateHeartsHUD();
    }

    public void GetHit()
    {
        Health--;
        InGameAudioManager.singleton.PlayAudio(InGameAudioManager.AudioType.hit);
        HealthBarController.singleton.UpdateHeartsHUD();
        Debug.Log("Health:" + Health);
        if (Health == 0)
        {
            _playerAnimator.SetBool("Die", true);
            GameSettings.singleton.GameOver();
        }
        else
        {
            _playerAnimator.SetTrigger("GetHit");
        }
    }

    public Transform ReturnTransform()
    {
        return this.transform;
    }
}
