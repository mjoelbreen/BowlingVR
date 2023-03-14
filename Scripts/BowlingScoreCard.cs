using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BowlingScoreCard : MonoBehaviour
{
    public int leftScore = 0;
    public int rightScore = 0;
    public int thirdScore = 0;
    public int sumScore = 0;
    public int frameScore = 0;
    public int runningTotal = 0;
    public int index;
    public int currentThrowCount = 0;
    public bool strike = false;
    public bool spare = false;
    public TextMeshProUGUI numberBox;
    public TextMeshProUGUI leftBox;
    public TextMeshProUGUI rightBox;
    public TextMeshProUGUI sumBox;
    public TextMeshProUGUI thirdBox;

    void Update()
    {
        sumScore = leftScore + rightScore + thirdScore;
    }

    public void ResetScoreCard()
    {
        leftScore = 0;
        rightScore = 0;
        thirdScore = 0;
        sumScore = 0;
        frameScore = 0;
        runningTotal = 0;
        currentThrowCount = 0;
        leftBox.text = "";
        rightBox.text = "";
        sumBox.text = "";
        thirdBox.text = "";
    }
}
