using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class nearMerchant : MonoBehaviour
{
    float distance;
    float maxDistance = 10.0f;
    public GameObject player;
    public GameObject shopBtn;
    public Vector3 offset;

    [SerializeField] GameObject Shop;
    [SerializeField] GameObject confirmation;
    [SerializeField] GameObject NoPoints;



    private void Start()
    {
        Shop.SetActive(false);
        confirmation.SetActive(false);
        NoPoints.SetActive(false);
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        shopBtn.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

        if (isNear())
        {
       
            shopBtn.SetActive(true);
        }
        else
        {
            shopBtn.SetActive(false);
        }
        
    }


    private bool isNear()
    {
        return (distance <= maxDistance);
    }
}
