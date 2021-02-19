using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public bool attachedToWood = false;
    bool fallKnife;
    public GameConroller _gameController;

    public GameObject particleFX;
    public Transform particlePOS;

    public AudioSource knifeInWoodSound;
    public AudioSource knifeFallSound;
    public AudioSource knifeAppleSound;

    private void Awake()
    {
        _gameController = GameObject
        .FindGameObjectWithTag("GameController")
        .GetComponent<GameConroller>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wood")
        {

            if (_gameController.isInvoke == false) { knifeInWoodSound.Play(); }

            //Instantiate(particleFX, particlePOS);

            rigidbody.velocity = Vector2.zero;
            transform.parent = col.transform;
            attachedToWood = true;

            if (_gameController.knifeCount == 0 && _gameController.gameIsOver == false)
            {
                if (_gameController.isAppleСhallenge == true  && _gameController.appleInWood > 0 && _gameController.currentStage == 5) 
                {
                     _gameController.gameIsOver = true; _gameController.Invoke("GameOverScreen", 0.8f); 
                }

                _gameController.InvokeDestroyWood();

                attachedToWood = false;
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15, ForceMode2D.Impulse);

                _gameController.knifeCount += 1;
                //_gameController.knifeSpawner.InstanseNewKnife();

                //Vibration.Init();
                //Vibration.VibratePop();



                _gameController.SetGameUIList(false);
                _gameController.NewStage();
                
            }
            else if (_gameController.knifeCount == 0 && _gameController.gameIsOver == false)
            {
                if (_gameController.isInvoke == false)
                    _gameController.gameIsOver = true;

                //Vibration.Init();
                //Vibration.VibrateNope();

                if (_gameController.knifeSpawner.knife != null) { Destroy(_gameController.knifeSpawner.knife.gameObject); }
                _gameController.Invoke("GameOverScreen", 0.8f);

            }
   
            
        }
        if (col.tag == "Knife" && _gameController.isInvoke == false)
        {
            if (col.gameObject.GetComponent<Knife>().attachedToWood == true && _gameController.gameIsOver == false)
            {
                knifeFallSound.Play();
                //Vibration.Init();
                //Vibration.VibratePeek();

                rigidbody.velocity = Vector2.zero;
                rigidbody.gravityScale = 20f;

                if (_gameController.isInvoke == false)
                    _gameController.gameIsOver = true;


                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(-15, 15), ForceMode2D.Impulse);

                if (_gameController.knifeSpawner.knife != null) { Destroy(_gameController.knifeSpawner.knife.gameObject); }
                _gameController.Invoke("GameOverScreen", 0.8f);

            }     
        }
        if (col.tag == "Apple" && _gameController.isInvoke == false)
        {
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 20f;
            col.transform.Rotate(0, 0, 30);
            col.transform.parent = null;
            //knifeAppleSound.Play();

            int appleCount;
            appleCount = PlayerPrefs.GetInt("AppleMax");
            appleCount += 1;
            PlayerPrefs.SetInt("AppleMax", appleCount);

            _gameController.appleCountText.text = "" + appleCount;
            _gameController.appleCount = appleCount;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Wood")
        {
            //Destroy(gameObject);
        }
    }

    private void fallKnifes()
    {
        if (attachedToWood == false && fallKnife == true)
        {
            gameObject.GetComponent<Transform>().transform.parent = null;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 20f;
        }
    }

    private void FixedUpdate()
    {
        if (_gameController.gameIsOver == true 
            && _gameController.isInvoke == false 
            && gameObject.GetComponent<Knife>().attachedToWood == false
            && gameObject != _gameController.knifeSpawner.knife)
        {
            var rot = _gameController.speedKnifeFall * Time.deltaTime;
            transform.Rotate(0, 0, rot); 
        }
        if (attachedToWood == false && fallKnife == true)
        {
            gameObject.GetComponent<Transform>().transform.parent = null;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 20f;
        }
    }
}
