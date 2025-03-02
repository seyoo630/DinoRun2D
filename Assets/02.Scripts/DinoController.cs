using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoController : MonoBehaviour
{
    [Header("Settings")]
    public float jumpPower = 2f;
    public bool isDown = false;
    public bool isGround = true;
    public bool isShootable = true;

    [Header("References")]
    public GameObject[] GunPrefabs;
    public Button SpawnButton;
    public Transform ArrowRangePoint;
    public Transform groundCheckPoint;
    public Transform SpawnCheckPoint;
    public LayerMask whatIsGround; //ground인지 비교할 Mask
    public LayerMask whatIsEnemy;
    public AudioSource ScoreSound;

    [SerializeField]
    [Tooltip("boxCollider offset과 size를 변경")]
    private Vector2 saveOffset;// 서있을때 offset
    private Vector2 saveSize;// 서있을때 BoxCollider의 Size
    
    private Animator Anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private List<Vector2> spawnPositions;
    private AudioSource Aud;






    void Start()
    {
        
        boxCollider = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Aud = GetComponent<AudioSource>();

        SaveColliderSettings();
        InitializeSpawnPositions();


        Anim.SetBool("isGround", true);

        SpawnButton.onClick.AddListener(SpawnGunPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOverPanel.activeSelf)
        {
            isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
            isShootable = Physics2D.OverlapCircle(ArrowRangePoint.position, 5f, whatIsEnemy);

            if (Input.GetKeyDown(KeyCode.Space) && isGround.Equals(true))
            {
                if (!isDown)
                {
                    //rb.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    Aud.Play();
                }
                    
                //Anim.SetBool("isGround", false);
            }

            Anim.SetBool("isGround", isGround);

            if (Input.GetKeyDown(KeyCode.DownArrow) && isGround.Equals(true))
            {
                SetDownArrowDown();

            }

            if (Input.GetKeyUp(KeyCode.DownArrow) && isGround.Equals(true))
            {
                SetDownArrowUp();

            }
        }
        

    }

    void SaveColliderSettings()
    {
        saveOffset = boxCollider.offset;
        saveSize = boxCollider.size;
    }

    void LoadColliderSettings()
    {
        boxCollider.offset = saveOffset;
        boxCollider.size = saveSize;
    }

    void SetDownArrowDown()
    {
        Anim.SetBool("isDown", true);

        boxCollider.offset = new Vector2(0f, -0.2f);
        boxCollider.size = new Vector2(1.76f, 0.86f);
        isDown = true;
    }

    void SetDownArrowUp()
    {
        Anim.SetBool("isDown", false);
        LoadColliderSettings();
        isDown = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Anim.SetBool("isGround", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ArrowRangePoint.position, 7f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(SpawnCheckPoint.position, 2f);
    }

    void InitializeSpawnPositions()
    {
        spawnPositions = new List<Vector2>();
        int pointCount = 8;
        float radius = 2f;
        Vector2 center = SpawnCheckPoint.position;

        for (int i = 0; i < pointCount; i++)
        {
            float angle = i * (360f / pointCount) * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);
            spawnPositions.Add(new Vector2(x, y));
        }
    }

    
    void SpawnGunPrefab()
    {
        if (spawnPositions.Count == 0)
        {
            Debug.Log("더 이상 사용할 스폰 좌표가 없습니다.");
            return;
        }

        
        Vector2 spawnPosition = spawnPositions[0];
        spawnPositions.RemoveAt(0);

        int randomIndex = Random.Range(0, GunPrefabs.Length);
        Instantiate(GunPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("게임 오버");
            Anim.SetBool("isDie", true);
            GameManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Point"))
        {
            Debug.Log("점수 휙득");
            GameManager.instance.mainScore += 10;
            GameManager.instance.updateScore();
            ScoreSound.Play();
        }

    }
}
