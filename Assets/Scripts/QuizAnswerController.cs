using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizAnswerController : QuizController
{
    public void Init(string answer, bool isCorrect)
    {
        var answerGo = FindWithTag(transform, "QuizAnswersText");
        if (answerGo != null)
        {
            answerGo.GetComponent<TextMeshProUGUI>()?.SetText(answer);
        }
        else
        {
            Assert.IsTrue(false, "Can't find tag QuizAnswersText");
        }
    }
}
