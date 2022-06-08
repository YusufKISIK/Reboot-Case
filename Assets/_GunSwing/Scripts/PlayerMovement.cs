using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cinemachine;
using Image = UnityEngine.UI.Image;


public class PlayerMovement : MonoBehaviour
{
  public static GameState _gameState;
    public CinemachineVirtualCamera vcm;
    //GENERAL-COMPONENT PROPERTIES
    CharacterController _controller;
    PlayerAction player;
    
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
    public static int CollectedObject;
    public ParticleSystem _starStunned;
    
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
        _gameState = GameState.None;
        _controller = GetComponent<CharacterController>();
        player.Movement.TouchPress.started += _ => TouchStart();
        player.Movement.TouchPress.canceled += _ => TouchEnd();
        velocity = Vector3.zero;

        canrun = true;
    }

    private void TouchStart()
    {
        if (_gameState != GameState.End && _gameState != GameState.Fail && canrun == true)
        {
            
            activateInput = true;
            _gameState = GameState.Run;
            TapToMove.SetActive(false);
      
        }
    }
    private void TouchEnd()
    {
        if (_gameState != GameState.End)
        {
            activateInput = false;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
        
        }
    }
    
    


}
