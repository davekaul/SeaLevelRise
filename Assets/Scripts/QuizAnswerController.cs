using System;
using TMPro;
using UnityEngine.Assertions;
using Oculus.Interaction;

public class QuizAnswerController : QuizController
{
    public bool IsCorrect { get; private set; }

    public void Init(string answer, bool isCorrect, Action<bool> OnAnswerSelected)
    {
        IsCorrect = isCorrect;

        var answerGo = FindWithTag(transform, "QuizAnswersText");
        if (answerGo != null)
        {
            answerGo.GetComponent<TextMeshProUGUI>().SetText(answer);

            var toggle = transform.GetComponent<ToggleDeselect>();

            toggle.onValueChanged.AddListener(
                delegate
                {
                    OnAnswerSelected(IsCorrect);
                });
        }
        else
        {
            Assert.IsTrue(false, "Can't find tag QuizAnswersText");
        }
    }

    public void InitNext(string answer, Action OnAnswerSelected)
    {
        var answerGo = FindWithTag(transform, "QuizAnswersText");
        if (answerGo != null)
        {
            answerGo.GetComponent<TextMeshProUGUI>().SetText(answer);

            var toggle = transform.GetComponent<ToggleDeselect>();

            toggle.onValueChanged.AddListener(
                delegate
                {
                    OnAnswerSelected();
                });
        }
        else
        {
            Assert.IsTrue(false, "Can't find tag QuizAnswersText");
        }
    }
}
