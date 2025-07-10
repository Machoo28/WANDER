using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] GameObject Shop;
    [SerializeField] TextMeshProUGUI points;

    [SerializeField] GameObject confirmation;
    [SerializeField] TextMeshProUGUI beforeStats;
    [SerializeField] TextMeshProUGUI afterStats;
    [SerializeField] Button ConfirmBTN;

    [SerializeField] GameObject NoPoints;

    private int num,stats, availablePoint;
    private float statsf;

    private void Start()
    {
        Shop.SetActive(false);
        confirmation.SetActive(false);
        NoPoints.SetActive(false);  
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
        gameObject.SetActive(false);
        confirmation.SetActive(false);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        Shop.SetActive(false);
        confirmation.SetActive(false);
        NoPoints.SetActive(false);
        Time.timeScale = 1;
    }

    public bool SubtractPoints()
    {
        try
        {
            availablePoint = Int32.Parse(points.text.Split(' ')[1]);
        } catch {
            availablePoint = 0;
        }
        

        if (availablePoint<=100)
        {
            return false;
        }
        else
        {
            //artCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
            return true;
        }
        
    }

    public void Confirm(int nums)
    {
        if (SubtractPoints())
        {
            num = nums;
            Shop.SetActive(false);
            confirmation.SetActive(true);
            ConfirmBTN.onClick.AddListener(Buy);


            switch (num)
            {
                case 1:
                    beforeStats.SetText(_stats.AttackDamage.ToString());
                    stats = _stats.AttackDamage + 7;
                    afterStats.SetText(stats.ToString());
                    break;
                case 2:
                    beforeStats.SetText(_stats.AttackRange.ToString());
                    statsf = _stats.AttackRange + 0.5f;
                    afterStats.SetText(statsf.ToString());
                    break;
                case 3:
                    beforeStats.SetText(_stats.MaxSpeed.ToString());
                    statsf = _stats.MaxSpeed + 7;
                    afterStats.SetText(statsf.ToString());
                    break;
                case 4:
                    beforeStats.SetText(_stats.JumpPower.ToString());
                    statsf = _stats.JumpPower + 5;
                    afterStats.SetText(statsf.ToString());
                    break;
                case 5:
                    beforeStats.SetText(_stats.MaxHealth.ToString());
                    statsf = _stats.MaxHealth + 1;
                    afterStats.SetText(statsf.ToString());
                    break;


            }
        }
        else
        {
            CloseShop();
            NoPoints.SetActive(true);
        }
        



    }

    public void Buy()
    {
       
            switch (num)
            {
                case 1:
                    AddAtkPow();
                    StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
                    OpenShop();
                    break;
                case 2:
                    AddAtkRng();
                    StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
                    OpenShop();
                    break;
                case 3:
                    AddMovSpd();
                    StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
                    OpenShop();
                    break;
                case 4:
                    AddJumpHgt();
                    StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
                    confirmation.SetActive(false);
                    break;
                case 5:
                    AddExtLife();
                    StartCoroutine(storeCollectedItem("http://localhost/Module2/collectData.php", "Purchase", "-100"));
                    OpenShop();
                    break;
                default:           
                    break;
            }
       
    }
    public void AddAtkPow()
    {

            _stats.AttackDamage += 7;
        
    }

    public void AddAtkRng()
    {
       
            _stats.AttackRange += 0.5f;
        
    }
    public void AddMovSpd()
    {
        
            _stats.MaxSpeed += 7;
        
    }
    public void AddJumpHgt()
    {
        
            _stats.JumpPower += 5;
        
    }

    public void AddExtLife()
    {
             _stats.MaxHealth+=1;
             _stats.CurrentHealth += 1;
            float CurrentHealth = _stats.MaxHealth;
            float diff = _stats.MaxHealth - _stats.CurrentHealth;
            CurrentHealth = Mathf.Clamp(_stats.MaxHealth - diff, 0, _stats.MaxHealth);
    }

    IEnumerator storeCollectedItem(string url, string item, string points)
    {
        WWWForm form = new WWWForm();
        form.AddField("item", item);
        form.AddField("points", points);


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }



    }
}
