using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class Quiz : MonoBehaviour
{
    private string pertanyaan;
    public TextMeshProUGUI pertanyaanTMP;
    [SerializedDictionary("Tombol", "Benar")]
    [HideInInspector]  public SerializedDictionary<Button, bool> kunciJawaban;
    [SerializedDictionary("Tombol", "Teks")]
    [HideInInspector] public SerializedDictionary<Button, string> teksJawaban;

    [HideInInspector] public UnityEvent onRightAnswer;
    [HideInInspector] public UnityEvent onFalseAnswer;
    public int skor;
    public TextMeshProUGUI skorText;
    public TextMeshProUGUI skorInt;
    public Button[] listJawabanButton;
    public UnityEvent onFinished;
    public List<QuizChoices> quizChoices = new();


    public void ResetSkor()
    {
        skor = 0;
        skorText.text = $"Skor : {skor}";
    }

    public void ListQuiz()
    {
        if (quizChoices[quizChoices.Count -1].isUsed == true)
        {
            Debug.Log("Beres");
            skorInt.text = skor.ToString();
            onFinished?.Invoke();

            return;
        }

        for (int i = 0; i < quizChoices.Count; i++)
        {
            if (quizChoices[i].isUsed)
            {
                continue;
            }

            for (int j = 0; j < listJawabanButton.Length; j++)
            {
                listJawabanButton[j].GetComponentInChildren<TextMeshProUGUI>().text = quizChoices[i].textAnswer[j];
                quizChoices[i].kunciJawaban.Add(listJawabanButton[j], quizChoices[i].rightAnswer[j]);
                quizChoices[i].teksJawaban.Add(listJawabanButton[j], quizChoices[i].textAnswer[j]);
            }

            pertanyaan = quizChoices[i].pertanyaan;
            kunciJawaban = quizChoices[i].kunciJawaban;
            teksJawaban = quizChoices[i].teksJawaban;
            quizChoices[i].isUsed = true;

            pertanyaanTMP.text = pertanyaan;
            break;
        }

        SetupButtons();

    }

    private void SetupButtons()
    {
        foreach (var kvp in kunciJawaban)
        {
            Button button = kvp.Key;
            bool isCorrect = kvp.Value;

            // Add a click event listener to the button
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnButtonClick(isCorrect));
        }
    }

    private void OnButtonClick(bool isCorrect)
    {
        if (isCorrect)
        {
            onRightAnswer?.Invoke();
            skor += 10;
            skorText.text = $"Skor : {skor}";
            ListQuiz();
        }
        else
        {
            onFalseAnswer?.Invoke();
            skorText.text = $"Skor : {skor}";
            ListQuiz();
        }
    }

    public void Restart()
    {
        kunciJawaban.Clear();
        teksJawaban.Clear();
        
        foreach (var quiz in quizChoices)
        {
            quiz.isUsed = false;
            quiz.kunciJawaban.Clear();
            quiz.teksJawaban.Clear();
        }
    }

    [Serializable]
    public class QuizChoices
    {
        public string pertanyaan;
        public List<bool> rightAnswer;
        public List<string> textAnswer;

        [SerializedDictionary("Tombol", "Benar")]
        [HideInInspector] public SerializedDictionary<Button, bool> kunciJawaban;
        [SerializedDictionary("Tombol", "Teks")]
        [HideInInspector] public SerializedDictionary<Button, string> teksJawaban;
        [HideInInspector] public bool isUsed = false;
    }
}
