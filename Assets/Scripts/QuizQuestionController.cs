using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using Oculus.Interaction;

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

    public void Init(QuizData.Question question, GameObject quizAnswerPrefab, Action OnAnswerSelected)
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

                var toggle = answer.GetComponent<ToggleDeselect>();

                toggle.onValueChanged.AddListener(
                    delegate
                    {
                        OnAnswerSelected();
                    });
            }
        }
        else
        {
            Assert.IsTrue(false, "QuizAnswers tag not found");
        }   
    }
}
