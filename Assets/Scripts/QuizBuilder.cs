using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _quizQuestionPrefab;
    [SerializeField] private GameObject _quizAnswerPrefab;
    [SerializeField] private QuizData _quizData;

    private GameObject _currentQuestion = null;

    private int _totalAnswersCorrect = 0;
    private int _totalAnswers = 0;

    public void Start()
    {
        _totalAnswers = _quizData.GetTotalNumOfQuestions();
        _quizData.InitQuiz();
        BuildNextQuestion();
    }

    private static string ConvertToPercent(int numer, int denom)
    {
        var score = (float) numer / denom;
        var percentage = double.Parse(score.ToString());
        string output = percentage.ToString("p0");
        return output;
    }

    private void BuildNextQuestion()
    {
        DestroyQuestion();
        _currentQuestion = Instantiate(_quizQuestionPrefab, transform);
        var questionController = _currentQuestion.GetComponent<QuizQuestionController>();

        var question = _quizData.GetNextQuestion();
        if (question == null)
        {
            questionController.InitComplete($"Quiz complete\nYour Score is {ConvertToPercent(_totalAnswersCorrect, _totalAnswers)}");
        }
        else
        {
            questionController.Init(question, _quizAnswerPrefab, OnAnswerSelected);
        }
    }

    private void DestroyQuestion()
    {
        if (_currentQuestion != null)
        {
            _currentQuestion.gameObject.SetActive(false);
            _currentQuestion = null;
        }
    }

    public void OnAnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Answer is Correct");
            _totalAnswersCorrect++;
        }
        else
        {
            Debug.Log("Answer is Incorrect");
        }

        BuildNextQuestion();
    }   
}
