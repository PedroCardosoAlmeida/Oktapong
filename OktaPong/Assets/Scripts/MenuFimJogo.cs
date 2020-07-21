using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFimJogo : MonoBehaviour
{

    [SerializeField] GameManager gm;// Script do GameManager

    private void Start()
    {
        // Procura o GameManager na cena e armazena seu Script
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //Reinicia a partida
    public void rejogar()
    {
        SceneManager.LoadScene("Game");
    }


    //Volta para o menu inicial e reseta a pontuação dos jogadores
    public void menuInicial()
    {
        gm.resetaScores();
        SceneManager.LoadScene("Menu");

    }
}
