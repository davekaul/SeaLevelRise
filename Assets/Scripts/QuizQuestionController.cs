using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizQuestionController : QuizController
{
    public void InitComplete(string successText)
    {
        var questionGo = FindWithTag(transform, "QuizQuestionText");
        if (questionGo != null)
        {
            var text = questionGo.GetComponent<TextMeshProUGUI>();
            Assert.IsNotNull(text, "No text found on QuizQuestion");

            text.text = successText;
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestion tag not found");
        }
    }

    public void Init(QuizData.Question question, GameObject quizAnswerPrefab, Action<bool> OnAnswerSelected)
    {
        var questionGo = FindWithTag(transform, "QuizQuestionText");
        if (questionGo != null)
        {
            var text = questionGo.GetComponent<TextMeshProUGUI>();
            Assert.IsNotNull(text, "No text found on QuizQuestion");

            text.text = question.GetQuestionText();
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestion tag not found");
        }

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

    private void UpdateScore(bool isCorrect)
    {
        // TODO: Cache question count and score as static and provide to InitComplete
    }
}
