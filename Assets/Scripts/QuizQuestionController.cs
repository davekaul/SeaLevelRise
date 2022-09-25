using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizQuestionController : QuizController
{
    public void Init(QuizData.Question question, GameObject quizAnswerPrefab)
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
                controller.Init(ansData.GetAnswerText(), ansData.IsCorrect());

                // TODO: Rig onclick event to load next question, or show completeion canvas
            }
        }
        else
        {
            Assert.IsTrue(false, "QuizAnswers tag not found");
        }   
    }
}
