using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int questionSeen = 0;   

    public int CorrectAnswers { get { return correctAnswer; } }
    public void IncrrementCorrecAnswers()
    {
        correctAnswer++;
    }

    public int QuestionSeen { get { return questionSeen; } }

    public void IncrementQustionSeen()
    {
        questionSeen++;
    }
    public int calculateScore()
    {
        return Mathf.RoundToInt(correctAnswer / (float)questionSeen * 100);
    }
}
