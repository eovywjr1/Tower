using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrePareManager : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Title");
    }
}
