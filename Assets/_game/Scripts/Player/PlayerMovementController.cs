using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using _game.Scripts.Level;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    #region Movement
    public bool Rotation;
    private Vector2 _input;//Input Systemden ald���m�z �nputlar
    private Vector3 _direction;//Karakterimizin haraket 3D edece�i y�nler
    [SerializeField] private float _speed;//Karakterimizin h�z�
    [SerializeField] private float _speedMultiplier;//Ko�u i�in bulunan h�z �arpan�
    [SerializeField] private bool run;//Karakterimizin ko�u durumu
    [HideInInspector] public GameObject door;
    [HideInInspector] public float DoorRotation;
    public bool CanInteractive;
    public bool CrouchBool;
    public bool NeedCrouch;
    #endregion//Haraket i�in bulunan de�i�kenler

    public LevelManager levelManager;
    
    #region Rotation
    [SerializeField] private float smoothTime = 0.05f;//D�n�� i�in yumu�atma
    private float _currentVelocity;
    #endregion//Karakterimizin bakt��� y�n

    #region Gravity
    private float _gravity = -9.81f;// yer�ekimi
    [SerializeField] private float gravityMultiplier = 3.0f;//yer�ekimi �arpan�
    private float _velocity; //karaktere uygulanan yer�ekimi
    #endregion// Yer�ekimi de�i�kenleri

    #region Jump
    [SerializeField] private float _jumpPower;//z�plama g�c�m�z
    #endregion

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Gravity();
        ApplyRotation();
        if (Rotation)
        {
            ApplyRotation();
            ApplyMovement();
        }
        Gravity();
    }

    public void Move(InputAction.CallbackContext context)//Move eventinin tu�lar�n� kullan�ld�k�a �a�r�lan fonksiyon
    {
        _input = context.ReadValue<Vector2>();//Input systemden 2D de�i�kenleri �ekiyor
        _direction = new Vector3(_input.x, 0, _input.y);// �ekti�i de�erleri karkterin x ve z eksenine veriyor
    }
    public void Run(InputAction.CallbackContext context)//Run eventinin left shifte bas�nca �a�r�lan fonksiyon
    {
        if (context.started)// left shifte basar isek ko�u durumuna ge�iyoruz
        {
            run = true;
            return;
        }
        if (context.canceled)// iptal edersek basmay� b�rak�rsak ko�u durumunu iptal ediyoruz
        {
            run = false;
            return;
        }
    }
    public void Jump(InputAction.CallbackContext context)//Z�plama eventinin tu�u kullan�nca �a�r�lan fonksiyon
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += _jumpPower;
    }
    public void Crouch(InputAction.CallbackContext context)//E�ilme eventinin tu�u kullan�ld�k�a �a�r�lacak fonksiyon
    {
        if (context.started)
        {
            //transform.localScale = new Vector3(1f, 0.5f, 1f);
            CrouchBool = true;
        }
        if (context.canceled && !NeedCrouch)
        {
            //transform.localScale = Vector3.one;
            CrouchBool = false;
        }
    }
    public void Interactive(InputAction.CallbackContext context)
    {
        if (context.started && CanInteractive)
        {
            // Debug.Log("Yeni sahneyi y�kle");
            // OpenDoor(DoorRotation);
            levelManager.LoadNextLevel();
            return;
        }
    }

    private void ApplyMovement()//Karakterin haraket kodlar�
    {
        if (run != true)
            _characterController.Move(_direction * _speed * Time.deltaTime);//�ekilen de�erler �zerinden ko�muyor ise hareket d�zenlenmesi
        else
            _characterController.Move(_direction * _speed * Time.deltaTime * _speedMultiplier);//�ekilen de�erler �zerinden ko�uyor ise hareket d�zenlenmesi
    }
    private void ApplyRotation()//Karakterimizin bak�� noktas�
    {
        if (_input.sqrMagnitude == 0) return;// herhangi bir input alm�yorsak karakter bakt��� yere bakmaya devam edecek
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;// karakterin bakmas�n� istedi�imi noktan�n a��s�n� tutuyor
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);// istedi�imiz a��ya daha yumu�ak bir �ekilde bak�yor
        transform.rotation = Quaternion.Euler(0f, angle, 0f);// bu a�� uygula�yor
        //transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, angle, 0f), 1f * Time.deltaTime);
    }
    private void Gravity()//Yer�ekimi kodlar�
    {
        if (IsGrounded() && _velocity < 0.0f)// Karakter zeminde ve ona uygulanan yer�ekimi 0 dan fazla ise
        {
            _velocity = -1.0f;//yer�ekimi a��a�� y�nde 1 olarak uygulan�yor
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime; // karkter z�plama durumunda ise ona uygulanan yer�ekimi zamanla art�yor
        }
        _direction.y = _velocity; // yer�ekimi karaktere uygulan�yor
    }
    private void OpenDoor(float DoorRotation)
    {
        door.transform.DORotate(new Vector3(0f, DoorRotation, 0f), 1f);
    }

    private bool IsGrounded() => _characterController.isGrounded;//Karakteri yerde olup olmad���n� kontrol ediyor
}
