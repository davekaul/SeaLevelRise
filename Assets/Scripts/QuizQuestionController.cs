using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuizQuestionController : QuizController
{
    private GameObject _quizAnswerPrefab;

    private bool _disableScoring = false;

    private void SetQuestionText(string text)
    {
        DisableQuestionImage();
        DisableQuestionVideo();

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

    private void DisableQuestionText()
    {
        var questionGo = FindWithTag(transform, "QuizQuestionText");
        if (questionGo != null)
        {
            questionGo.gameObject.SetActive(false);
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestionText tag not found");
        }
    }
    
    private void SetQuestionImage(Sprite sprite)
    {
        DisableQuestionText();
        DisableQuestionVideo();

        var questionGo = FindWithTag(transform, "QuizQuestionImage");
        if (questionGo != null)
        {
            var img = questionGo.GetComponent<Image>();
            Assert.IsNotNull(img, "No image found on QuizQuestion");

            img.sprite = sprite;
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestionImage tag not found");
        }
    }

    private void DisableQuestionImage()
    {
        var questionGo = FindWithTag(transform, "QuizQuestionImage");
        if (questionGo != null)
        {
            questionGo.gameObject.SetActive(false);
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestionImage tag not found");
        }
    }

    private void SetQuestionVideo(string videoPath)
    {
        DisableQuestionText();
        DisableQuestionImage();

        var questionGo = FindWithTag(transform, "QuizQuestionVideo");
        if (questionGo != null)
        {
            var video = questionGo.GetComponent<VideoPlayer>();
            Assert.IsNotNull(video, "No video found on QuizQuestionVideo");

            video.url = Application.streamingAssetsPath + "/" + videoPath;
            video.Play();
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestionVideo tag not found");
        }
    }

    private void DisableQuestionVideo()
    {
        var questionGo = FindWithTag(transform, "QuizQuestionVideo");
        if (questionGo != null)
        {
            questionGo.gameObject.SetActive(false);
        }
        else
        {
            Assert.IsTrue(false, "QuizQuestionVideo tag not found");
        }
    }

    public void InitComplete(string successText, Action<bool> OnSelected)
    {
        SetQuestionText(successText);
        SetNextButton("Rety Quiz", OnSelected);
    }

    public void Init(QuizData.Question question, GameObject quizAnswerPrefab, Action<bool> OnAnswerSelected)
    {
        _quizAnswerPrefab = quizAnswerPrefab;

        _disableScoring = question.IsScoringDisabled();

        if (question.GetQuestionVideoPath().Any())
        {
            SetQuestionVideo(question.GetQuestionVideoPath());
        }
        else if (question.GetQuestionImage() != null)
        {
            SetQuestionImage(question.GetQuestionImage());
        }
        else
        {
            SetQuestionText(question.GetQuestionText());
        }
        
        var answerGo = FindWithTag(transform, "QuizAnswers");
        if (answerGo != null)
        {
            if (question.GetAnswers().Any())
            {
                foreach (var ansData in question.GetAnswers())
                {
                    var answer = Instantiate(_quizAnswerPrefab, answerGo);

                    var controller = answer.GetComponent<QuizAnswerController>();
                    controller.Init(ansData.GetAnswerText(), ansData.IsCorrect(), OnAnswerSelected);
                }
            }
            else
            {
                var answer = Instantiate(_quizAnswerPrefab, answerGo);
                var controller = answer.GetComponent<QuizAnswerController>();
                controller.InitNext("Next", OnAnswerSelected);
            }
        }
        else
        {
            Assert.IsTrue(false, "QuizAnswers tag not found");
        }   
    }

    public void SetResult(bool isCorrect, Action<bool> OnSelected)
    {
        if (_disableScoring)
        {
            OnSelected(false);
            return;
        }

        SetQuestionText(isCorrect ? "Correct!" : "Incorrect");
        var answers = transform.GetComponentsInChildren<QuizAnswerController>().ToList();
        answers.ForEach(x => x.gameObject.SetActive(false));
        SetNextButton("Next Question", OnSelected);
    }

    private void SetNextButton(string text, Action<bool> OnSelected)
    {
        var answerGo = FindWithTag(transform, "QuizAnswers");
        if (answerGo != null)
        {
            // TODO: We get a reference error here, probably need to cache a different way
            var answer = Instantiate(_quizAnswerPrefab, answerGo);

            var controller = answer.GetComponent<QuizAnswerController>();
            controller.InitNext(text, OnSelected);
        }
        else
        {
            Assert.IsTrue(false, "QuizAnswers tag not found");
        }
    }
}
