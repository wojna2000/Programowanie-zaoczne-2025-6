using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // int - liczba ca³kowita (1, 214324, -32434)
    // float - liczba z przecinkiem 213.4545f, 23.0f
    // string - tekst "tekst" 
    // bool - zmienna logiczna true/false

    public event Action InitializeEvent;

    [SerializeField] private float speed = 2;
    [SerializeField] private float lookSpeed = 100;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float interactionRange = 10;
    [SerializeField]
    private bool isAlive;
    //ctrl + r + r - replace wszêdzie
    [SerializeField] private bool isOnTheRight;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private ParticleSystem shootParticles;
    [SerializeField] private GunStats gunStats;
    private float timer = 1;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Rigidbody rb;
    [SerializeField] public BattleStatsConfig statsConfig;
    public Health health;
    public static Player Instance;
    private bool initialized;

    public void Attack(Player target)
    {
        AttackData attackData = new AttackData(statsConfig.Attack);
        AttackData attackData2 = attackData;

        attackData2.Damage = 20;
        Vector3 direction = new Vector3(1, 0, 0);
        int armor = target.statsConfig.Armor;
        int Damage = DamgeCalculator.CalculateDamage(attackData, new DefenceData(armor, null));
        target.health.TakeDamge(Damage);

    }

    [ContextMenu("Deal 1 damage")]
    public void TestDamage()
    {
        health.TakeDamge(1);
    }


    public int CurrentHealth
    {
        get
        {
            return health.CurrentHealth;
        }
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = new Health(statsConfig.Health);
        Debug.Log(CurrentHealth);
        rb = GetComponent<Rigidbody>();
        isAlive = !isAlive;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialized = true;
        InitializeEvent?.Invoke();
    }

    public void CallOrSubscribe(Action onInitialzed)
    {
        if(initialized)
        {
            onInitialzed();
        }
        else
        {
            InitializeEvent += onInitialzed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = moveInput * speed * Time.deltaTime;

        transform.Rotate(new Vector3(0, lookInput.x * lookSpeed * Time.deltaTime, 0));
        cameraTransform.transform.Rotate(new Vector3(-lookInput.y * lookSpeed * Time.deltaTime, 0, 0));
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = moveInput * speed;
        rb.linearVelocity = transform.forward * moveVector.y + transform.right * moveVector.x;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void OnAttack()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        shootParticles.Emit(1);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
            else
            {
                GameObject spawnedParticle = Instantiate(hitParticles, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(spawnedParticle, 2);
            }
            Debug.Log(hit.transform.gameObject.name);
        }
    }

    private void OnInteract()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            print("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void DoSomething()
    {

    }
}


