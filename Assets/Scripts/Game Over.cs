using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TMP_InputField names;
    public TextMeshProUGUI error;
    public GameObject errorContainter;
    [SerializeField] private ScriptableStats _stats;

    public void Start()
    {
        errorContainter.SetActive(false);
    }
    public void Play()
    {
      if (names.text.Trim() != "")
        {
           
            StartCoroutine(storeCollectedItem("http://localhost/Module2/checkUser.php", names.text, _stats.AttackDamage, _stats.AttackRange, _stats.MaxSpeed, _stats.JumpPower, _stats.MaxHealth, _stats.CurrentHealth));
            StartCoroutine(deleteCollectedItems("http://localhost/Module2/deleteData.php"));
            Reset();
            SceneManager.LoadScene("Forest Level");
        }
        else
        {
            error.text = "Enter a Username!";
            errorContainter.SetActive(true);
        }
        
    }

    public void CloseError (){
        errorContainter.SetActive(false);
    }

    private void Reset()
    {
        _stats.AttackDamage = 25;
        _stats.AttackRange = 0.6f;
        _stats.MaxSpeed = 14;
        _stats.JumpPower = 36;
        _stats.MaxHealth = 3;
        _stats.CurrentHealth = 3;
    }


    public void Quit()
    {
        Application.Quit();
    }



    IEnumerator storeCollectedItem(string url, string user, int atkDmg, float atkRng, float movSpd, float jmpHgt, float maxHealth, float currentHealth)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
        form.AddField("atkDmg", atkDmg);
        form.AddField("atkRng", atkRng.ToString());
        form.AddField("movSpd", movSpd.ToString());
        form.AddField("jmpHgt", jmpHgt.ToString());
        form.AddField("maxHealth", maxHealth.ToString());
        form.AddField("currentHealth", currentHealth.ToString());


        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }

        Debug.Log(uwr.downloadHandler.text);

    }

    IEnumerator deleteCollectedItems(string url)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
    }
}
