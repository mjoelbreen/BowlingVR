using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlingManager : ActivityBase
{
    [Header("Set Geometry")]
    [SerializeField] private BowlingBall ball;
    private GameObject[] pinObjects;
    public GameObject[] games;
    public BowlingGame[] bowlingGames;
    public BowlingScoreBoard board;
    private List<int> throwScores = new List<int>();
    [SerializeField] private TextMeshProUGUI scoreTotal;
    private List<Pin> pins = new List<Pin>();
    private int throwCount = 1;
    private int throwScore;
    private string scoreString;
    private bool lastRound = false;
    private BowlingScoreCard currentScoreCard;
    [SerializeField] private int ballReturnTime;
    
    public override void StartGame(int difficulty)
    {
       base.StartGame(difficulty);
       switch (difficulty)
        {
            case 0:
                pinObjects = bowlingGames[0].bowlingPins;
                ballReturnTime = 5;
                games[0].SetActive(true);
                break;
            case 1:
                pinObjects = bowlingGames[1].bowlingPins;
                ballReturnTime = 7;
                games[1].SetActive(true);
                break;
            case 2:
                pinObjects = bowlingGames[2].bowlingPins;
                ballReturnTime = 10;
                games[2].SetActive(true);
                break;
            case 3:
                pinObjects = bowlingGames[3].bowlingPins;
                ballReturnTime = 12;
                games[3].SetActive(true);
                break;
            default:
                Debug.Log("Game does not exist.");
                break;
        }
        score = 0;
        throwScore = 0;
        ball.g.SetActive(true);
        foreach (GameObject o in pinObjects)
        {
            pins.Add(o.GetComponentInChildren<Pin>());
         }

        // Check for pin colliders to hit the ground

        foreach (GameObject o in pinObjects)
        {
            o.GetComponentInChildren<PinCollisionCheck>().OnKnockedOverEvent.AddListener(delegate
            {
                o.SetActive(false);
                score++;
                throwScore++;
             });
        }
    }

    public override void FinishGame()
    { 
        base.FinishGame();
        board.ResetScoreCards();
        StopAllCoroutines();
    }

    private void Update()
    {
        
        currentScoreCard = board.currentCard;

        if(board.currentCardIndex == 9)
        {
            lastRound = true;
        }
     }

    

    // Determines what to display in scoreboxes
    private string DetermineScoreString(int value)
    {
       
        if(throwScore == 0)
        {
            return "-";
        }
       
        else
        {
            return throwScore.ToString();
        }
    }

    private void ReturnBall()
    {
        ball.transform.rotation = ball.rot;
        ball.transform.position = ball.pos;
        ball.rb.velocity = Vector3.zero;
        ball.rb.angularVelocity = Vector3.zero;
        ball.g.SetActive(true);
    }

    // Returns ball and resets pins after second throw
    public void ResetBall()
    {
        StartCoroutine(EvaluateThrow());
     }

    private void FirstThrow()
    {
        currentScoreCard.leftScore = throwScore;

        if (throwScore == 10 && !lastRound)
        {
            currentScoreCard.leftBox.text = "";
            currentScoreCard.rightBox.text = "X";
            currentScoreCard.sumBox.text = score.ToString();
            ResetPins();
            board.NextBowlingScoreCard();
            throwCount = 1;
        }
        else if (throwScore == 10 && lastRound)
        {
            currentScoreCard.leftBox.text = "X";
            throwCount++;
        }
        else
        {
            currentScoreCard.leftBox.text = DetermineScoreString(currentScoreCard.leftScore);
            throwCount++;
        }
      
        ReturnBall();
        throwScore = 0;
    }

    private void SecondThrow()
    {
        currentScoreCard.rightScore = throwScore;
        if (throwScore == 10 && !lastRound)
        {
            currentScoreCard.rightBox.text = "/";
            throwCount = 1;
            currentScoreCard.sumBox.text = score.ToString();
            board.NextBowlingScoreCard();
        }
        else if (throwScore == 10 && lastRound)
        {
            if(currentScoreCard.leftScore == 10)
            {
                currentScoreCard.rightBox.text = "X";
            }
            else
            {
                currentScoreCard.rightBox.text = "/";
            }
            throwCount++;
        }
        else if(throwScore < 10 && !lastRound)
        {
            if((currentScoreCard.leftScore + currentScoreCard.rightScore) == 10)
            {
                currentScoreCard.rightBox.text = "/";
            }
            else
            {
                currentScoreCard.rightBox.text = DetermineScoreString(currentScoreCard.rightScore);
            }
            currentScoreCard.sumBox.text = score.ToString();
            board.NextBowlingScoreCard();
            throwCount = 1;
         }
        else if(throwScore < 10 && lastRound)
        {
            if ((currentScoreCard.leftScore + currentScoreCard.rightScore) == 10)
            {
                currentScoreCard.rightBox.text = "/";
            }
            else
            {
                currentScoreCard.rightBox.text = DetermineScoreString(currentScoreCard.rightScore);
            }
            throwCount++;
        }
           
        ResetPins();
        ReturnBall();
        throwScore = 0;
    }

    // Returns ball to starting position and resets if strike

  private IEnumerator EvaluateThrow()
    {
        yield return new WaitForSeconds(ballReturnTime);

        throwScores.Add(throwScore);

        if(throwCount == 1)
        {
            FirstThrow();
        }
        else if(throwCount == 2)
        {
            SecondThrow();
        }

        else 
        {
            currentScoreCard.thirdScore = throwScore;
            if(throwScore == 10)
            {
                currentScoreCard.thirdBox.text = "X";
            }
            else
            {
                currentScoreCard.thirdBox.text = DetermineScoreString(currentScoreCard.thirdScore);
            }
            currentScoreCard.sumBox.text = score.ToString();
            scoreTotal.text = score.ToString();
            ball.g.SetActive(false);
            FinishGame();
        }
        
    }

    private void ResetPins()
    {
        foreach (Pin p in pins)
        {
            p.transform.position = p.pos;
            p.transform.rotation = p.rot;
            p.rb.velocity = Vector3.zero;
            p.rb.angularVelocity = Vector3.zero;
        }
       foreach(GameObject o in pinObjects)
        {
            o.SetActive(true);
        }
     }

    /*
     * Functions for determining updated scores of spares and strikes
     
    private int EvaluateStrike(BowlingScoreCard c)
    {
        int frameScore = c.sumScore + throwScores[throwScores.Count - 2] + throwScores[throwScores.Count - 1];
        return frameScore;
    }

    private int EvaluateSpare(BowlingScoreCard c)
    {
        int frameScore = c.sumScore + throwScores[throwScores.Count - 1];
        return frameScore;
    }

    private int CalculateRunningTotal(BowlingScoreCard c)
    {
        int runningTotal;
        if (c.index == 0)
        {
            runningTotal = c.frameScore;
        }
        else
        {
            runningTotal = c.frameScore + board.scoreCards[c.index - 1].runningTotal;
        }
        return runningTotal;
    }

    void EvaluateScores()
    {
        foreach (BowlingScoreCard s in board.scoreCards)
        {
            if (s.strike && ((throwScores.Count - s.currentThrowCount) == 2))
            {
                s.frameScore = EvaluateStrike(s);

            }
            else if (s.spare && ((throwScores.Count - s.currentThrowCount) == 1))
            {
                s.frameScore = EvaluateSpare(s);
            }
            else
            {
                s.frameScore = s.sumScore;
            }
        }
    }
    */

}

