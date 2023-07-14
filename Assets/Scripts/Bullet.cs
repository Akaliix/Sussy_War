using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public Rigidbody _rb;

    [SerializeField]
    public LayerMask bulletHitLayer;

    [SerializeField]
    public GameObject bulletModel;

    [SerializeField]
    public float sphereRadius = 1f;
    [SerializeField]
    public float offset = -0.5f;

    float lifetime = 5f;
    float gecenSure = 0f;

    float bulletVelocity = -1f;

    bool hitted = false;

    public void SetBullet(float speed)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.AddForce(transform.forward * speed);
        bulletVelocity = (speed / _rb.mass) * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (hitted)
            return;

        gecenSure += Time.deltaTime;
        if (gecenSure > lifetime)
        {
            Yoket();
            return;
        }

        float dist = bulletVelocity * 2 * Time.fixedDeltaTime;

        if (Physics.SphereCast(transform.position + offset * transform.forward, sphereRadius, transform.forward, out RaycastHit hit, dist, bulletHitLayer))
        {
            hitted = true;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemySpawner.DestroyEnemy(hit.transform.gameObject);
            }
            else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerController.singleton.GetHit();
            }
            Debug.Log(hit.transform.gameObject.layer);
            Yoket();
        }
    }

    private void Yoket()
    {
        bulletModel.SetActive(false);
        _rb.velocity = new Vector3(0,0,0);
        Destroy(this.gameObject, 2f);
    }

//#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset * transform.forward, sphereRadius);
    }
//#endif
}
