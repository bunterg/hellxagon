using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
    }
}
