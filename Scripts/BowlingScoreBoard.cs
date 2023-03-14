using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingScoreBoard : MonoBehaviour
{
    public BowlingScoreCard[] scoreCards;
    public BowlingScoreCard currentCard;
    public int currentCardIndex = 0;
    
    

    void Start()
    {
        currentCard = scoreCards[0];
        for (int i = 0; i < scoreCards.Length; i++)
            {
               scoreCards[i].index = i;
            }
          
    }
    void Update()
    {
        currentCard = scoreCards[currentCardIndex];
    }

    public void NextBowlingScoreCard()
    {
       
        currentCardIndex++;
     }

    public void ResetScoreCards()
    {
        foreach(BowlingScoreCard s in scoreCards)
        {
            s.ResetScoreCard();
        }
        currentCardIndex = 0;
    }
}



