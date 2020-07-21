using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] Player player1; // Script player do player 1
    [SerializeField] Player player2; // Script player do player 2

    [SerializeField] Text vidaPlayer1; // Texto que conta a vida do player 1
    [SerializeField] Text vidaPlayer2; // Texto que conta a vida do player 2 
    [SerializeField] Text nomeBalaP1; // Texto que guarda o nome da bala usada pelo player 1
    [SerializeField] Text nomeBalaP2; // Texto que guarda o nome da bala usada pelo player 2
    [SerializeField] Text scorePlayer1; // Texto que guarda a pontuação do player 1
    [SerializeField] Text scorePlayer2; //Texto que guarda a pontuação do player 2
    [SerializeField] GameObject panelFimJogo; //painel que é ativado quando uma partida termina
    [SerializeField] Text textoPanel; // Texto do painel que muda de acordo com qual player venceu a partida
    private static int scoreP1 = 0; // Variável estática para guarda a pontuação do player 1 quando a cena é recarregada
    private static int scoreP2 = 0; // Variável estática para guarda a pontuação do player 1 quando a cena é recarregada


    // Start is called before the first frame update
    void Start()
    {   
        // Armazena o Script dos players 
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>(); 
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();

        // Texto recebe do valor da pontuação de cada player
        scorePlayer1.text = scoreP1.ToString();
        scorePlayer2.text = scoreP2.ToString();

        // Texto recebe o valor das vidas iniciais de cada player
        vidaPlayer1.text = player1.vida.ToString();
        vidaPlayer2.text = player2.vida.ToString();


        // A partida começa no turno do player 1, logo a variável podeMover do player 1 começa com valor true
        player1.podeMover = true;
        player2.podeMover = false;

    }

    // Update is called once per frame
    void Update()
    {
       
        gerenciarTurnos();


        // Troca o nome das balas
        if(player1.balaUsada != null)
            nomeBalaP1.text = player1.balaUsada.name.ToString().Replace("(Clone)","");

        if (player2.balaUsada != null)
            nomeBalaP2.text = player2.balaUsada.name.ToString().Replace("(Clone)", "");
    }


    //Função usada para gerenciar os turnos
    private void gerenciarTurnos()
    {
        GameObject acharBala = GameObject.FindGameObjectWithTag("Bala"); //Procura um objeto Bala na cena


        if (acharBala == null) // Se acharBala for nulo, faz a troca de turno
        {
            // Se for o turno do player 1
            if (player1.passarTurno == true)
            {
                player2.podeMover = true; // Variável que permite movimentação
                player1.passarTurno = false; // Passa a variável para falso para o próximo turno
                nomeBalaP1.text = string.Empty; // Texto com o nome da bala é apagado
                


            }
            // Se for o turno do player 2
            else if (player2.passarTurno == true)
            {
                player1.podeMover = true;// Variável que permite movimentação
                player2.passarTurno = false;// Passa a variável para falso para o próximo turno
                nomeBalaP2.text = string.Empty;// Texto com o nome da bala é apagado

            }
        }
    }

    //Função que genencia a perda de vida dos players
    public void perderVida(string tag, int dano)
    {
        if(tag == "Player1")
        {
            
            player1.vida -= dano; // Perde vida igual ao dano da Bala
            vidaPlayer1.text = player1.vida.ToString();// Atualiza o texto da vida
            
        }
        else if(tag == "Player2")
        {
            player2.vida -= dano;// Perde vida igual ao dano da Bala
            vidaPlayer2.text = player2.vida.ToString();// Atualiza o texto da vida

        }

        
        checaFimDeJogo();
    }

    //Função para checar se a partida acabou
    private void checaFimDeJogo()
    {
        if(player1.vida <= 0) // Se a vida do player 1 chegou a 0 ou menos que 0
        {
            textoPanel.text = "Player2 Venceu!!!";// atualiza o texto do painel de encerramento
            panelFimJogo.SetActive(true);// Ativa o painel de encerramneto
            scoreP2++;// Aumenta a pontuação do player 2
            scorePlayer2.text = scoreP2.ToString();// Atualiza o texto de pontuação
            //Impede que os players se movimentem com a tela de encerramento ativada
            player1.podeMover = false;
            player2.podeMover = false;
        }
        else if(player2.vida <= 0)// Se a vida do player 2 chegou a 0 ou menos que 0
        {
            textoPanel.text = "Player1 Venceu!!!";// atualiza o texto do painel de encerramento
            panelFimJogo.SetActive(true);// Ativa o painel de encerramneto
            scoreP1++;// Aumenta a pontuação do player 2
            scorePlayer1.text = scoreP1.ToString();// Atualiza o texto de pontuação
            //Impede que os players se movimentem com a tela de encerramento ativada
            player1.podeMover = false;
            player2.podeMover = false;
        }


    }

    //Função usada para resetar as pontuações quando volta ao menu principal
    public void resetaScores()
    {
        scoreP1 = 0;
        scoreP2 = 0;
    }

}
