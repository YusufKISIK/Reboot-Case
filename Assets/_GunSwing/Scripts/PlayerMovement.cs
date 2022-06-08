using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cinemachine;
using Sirenix.Utilities;
using DG.Tweening;
using Image = UnityEngine.UI.Image;


public class PlayerMovement : MonoBehaviour
{
    public static GameState _gameState;
    public CinemachineVirtualCamera vcm;
    //GENERAL-COMPONENT PROPERTIES
    CharacterController _controller;
    PlayerAction player;
    public Rigidbody playerrb;
    public Animator PlayerAnimator;
    [SerializeField] Transform model;
    //SERILIZEFIELD PROPERTIES
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _sideMoveSpeed;
    [SerializeField] float _strafeSpeed;
    [SerializeField] float yRotateLimit;
    [SerializeField] WaitForSeconds _waitForSeconds;
    [SerializeField] TextMeshProUGUI CollectedItemText;

    [SerializeField] GameObject TapToMove;
    //BOOL PROPERTIES
    public static bool activateInput;
    bool _isDwarf;
    //FLOAT PROPERTIES
    [SerializeField] float leftXClamp;
    [SerializeField] float rightXClamp;
    //VECTOR PROPERTIES   
    Vector3 _leanStartVector;
    Vector3 _leanEndVector;
    private Vector3 velocity;
    public ParticleSystem _hitParticleSystem;
    public UIScript UIgo;
    public static bool canrun;
    public static bool enemyCanMove = true;
    public static bool enemyWins = false;
    public static int CollectedObject;
    public ParticleSystem _starStunned;
    
    public List<Enemie> enemy = new List<Enemie>();
    public GameObject[] enemyParent;
    public GameObject[] guns;
    public int gunLvl;
    public int gunNumb;
    
    

    Color color;
    private void OnEnable()
    {
        player.Enable();
    }
    private void OnDisable()
    {
        player.Disable();
    }
    private void Awake()
    {
        player = new PlayerAction();
    }
    void Start()
    {
        CollectedObject = 0;
        _controller = GetComponent<CharacterController>();
        player.Movement.TouchPress.started += _ => TouchStart();
       // player.Movement.TouchPress.canceled += _ => TouchEnd();
        velocity = Vector3.zero;
        gunNumb = 1;

        canrun = true;

        
        enemy.SetLength(enemyParent[UIScript.currentLevel - 1].transform.childCount);

        for (int i = 0; i < enemyParent[UIScript.currentLevel - 1].transform.childCount; i++)
        {
            enemy[i] = enemyParent[UIScript.currentLevel - 1].transform.GetChild(i).GetComponent<Enemie>();
        }
    }

    private void TouchStart()
    {
        if (_gameState != GameState.End && _gameState != GameState.Fail && canrun == true)
        {
            
            activateInput = true;
            _gameState = GameState.Run;
            TapToMove.SetActive(false);
            Enemie.GameStarted = true;
           // PlayerAnimator.Play("Running Backward");
        }
    }
 
    void Update()
    {
        if (_gameState == GameState.Run)
        {
            if (_gameState == GameState.Fail)
            {
                return;
            }
            velocity.y += -9.81f * Time.deltaTime;
            _controller.Move(velocity * Time.deltaTime);
            float rotation;
            if (activateInput)
            {
                float strafeDelta = player.Movement.Delta.ReadValue<Vector2>().x;
                _controller.Move(transform.right * strafeDelta * _strafeSpeed * Time.deltaTime);

                if (strafeDelta == 0)
                {
                    rotation = model.localRotation.eulerAngles.y;
                    if (rotation != 0)
                    {
                        var fixAmount = yRotateLimit * Time.deltaTime;
                        if (rotation < 180) fixAmount *= -1;
                        model.Rotate(0, fixAmount, 0);
                    }
                }
                else
                {
                    model.Rotate(0, strafeDelta * yRotateLimit * Time.deltaTime, 0);
                    rotation = model.localRotation.eulerAngles.y;

                    if (rotation > 180) rotation -= 360;
                    model.localRotation = Quaternion.Euler(model.localRotation.eulerAngles.x, Mathf.Clamp(rotation, -yRotateLimit, yRotateLimit), model.localRotation.eulerAngles.z);
                }
            }
            else
            {
                rotation = model.localRotation.eulerAngles.y;
                if (rotation != 0)
                {
                    var fixAmount = yRotateLimit * Time.deltaTime;
                    if (rotation < 180) fixAmount *= -1;

                    model.Rotate(0, fixAmount, 0);
                }
            }
            if (activateInput)
            {
                _controller.Move(transform.forward * _forwardSpeed * Time.deltaTime);
                var position = transform.position;
                position.x = Mathf.Clamp(position.x, leftXClamp, rightXClamp);
                transform.position = position;
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemy.Count <= 0)
        {
            if (_gameState == GameState.Run)
            {
                UIgo.Success(enemy.Count);
                Enemie.GameStarted = false;
                _gameState = GameState.End;
                playerrb.isKinematic = true;
            }
        }
        else
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].enemyAllive)
                {
                    enemy.RemoveAt(i);
                }
            } 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            if (gunLvl <= 3)
            {
                gunLvl++;
                for (int i = 0; i < guns.Length; i++)
                {
                    guns[i].transform.GetChild(gunLvl - 1).gameObject.SetActive(false);
                    guns[i].transform.GetChild(gunLvl).gameObject.SetActive(true);
                }
                other.gameObject.transform.DOScale(Vector3.zero, 1f);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
           
        }
        if (other.gameObject.tag == "Pistol")
        {
            gunNumb++;
            guns[gunNumb].SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                guns[gunNumb].transform.GetChild(i).gameObject.SetActive(false);
            }
            guns[gunNumb].transform.GetChild(0).gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.tag == "Rifle")
        {
            gunNumb++;
            guns[gunNumb].SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                guns[gunNumb].transform.GetChild(i).gameObject.SetActive(false);
            }
            guns[gunNumb].transform.GetChild(1).gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.tag == "Shotgun")
        {
            gunNumb++;
            guns[gunNumb].SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                guns[gunNumb].transform.GetChild(i).gameObject.SetActive(false);
            }
            guns[gunNumb].transform.GetChild(2).gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "LaserGun")
        {
            gunNumb++;
            guns[gunNumb].SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                guns[gunNumb].transform.GetChild(i).gameObject.SetActive(false);
            }
            guns[gunNumb].transform.GetChild(3).gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Enemie.GameStarted = false;
            enemyWins = true;
            _gameState = GameState.End;
            playerrb.isKinematic = true;
            UIgo.Fail();
        }
    }
    
    


}
