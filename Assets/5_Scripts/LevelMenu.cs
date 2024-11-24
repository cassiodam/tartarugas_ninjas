using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
/*
    public Button[] botoes;

    private void Desbloquear()
    {
        int nivelDesbloq = PlayerPrefs.GetInt("NivelDesbloq", 1);
        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].interactable = false;
        }
        for (int i = 0; i < nivelDesbloq; i++)
        {
            botoes[i].interactable = true;
        }
    }
*/
    public void OpenLevel(int levelId)
    {
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

}
