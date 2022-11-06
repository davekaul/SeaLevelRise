using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class QuizBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _quizQuestionPrefab;
    [SerializeField] private GameObject _quizAnswerPrefab;
    [SerializeField] private QuizData _quizData;
    [SerializeField] private MountPointController _mountPointController;

    private GameObject _currentQuestion = null;

    private int _totalAnswersCorrect = 0;
    private int _totalAnswers = 0;

    private WaterLevelController _waterLevelController;

    private Coroutine _currentCoroutine = null;

    public void Start()
    {
        _waterLevelController = FindObjectOfType<WaterLevelController>();
        Assert.IsNotNull(_waterLevelController);

        _totalAnswers = _quizData.GetTotalNumOfQuestions();
        _quizData.InitQuiz();
        BuildNextQuestion();
    }

    private static string ConvertToPercent(int numer, int denom)
    {
        var score = (float) numer / denom;
        var percentage = double.Parse(score.ToString());
        string output = percentage.ToString("p0");
        return output;
    }

    private void BuildNextQuestion()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        DestroyQuestion();
        _currentQuestion = Instantiate(_quizQuestionPrefab, transform);
        var questionController = _currentQuestion.GetComponent<QuizQuestionController>();

        var question = _quizData.GetNextQuestion();
        if (question == null)
        {
            var msg = ((float)_totalAnswersCorrect / _totalAnswers) > 0.8f ? "Victory!" : "You Failed :`(";
            questionController.InitComplete($"{msg}\nYour Score is {ConvertToPercent(_totalAnswersCorrect, _totalAnswers)}", ReloadScene, _quizAnswerPrefab);
        }
        else
        {
            _mountPointController.MountByIndex(question.GetCurrentMountPoint());
            questionController.Init(question, _quizAnswerPrefab, OnAnswerSelected, OnScoreUpdated);
        }
    }

    private void DestroyQuestion()
    {
        if (_currentQuestion != null)
        {
            _currentQuestion.gameObject.SetActive(false);
            _currentQuestion = null;
        }
    }

    public void OnScoreUpdated(bool isCorrect)
    {
        _currentQuestion.GetComponent<QuizQuestionController>().SetResult(isCorrect, BuildNextQuestion);

        _waterLevelController?.SetLevel(isCorrect);

        if (isCorrect)
        {
            Debug.Log("Answer is Correct");
            _totalAnswersCorrect++;
        }
        else
        {
            Debug.Log("Answer is Incorrect");
        }
    }

    public void OnAnswerSelected()
    {
        _currentCoroutine = StartCoroutine(DisplayAnswerResult()); 
    }

    private IEnumerator DisplayAnswerResult()
    {
        yield return new WaitForSeconds(1f);
        BuildNextQuestion();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
