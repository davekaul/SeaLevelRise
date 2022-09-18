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
            _quizQuestionPrefab = Instantiate(_quizQuestionPrefab, transform);
            var questionController = _quizQuestionPrefab.GetComponent<QuizQuestionController>();
            questionController.Init(question, _quizAnswerPrefab);
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
