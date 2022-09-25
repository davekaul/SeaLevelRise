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

    public Action OnQuizComplete = delegate { };

    public void Start()
    {
        _quizData.InitQuiz();
        BuildNextQuestion();
    }

    private void BuildNextQuestion()
    {
        var question = _quizData.GetNextQuestion();
        if (question == null)
        {
            OnQuizComplete();
        }
        else
        {
            DestroyQuestion();
            _currentQuestion = Instantiate(_quizQuestionPrefab, transform);
            var questionController = _currentQuestion.GetComponent<QuizQuestionController>();
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

    public void OnAnswerSelected()
    {
        BuildNextQuestion();
    }   
}
