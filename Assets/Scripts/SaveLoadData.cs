using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadData : MonoBehaviour
{
    public Quiz[] quiz;
    public int[] savedScore;
    public Button[] level;
    public TextMeshProUGUI[] showedScore;


    private void Start()
    {

        for (int i = 0; i < quiz.Length; i++)
        {
            savedScore[i] = PlayerPrefs.GetInt(quiz[i].ToString());
            showedScore[i].text = savedScore[i].ToString();
        }
        Level();

    }

    public void SaveDatas() 
    {
        for (int i = 0; i < quiz.Length; i++)
        {
            if (quiz[i].skor > savedScore[i])
            {
                PlayerPrefs.SetInt(quiz[i].ToString(), quiz[i].skor);
                savedScore[i] = PlayerPrefs.GetInt(quiz[i].ToString());
                showedScore[i].text = savedScore[i].ToString();
            }
        }
    }

    public void LoadDatas()
    {
        for (int i = 0; i < quiz.Length; i++)
        {
            PlayerPrefs.GetInt(quiz[i].ToString());
        }
    }

    public void Level()
    {
        for (int i = 0; i < savedScore.Length-1; i++)
        {
            if (savedScore[i] >= 70)
            {
                Debug.Log("Ini lolos" + savedScore[i]);
                level[i+1].interactable = true;
            }
        }
    }
}
