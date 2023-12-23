using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public float WaitUntilChange = 3f;

    public void ChangeAfterWait()
    {
        StartCoroutine(WaitAndChange());
    }

    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(WaitUntilChange);
        SceneManager.LoadScene(SceneName);
    }
}
