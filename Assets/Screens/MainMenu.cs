using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelName;


	public void ChangeScene()
    {
        Debug.Log("Mudando Cena");
        SceneManager.LoadScene(levelName);
    }
}
