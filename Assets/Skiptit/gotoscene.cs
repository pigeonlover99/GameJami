using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotoscene : MonoBehaviour
{

    public void Goscene()
    {
        SceneManager.LoadScene("pääpeli");
    }
    public void gotocredits()
    {
        SceneManager.LoadScene("Credits");
    }

public void Gotomain()
    {
        SceneManager.LoadScene("titlescreen");
    }

}    

