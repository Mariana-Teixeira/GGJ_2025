using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

[Serializable]
public struct QuestionContainer
{
    public string Question;
    public string Answer1;
    public string Answer2;
    public string CorrectAnswer;
}

public class ToddlerMillionaire : BaseActivity
{
    private KeyCode _player1option1;
    private KeyCode _player1option2;

    private KeyCode _player2option1;
    private KeyCode _player2option2;

    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _answer1Text;
    [SerializeField] private TMP_Text _answer2Text;
    [SerializeField] private TMP_Text _miniGameTimerText;
    [SerializeField] private TMP_Text _questionTimerText;

    // private List<List<string>> _questionsList = new List<List<string>>();
    // [SerializeField] private List<string> _question1 = new List<string>();
    // [SerializeField] private List<string> _question2 = new List<string>();
    // [SerializeField] private List<string> _question3 = new List<string>();
    // [SerializeField] private List<string> _question4 = new List<string>();

    [SerializeField] private QuestionContainer[] _questionContainers;

    private int _player1Count;
    private int _player2Count;

    private float _miniGameTime;
    private float _questionTime;

    private bool _miniGameTimer;
    private bool _correctQuestion;
    private bool _player1WrongQuestion;
    private bool _player2WrongQuestion;

    private string _answer1;
    private string _answer2;
    private string _correctAnswer;

    private void Awake()
    {
        // _question1.Add("What color is a carrot"); //orange or a carrot
        // _question1.Add("Orange");
        // _question1.Add("Carrot");
        // _question1.Add("Orange");
        //
        // _question2.Add("If you dig a 6 foot hole, how deep is that hole?"); //6 foot or 20 feet
        // _question2.Add("6 foot");
        // _question2.Add("20 feet");
        // _question2.Add("6 foot");
        //
        // _question3.Add("What is 1 - 1 equal to?"); //35 or 0
        // _question3.Add("35");
        // _question3.Add("0");
        // _question3.Add("0");
        //
        // _question4.Add("Spell BMW!"); //BAY or BMW
        // _question4.Add("B A Y");
        // _question4.Add("B M W");
        // _question4.Add("B M W");
        //
        // _questionsList.Add(_question1);
        // _questionsList.Add(_question2);
        // _questionsList.Add(_question3);
        // _questionsList.Add(_question4);

        //_miniGameTime = 20.0f;
        //_questionTime = 3.0f;
        //_player1Count = 0;
        //_player2Count = 0;

        //_miniGameTimer = false;
        //_correctQuestion = false;
        //_player1WrongQuestion = false;
        //_player2WrongQuestion = false;

        //_answer1 = "";
        //_answer2 = "";

        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        _player1option1 = SceneManager.Instance.Player1Data.PrimaryKey;
        _player1option2 = SceneManager.Instance.Player1Data.TertiaryKey;

        _player2option1 = SceneManager.Instance.Player2Data.PrimaryKey;
        _player2option2 = SceneManager.Instance.Player2Data.TertiaryKey;
    }

    public override void StartActivity()
    {
        _miniGameTime = 20.0f;
        _questionTime = 3.0f;
        _player1Count = 0;
        _player2Count = 0;

        _miniGameTimer = false;
        _correctQuestion = false;
        _player1WrongQuestion = false;
        _player2WrongQuestion = false;

        _answer1 = "";
        _answer2 = "";

        StartShow();
    }

    public override void EndActivity()
    {
        _miniGameTimer = false;
    }

    private void Update()
    {
        if (_miniGameTimer)
        {
            _miniGameTime -= Time.deltaTime;
            //show 20 second timer
            _miniGameTimerText.text = "Mini Game Time: " + (int)_miniGameTime;

            _questionTime -= Time.deltaTime;
            //show 3 second timer
            _questionTimerText.text = "Time to answer: " + (int)_questionTime;

            ShowGame();
            
            if (_miniGameTime <= 0.0f)
            {
                TimerEnded();
            }
        }
    }

