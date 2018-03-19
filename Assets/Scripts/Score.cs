using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Score : UnityEngine.MonoBehaviour
{

    private static int FinalScore;
    public Text text;

    private void Start()
    {
        text.text = "0";
    }

    public void UpScore()
    {
        print("I am being called");
        FinalScore += 10;
        text.text = "" + FinalScore;
        if(FinalScore==100)
        {
            Application.LoadLevel(3);
        }
    }

    public int  GetScore()
    {
        return FinalScore;
    }

    public void ResetScore()
    {
        FinalScore = 0;
        text.text = "" + FinalScore;
    }

}
