using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuizData", menuName = "Create new Quiz data", order = 51)]
public class QuizData : ScriptableObject
{
    [Serializable]
    public class Question
    {
        [SerializeField] private string _questionText;
        [SerializeField] private List<Answer> _answers;
    }

    [Serializable]
    public class Answer
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrect;
    }

    [SerializeField] private List<Question> _questions;

    private int _currentQuestion = -1;
    
    public void InitQuiz()
    {
        _currentQuestion = 0;
    }

    public Question GetNextQuestion()
    {
        if (_currentQuestion >= _questions.Count) return null;
        return _questions[_currentQuestion++];
    }
}
