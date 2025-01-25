using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ToddlerMillionaire : BaseActivity
{
    private KeyCode _player1option1 = KeyCode.Q;//SceneManager.Instance.Player1Keys.PrimaryKey;
    private KeyCode _player1option2 = KeyCode.E;//SceneManager.Instance.Player1Keys.SecondaryKey;

    private KeyCode _player2option1 = KeyCode.I;//SceneManager.Instance.Player2Keys.PrimaryKey;
    private KeyCode _player2option2 = KeyCode.P;//SceneManager.Instance.Player2Keys.SecondaryKey;

    [SerializeField] private GameObject _questionText;
    [SerializeField] private GameObject _answer1Text;
    [SerializeField] private GameObject _answer2Text;
    [SerializeField] private GameObject _miniGameTimerText;
    [SerializeField] private GameObject _questionTimerText;

    private List<List<string>> _questionsList = new List<List<string>>();
    [SerializeField] private List<string> _question1 = new List<string>();
    [SerializeField] private List<string> _question2 = new List<string>();
    [SerializeField] private List<string> _question3 = new List<string>();
    [SerializeField] private List<string> _question4 = new List<string>();

    private int _player1Count;
    private int _player2Count;

    private float _miniGameTime;
    private float _questionTime;

    private bool _miniGameTimer;
    private bool _questionSelected;

    private string _answer1;
    private string _answer2;
    private string _correctAnswer;

    private void Awake()
    {
        _question1.Add("What color is a carrot"); //orange or a carrot
        _question1.Add("Orange");
        _question1.Add("Carrot");
        _question1.Add("Orange");

        _question2.Add("If you dig a 6 foot hole, how deep is that hole?"); //6 foot or 20 feet
        _question2.Add("6 foot");
        _question2.Add("20 feet");
        _question2.Add("6 foot");

        _question3.Add("What is 1 - 1 equal to?"); //35 or 0
        _question3.Add("35");
        _question3.Add("0");
        _question3.Add("0");

        _question4.Add("Spell BMW!"); //BAY or BMW
        _question4.Add("B A Y");
        _question4.Add("B M W");
        _question4.Add("B M W");

        _questionsList.Add(_question1);
        _questionsList.Add(_question2);
        _questionsList.Add(_question3);
        _questionsList.Add(_question4);

        _miniGameTime = 20.0f;
        _questionTime = 3.0f;
        _player1Count = 0;
        _player2Count = 0;

        _miniGameTimer = false;
        _questionSelected = false;

        _answer1 = "";
        _answer2 = "";

        //this.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartShow();
    }

    public override void StartActivity()
    {
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
            _miniGameTimerText.GetComponent<TMP_Text>().text = "Mini Game Time: " + (int)_miniGameTime;

            _questionTime -= Time.deltaTime;
            //show 3 second timer
            _questionTimerText.GetComponent<TMP_Text>().text = "Time to answer: " + (int)_questionTime;

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
        var randomStartNum = _questionsList[Random.Range(0, 4)];
        _questionText.GetComponent<TMP_Text>().text = randomStartNum[0];
        _answer1 = randomStartNum[1];
        _answer2 = randomStartNum[2];
        _answer1Text.GetComponent<TMP_Text>().text = _answer1;
        _answer2Text.GetComponent<TMP_Text>().text = _answer2;
        _correctAnswer = randomStartNum[3];
    }

    private void ShowGame()
    {
        if (Input.GetKeyDown(_player1option1))
        {
            _questionSelected = true;
            if (_answer1.Equals(_correctAnswer))
            {
                _player1Count++;
                Debug.Log("Correct");

            }
            else
            {
                Debug.Log("Wrong");
            }
        }
        if (Input.GetKeyDown(_player1option2))
        {
            _questionSelected = true;
            if (_answer2.Equals(_correctAnswer))
            {
                _player1Count++;
                Debug.Log("Correct");

            }
            else
            {
                Debug.Log("Wrong");
            }
        }
        
        if (Input.GetKeyDown(_player2option1))
        {
            _questionSelected = true;
            if (_answer1.Equals(_correctAnswer))
            {
                _player2Count++;
                Debug.Log("Correct");

            }
            else
            {
                Debug.Log("Wrong");
            }
        }
        if (Input.GetKeyDown(_player2option2))
        {
            _questionSelected = true;
            if (_answer2.Equals(_correctAnswer))
            {
                _player2Count++;
                Debug.Log("Correct");

            }
            else
            {
                Debug.Log("Wrong");
            }
        }

        if (_questionTime <= 0.0f || _questionSelected)
        {
            ChangeQuestion();
        }
    }

    private void ChangeQuestion()
    {
        _questionSelected = false;
        _questionTime = 3.0f;

        var randomNum = _questionsList[Random.Range(0, 4)];
        _questionText.GetComponent<TMP_Text>().text = randomNum[0];
        _answer1 = randomNum[1];
        _answer2 = randomNum[2];
        _answer1Text.GetComponent<TMP_Text>().text = _answer1;
        _answer2Text.GetComponent<TMP_Text>().text = _answer2;
        _correctAnswer = randomNum[3];
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
        }
        else if (_player1Count > _player2Count)
        {
            //player 1 wins
            Debug.Log("Player1 wins");
        }
        else if (_player2Count > _player1Count)
        {
            //player 2 wins
            Debug.Log("Player2 wins");

        }

        _player1Count = 0;
        _player2Count = 0;
    }
}
