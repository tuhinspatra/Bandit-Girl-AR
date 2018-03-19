using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{

    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}