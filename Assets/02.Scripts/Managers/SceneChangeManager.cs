using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void MainScene()
    {
        SceneManager.LoadScene("TitleScene");
        Destroy(Player.Instance.gameObject);
    }
}
