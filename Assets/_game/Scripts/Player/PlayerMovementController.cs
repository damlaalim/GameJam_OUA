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
    private Vector2 _input;//Input Systemden aldýðýmýz ýnputlar
    private Vector3 _direction;//Karakterimizin haraket 3D edeceði yönler
    [SerializeField] private float _speed;//Karakterimizin hýzý
    [SerializeField] private float _speedMultiplier;//Koþu için bulunan hýz çarpaný
    [SerializeField] private bool run;//Karakterimizin koþu durumu
    [HideInInspector] public GameObject door;
    [HideInInspector] public float DoorRotation;
    public bool CanInteractive;
    public bool CrouchBool;
    public bool NeedCrouch;
    #endregion//Haraket için bulunan deðiþkenler

    public LevelManager levelManager;
    
    #region Rotation
    [SerializeField] private float smoothTime = 0.05f;//Dönüþ için yumuþatma
    private float _currentVelocity;
    #endregion//Karakterimizin baktýðý yön

    #region Gravity
    private float _gravity = -9.81f;// yerçekimi
    [SerializeField] private float gravityMultiplier = 3.0f;//yerçekimi çarpaný
    private float _velocity; //karaktere uygulanan yerçekimi
    #endregion// Yerçekimi deðiþkenleri

    #region Jump
    [SerializeField] private float _jumpPower;//zýplama gücümüz
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

    public void Move(InputAction.CallbackContext context)//Move eventinin tuþlarýný kullanýldýkça çaðrýlan fonksiyon
    {
        _input = context.ReadValue<Vector2>();//Input systemden 2D deðiþkenleri çekiyor
        _direction = new Vector3(_input.x, 0, _input.y);// Çektiði deðerleri karkterin x ve z eksenine veriyor
    }
    public void Run(InputAction.CallbackContext context)//Run eventinin left shifte basýnca çaðrýlan fonksiyon
    {
        if (context.started)// left shifte basar isek koþu durumuna geçiyoruz
        {
            run = true;
            return;
        }
        if (context.canceled)// iptal edersek basmayý býrakýrsak koþu durumunu iptal ediyoruz
        {
            run = false;
            return;
        }
    }
    public void Jump(InputAction.CallbackContext context)//Zýplama eventinin tuþu kullanýnca çaðrýlan fonksiyon
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += _jumpPower;
    }
    public void Crouch(InputAction.CallbackContext context)//Eðilme eventinin tuþu kullanýldýkça çaðrýlacak fonksiyon
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
            // Debug.Log("Yeni sahneyi yükle");
            // OpenDoor(DoorRotation);
            levelManager.LoadNextLevel();
            return;
        }
    }

    private void ApplyMovement()//Karakterin haraket kodlarý
    {
        if (run != true)
            _characterController.Move(_direction * _speed * Time.deltaTime);//Çekilen deðerler üzerinden koþmuyor ise hareket düzenlenmesi
        else
            _characterController.Move(_direction * _speed * Time.deltaTime * _speedMultiplier);//Çekilen deðerler üzerinden koþuyor ise hareket düzenlenmesi
    }
    private void ApplyRotation()//Karakterimizin bakýþ noktasý
    {
        if (_input.sqrMagnitude == 0) return;// herhangi bir input almýyorsak karakter baktýðý yere bakmaya devam edecek
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;// karakterin bakmasýný istediðimi noktanýn açýsýný tutuyor
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);// istediðimiz açýya daha yumuþak bir þekilde bakýyor
        transform.rotation = Quaternion.Euler(0f, angle, 0f);// bu açý uygulaýyor
        //transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, angle, 0f), 1f * Time.deltaTime);
    }
    private void Gravity()//Yerçekimi kodlarý
    {
        if (IsGrounded() && _velocity < 0.0f)// Karakter zeminde ve ona uygulanan yerçekimi 0 dan fazla ise
        {
            _velocity = -1.0f;//yerçekimi aþþaðý yönde 1 olarak uygulanýyor
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime; // karkter zýplama durumunda ise ona uygulanan yerçekimi zamanla artýyor
        }
        _direction.y = _velocity; // yerçekimi karaktere uygulanýyor
    }
    private void OpenDoor(float DoorRotation)
    {
        door.transform.DORotate(new Vector3(0f, DoorRotation, 0f), 1f);
    }

    private bool IsGrounded() => _characterController.isGrounded;//Karakteri yerde olup olmadýðýný kontrol ediyor
}
