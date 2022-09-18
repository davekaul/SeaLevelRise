using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuizData", menuName = "Create new Quiz data", order = 51)]
public class QuizData : ScriptableObject
{
    [Serializable]
    private class Question
    {
        [SerializeField] private string _questionText;
        [SerializeField] private List<Answer> _answers;
    }

    [Serializable]
    private class Answer
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrect;
    }

    [SerializeField] private List<Question> _questions;
}
