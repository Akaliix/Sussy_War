using UnityEngine;
using UnityEngine.UI;

public class BulletBarController : MonoBehaviour
{
    private GameObject[] bulletContainers;
    private Image[] bulletFills;

    public Transform bulletsParent;
    public GameObject bulletContainerPrefab;

    public static BulletBarController singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        // Should I use lists? Maybe :)
        bulletContainers = new GameObject[(int)PlayerController.singleton.MaxBulletAmount];
        bulletFills = new Image[(int)PlayerController.singleton.MaxBulletAmount];

        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < bulletContainers.Length; i++)
        {
            if (i < PlayerController.singleton.MaxBulletAmount)
            {
                bulletContainers[i].SetActive(true);
            }
            else
            {
                bulletContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < bulletFills.Length; i++)
        {
            if (i < PlayerController.singleton.BulletAmount)
            {
                bulletFills[i].fillAmount = 1;
            }
            else
            {
                bulletFills[i].fillAmount = 0;
            }
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < PlayerController.singleton.MaxBulletAmount; i++)
        {
            GameObject temp = Instantiate(bulletContainerPrefab);
            temp.transform.SetParent(bulletsParent, false);
            bulletContainers[i] = temp;
            bulletFills[i] = temp.transform.Find("BulletFill").GetComponent<Image>();
        }
    }
}
