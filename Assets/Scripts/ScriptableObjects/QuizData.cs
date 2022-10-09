using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuizData", menuName = "Create new Quiz data", order = 51)]
public class QuizData : ScriptableObject
{
    [Serializable]
    public class Question
    {
        // Video > Image > Text, do not add multiple fields
        [SerializeField] private string _questionText;
        [SerializeField] private Sprite _questionImage;
        [SerializeField] private string _questionVideoPath;

        [SerializeField] private bool _disableScoring;

        // If scoring is disabled then we auto generate the next button, no need to populate these answers
        [SerializeField] private List<Answer> _answers;

        public bool IsScoringDisabled()
        {
            return _disableScoring;
        }

        public string GetQuestionText()
        {
            return _questionText;
        }

        public Sprite GetQuestionImage()
        {
            return _questionImage;
        }

        public string GetQuestionVideoPath()
        {
            return _questionVideoPath;
        }

        public List<Answer> GetAnswers()
        {
            return new List<Answer>(_answers);
        }
    }

    [Serializable]
    public class Answer
    {
        [SerializeField] private string _answerText;
        [SerializeField] private bool _isCorrect;
        public string GetAnswerText()
        {
            return _answerText;
        }

        public bool IsCorrect()
        {
            return _isCorrect;
        }
    }

    [SerializeField] private List<Question> _questions;

    public int GetTotalNumOfQuestions()
    {
       return _questions.FindAll(x => !x.IsScoringDisabled()).Count;
    }
    

    private int _currentQuestion = -1;
    
    public void InitQuiz(bool dummy = false)
    {
        _currentQuestion = 0;
    }

    public Question GetNextQuestion()
    {
        if (_currentQuestion >= _questions.Count) return null;
        return _questions[_currentQuestion++];
    }
}
