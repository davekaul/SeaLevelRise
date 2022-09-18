using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class QuizQuestionController : MonoBehaviour
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
    }

    private Transform FindWithTag(Transform root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag)) return t;
        }
        return null;
    }
}
