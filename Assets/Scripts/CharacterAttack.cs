using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnTransform;

    [SerializeField]
    private float attackTime = 2f;

    [SerializeField]
    private float firstAttackWaitTime = -1f;

    private float attackTimeAccumulation = 0f;

    [SerializeField]
    private float NearAttackDistance = 20f;

    [SerializeField]
    private float bulletSpeed = 20f;

    private void Awake()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet bulunamadi!");
           // UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public bool Attack()
    {
        if (attackTimeAccumulation < attackTime)
        {
            attackTimeAccumulation += Time.deltaTime;
            return false;
        }
        attackTimeAccumulation = 0f;

        InGameAudioManager.singleton.PlayAudio(InGameAudioManager.AudioType.pistolShot);
        GameObject go;
        go = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation) as GameObject;
        Bullet _bullet = go.GetComponent<Bullet>();
        _bullet.SetBullet(bulletSpeed);
        return true;
    }

    public void NotAttack()
    {
        attackTimeAccumulation = firstAttackWaitTime;
    }
}
