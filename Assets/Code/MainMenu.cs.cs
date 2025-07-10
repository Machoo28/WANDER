using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private ScriptableStats _stats;
    public void Play()
    {
        StartCoroutine(deleteCollectedItems("http://localhost/Module2/deleteData.php"));
        Reset();
        SceneManager.LoadScene("Forest Level");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
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