    public void StartShow()
    {
        _miniGameTimer = true;

        //show first question
        // var randomStartNum = _questionsList[Random.Range(0, 4)];
        // _questionText.GetComponent<TMP_Text>().text = randomStartNum[0];
        // _answer1 = randomStartNum[1];
        // _answer2 = randomStartNum[2];
        // _answer1Text.GetComponent<TMP_Text>().text = _answer1;
        // _answer2Text.GetComponent<TMP_Text>().text = _answer2;
        // _correctAnswer = randomStartNum[3];
        
        var question = _questionContainers[Random.Range(0, _questionContainers.Length)];
        _questionText.text = question.Question;
        _answer1 = question.Answer1;
        _answer2 = question.Answer2;
        _answer1Text.text = _answer1;
        _answer2Text.text = _answer2;
        _correctAnswer = question.CorrectAnswer;
    }

    private void ShowGame()
    {
        if (Input.GetKeyDown(_player1option1) && !_player1WrongQuestion)
        {
            if (_answer1.Equals(_correctAnswer))
            {
                _player1Count++;
                _correctQuestion = true;
                Debug.Log("Correct");
            }
            else
            {
                _player1WrongQuestion = true;
                Debug.Log("Wrong");
            }
        }
        if (Input.GetKeyDown(_player1option2) && !_player1WrongQuestion)
        {
            if (_answer2.Equals(_correctAnswer))
            {
                _player1Count++;
                Debug.Log("Correct");
                _correctQuestion = true;
            }
            else
            {
                _player1WrongQuestion = true;
                Debug.Log("Wrong");
            }
        }
        
        if (Input.GetKeyDown(_player2option1) && !_player2WrongQuestion)
        {
            if (_answer1.Equals(_correctAnswer))
            {
                _player2Count++;
                Debug.Log("Correct");
                _correctQuestion = true;
            }
            else
            {
                _player2WrongQuestion = true;
                Debug.Log("Wrong");
            }
        }
        if (Input.GetKeyDown(_player2option2) && !_player2WrongQuestion)
        {
            if (_answer2.Equals(_correctAnswer))
            {
                _player2Count++;
                Debug.Log("Correct");
                _correctQuestion = true;
            }
            else
            {
                _player2WrongQuestion = true;
                Debug.Log("Wrong");
            }
        }

        if (_questionTime <= 0.0f || _correctQuestion || (_player1WrongQuestion && _player2WrongQuestion))
        {
            ChangeQuestion();
        }
    }

    private void ChangeQuestion()
    {
        _correctQuestion = false;
        _player1WrongQuestion = false;
        _player2WrongQuestion = false;
        _questionTime = 3.0f;

        // var randomNum = _questionsList[Random.Range(0, 4)];
        // _questionText.GetComponent<TMP_Text>().text = randomNum[0];
        // _answer1 = randomNum[1];
        // _answer2 = randomNum[2];
        // _answer1Text.GetComponent<TMP_Text>().text = _answer1;
        // _answer2Text.GetComponent<TMP_Text>().text = _answer2;
        // _correctAnswer = randomNum[3];
        
        var question = _questionContainers[Random.Range(0, _questionContainers.Length)];
        _questionText.text = question.Question;
        _answer1 = question.Answer1;
        _answer2 = question.Answer2;
        _answer1Text.text = _answer1;
        _answer2Text.text = _answer2;
        _correctAnswer = question.CorrectAnswer;
    }

    private void TimerEnded()
    {        
        _miniGameTimer = false;
        _miniGameTime = 20.0f;
        _questionTime = 3.0f;

        if (_player1Count == _player2Count)
        {
            //equal amount of points
            Debug.Log("Players Draw");
            _onFinish.Invoke(new ActivityData(Loser.Both));
        }
        else if (_player1Count > _player2Count)
        {
            //player 1 wins
            _onFinish.Invoke(new ActivityData(Loser.Player2));
            Debug.Log("Player1 wins");
        }
        else if (_player2Count > _player1Count)
        {
            //player 2 wins
            _onFinish.Invoke(new ActivityData(Loser.Player1));
            Debug.Log("Player2 wins");

        }

        _player1Count = 0;
        _player2Count = 0;
    }
}
