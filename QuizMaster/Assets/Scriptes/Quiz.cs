using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] TextMeshProUGUI questionTxt;
    [SerializeField] List<QuestionSO> questionslist = new List<QuestionSO>();   
     QuestionSO currentquestion;

    [Header("Ansewr")]
    [SerializeField] GameObject[] answerButtons;
    public bool hasAnsweredEarly;

    [Header("Button Image")]
    [SerializeField] Sprite defaulAnswerSprite;
    [SerializeField] Sprite correctAnserSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProcessBar")]
    [SerializeField] Slider progressBar;

    [Header("WinQuiz")]
    [SerializeField] Canvas wincanvas;
    [SerializeField] TextMeshProUGUI scoreTxT;

    public bool iscomplete = false;

    void Start()
    {
        timer = GetComponent<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        
        progressBar.maxValue = questionslist.Count;
        progressBar.value = 0;  
    }
    private void Update()
    {
        if(iscomplete)
        {
            winQuiz();
            return;
        }
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false; 
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplaAnswer(-1);
            SetButtonState(false);
        }
    }
    void DisplayQuestion() 
    {
        questionTxt.text = currentquestion.Question;


        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonTxt = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonTxt.text = currentquestion.GetAnswer(i);
        }
    }
    public void OnAnsweredselected(int index) 
    {
        hasAnsweredEarly = true;
        DisplaAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score : " + scoreKeeper.calculateScore() +"%";
    }
    void DisplaAnswer(int index)
    {
        Image buttonImage;

        if (index == currentquestion.CorrecAnswerIndex)
        {
            questionTxt.text = "정답입니다!!!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnserSprite;
            scoreKeeper.IncrrementCorrecAnswers();
        }
        else
        {
            string correcAnswer = currentquestion.GetAnswer(currentquestion.CorrecAnswerIndex);
            questionTxt.text = "틀렸습니다";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnserSprite;
        }
    }
    void SetButtonState(bool state)
    {
        for (int a = 0; a < answerButtons.Length; a++)
        {
            Button buttonTxt = answerButtons[a].GetComponent<Button>();
            buttonTxt.interactable = state;
        }
    }
    void GetNextQuestion()
    {
        if(questionslist.Count > 0)
        {
            GetRandomQuestion();

            SetButtonState(true);

            SetDeafaulButtonSprite();

            DisplayQuestion();

            progressBar.value++;

            scoreKeeper.IncrementQustionSeen();
        }
        else
        {
            iscomplete = true;
        }
    }
    void GetRandomQuestion() 
    {
        int indexr = UnityEngine.Random.Range(0, questionslist.Count);
        currentquestion = questionslist[indexr];
        //questionslist.RemoveAt(index);
        if (questionslist.Contains(currentquestion))
        {
            questionslist.Remove(currentquestion);
        }
    }
    public void SetDeafaulButtonSprite()
    {
        for (int a = 0; a < answerButtons.Length; a++)
        {
            Image buttonImage = answerButtons[a].GetComponent<Image>();
            buttonImage.sprite = defaulAnswerSprite;
        }
    }
    private void winQuiz()
    {
        if(iscomplete)
        {
            wincanvas.gameObject.SetActive(true);
            scoreTxT.text = "Congratulations! " + "\nYou scored " + scoreKeeper.calculateScore() + "%";
            gameObject.SetActive(false);
        }
    }

    public void replayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
