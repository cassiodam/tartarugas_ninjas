using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pontuacao : MonoBehaviour
{

    public static Pontuacao instanciar;
    public Text Placar;
    public Text Recorde;

    int pontos = 0;
    int recorde = 0;

    private void Awake()
    {
        instanciar = this;
    }
    void Start()
    {
        recorde = PlayerPrefs.GetInt("recorde", 0);
        Placar.text = "Pontuacao: " + pontos.ToString();
        Recorde.text = "Recorde: " + recorde.ToString();
    }


public void AtualizaPontos()
{
    pontos += 1;
    Placar.text = "Pontuacao: " + pontos.ToString();
    if(recorde < pontos)
    PlayerPrefs.SetInt("recorde", pontos);
}

}
