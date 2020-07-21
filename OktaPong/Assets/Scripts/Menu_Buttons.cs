using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    // Carregar a cena do jogo
    public void Jogo()
    {
        // 1 = cena "Game"
        SceneManager.LoadScene(1);
    }

    //Fechar aplicação
    public void Sair()
    {
        Application.Quit();
    }
    

}
