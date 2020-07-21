using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    
    public string playerAtirando;// Player atirando no momento
    public int dano;// Dano que a Bala causa no player
    public bool atravObstaculo;// Determinar se a bala atravessa obstaculos
    public int numeroQuique;// Numero de quiques permitido antes da Bala se destruida
    public float velocidade;// Velocidade da Bala
    public bool atirou = false;// Determina se a Bala foi atirada
    public Vector2 direcao;// Direção da Bala quando for atirada
    GameManager gm;// Script do GameManager

    // Start is called before the first frame update
    void Start()
    {
        direcao = Vector2.one.normalized;// Normaliza a direção
       
        numeroQuique = numeroQuique + 1;// numeroQuique + 1 para ser a o valor exato de quiques
        // Procura objeto GameManager na cena e armazena seu Script
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // Movimenta a bala quando é atirada
        if (atirou == true)
        transform.Translate((direcao * velocidade * Time.deltaTime));
    }


    // Função que checa as colisões da Bala
    private void OnTriggerEnter2D(Collider2D outro)
    {
        // Turno do player 1
        if (playerAtirando == "Player1")
        {
            if (outro.tag == "Obstaculo" && atravObstaculo == false)
            {
                // Colisão com o Obstaculo, muda a direção de acordo com a posição da bala
                if (this.transform.position.x > 0 && (this.transform.position.y > outro.transform.position.y || this.transform.position.y < outro.transform.position.y))
                {
                    direcao.x = -direcao.x;
                }
                else
                {
                    direcao.y = -direcao.y;
                }

                numeroQuique--; // Diminui a quantidade de quiques
            }

            // Colisão com as Paredes embaixo e encima
            if (outro.tag == "PBaixo")
            {
                direcao.y = -direcao.y;// Muda a direção
                numeroQuique--;// Diminui a quantidade de quiques
            }

            if (outro.tag == "PCima")
            {
                direcao.y = -direcao.y;// Muda a direção
                numeroQuique--;// Diminui a quantidade de quiques
            }


            // Colisão com os Players
            if(outro.tag == "Player1" && atirou == true)
            {
                gm.perderVida(playerAtirando, dano);// Função que gerencia a perda de vida do player
                Destroy(this.gameObject);// Destroi a bala
                
                
            }
            else if(outro.tag == "Player2" == true)
            {
                gm.perderVida(outro.tag, dano);// Função que gerencia a perda de vida do player
                Destroy(this.gameObject);// Destroi a bala
            }

            gerenciaQuiques();
        }
        //Turno do player 2
        else
        {
            // Colisão com o Obstaculo, muda a direção de acordo com a posição da bala
            if (outro.tag == "Obstaculo" && atravObstaculo == false)
            {
                if (this.transform.position.x < 0 && (this.transform.position.y > outro.transform.position.y || this.transform.position.y < outro.transform.position.y))
                {
                    direcao.x = -direcao.x;
                    
                }
                else
                {
                    direcao.y = -direcao.y;
                }

                numeroQuique--;// Diminui a quantidade de quiques

            }

            // Colisão com as Paredes embaixo e encima
            if (outro.tag == "PBaixo")
            {
                direcao.y = -direcao.y;// Muda a direção
                numeroQuique--;// Diminui a quantidade de quiquess
            }

            if (outro.tag == "PCima")
            {
                direcao.y = -direcao.y;// Muda a direção
                numeroQuique--;// Diminui a quantidade de quiquess
            }

            if (outro.tag == "Player1" && atirou == true)
            {
                gm.perderVida(outro.tag, dano);// Função que gerencia a perda de vida do player
                Destroy(this.gameObject);// Destroi a bala
            }
            else if (outro.tag == "Player2" && atirou == true)
            {
                gm.perderVida(playerAtirando, dano);// Função que gerencia a perda de vida do player
                Destroy(this.gameObject);// Destroi a bala
            }
            gerenciaQuiques();
        }

        // Colisão com as paredes atrás dos players
        if(outro.tag == "Parede")
        {
            Destroy(this.gameObject);// Destroi a bala
        }
    }

    //Função que gerencia os quiques da bala
    private void gerenciaQuiques()
    {

       
            if(numeroQuique == 0)
            {
                Destroy(this.gameObject);//Destroi a bala
            }
        
    }


   }
