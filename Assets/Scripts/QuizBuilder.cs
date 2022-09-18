using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _quizPrefab;
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
            _quizPrefab = Instantiate(_quizPrefab, transform);
            var questionController = _quizPrefab.GetComponent<QuizQuestionController>();
            questionController.Init(question);
        }
    }

    private void DestroyQuestion()
    {
        if (_currentQuestion != null) Destroy(_currentQuestion);
    }

    public void OnAnswerSelected()
    {
        BuildNextQuestion();
    }   
}
