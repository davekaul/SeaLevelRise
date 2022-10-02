using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizQuestionController : QuizController
{
    private void SetQuestionText(string text)
    {
        var questionGo = FindWithTag(transform, "QuizQuestionText");
        if (questionGo != null)
        {
            var txt = questionGo.GetComponent<TextMeshProUGUI>();
            Assert.IsNotNull(text, "No text found on QuizQuestion");

            txt.text = text;
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestion tag not found");
        }
    }
    
    public void InitComplete(string successText)
    {
        SetQuestionText(successText);
    }

    public void Init(QuizData.Question question, GameObject quizAnswerPrefab, Action<bool> OnAnswerSelected)
    {
        SetQuestionText(question.GetQuestionText());
        
        var answerGo = FindWithTag(transform, "QuizAnswers");
        if (answerGo != null)
        {
            foreach (var ansData in question.GetAnswers())
            {
                var answer = Instantiate(quizAnswerPrefab, answerGo);

                var controller = answer.GetComponent<QuizAnswerController>();
                controller.Init(ansData.GetAnswerText(), ansData.IsCorrect(), OnAnswerSelected);
            }
        }
        else
        {
            Assert.IsTrue(false, "QuizAnswers tag not found");
        }   
    }

    public void SetResult(bool isCorrect)
    {
        SetQuestionText(isCorrect ? "Correct!" : "Incorrect");
        var answers = transform.GetComponentsInChildren<QuizAnswerController>().ToList();
        answers.ForEach(x => x.gameObject.SetActive(false));

    }
}
