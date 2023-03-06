using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public int direction = 2; // 0 - up, 1 - right, 2 - front
    public int frame = 0;
    public int frames = 8;

    public float duration = 15;
    private float animeTime = 0;

    private float hspeed;
    public float maxhSpeed = 1;
    private float vspeed;
    public float maxvSpeed = 1;
    public float friction = 0.95f;

    public GameObject door;
    public GameObject key;

    public GameObject npc1Note0;
    public GameObject npc1Note1;
    public GameObject npc2Note0;

    public float noteOffset = 10f;

    private bool hasKey = false;

    public AudioClip DoorSound;
    public AudioClip KeySound;
    public AudioClip NpcSound;

    public Sprite[] UpSprites;
    public Sprite[] RightSprites;
    public Sprite[] FrontSprites;

    private List<Sprite> FrameSprites;


    // Start is called before the first frame update
    void Start()
    {
        npc1Note0.SetActive(false);
        npc1Note1.SetActive(false);
        npc2Note0.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            hspeed = maxhSpeed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<SpriteRenderer>().sprite = RightSprites[frame];
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = 1;
            hspeed = -maxhSpeed;
            transform.rotation = Quaternion.Euler(0, -180, 0);
            GetComponent<SpriteRenderer>().sprite = RightSprites[frame];

        }
        else
        {
            hspeed *= friction;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction = 0;
            vspeed = maxvSpeed;
            GetComponent<SpriteRenderer>().sprite = UpSprites[frame];
        }
        else if (Input.GetKey(KeyCode.S))
        {

            vspeed = -maxvSpeed;
            direction = 2;
            GetComponent<SpriteRenderer>().sprite = FrontSprites[frame];


        }
        else
        {
            vspeed *= friction;
        }

        if (animeTime >= duration * Time.deltaTime)
        {

            if (direction == 0)
            {
                frames = 8;
                GetComponent<SpriteRenderer>().sprite = UpSprites[6];
            }
            else if (direction == 1)
            {
                frames = 3;
                GetComponent<SpriteRenderer>().sprite = RightSprites[2];

            }
            else if (direction == 2)
            {

                frames = 4;
                GetComponent<SpriteRenderer>().sprite = FrontSprites[3];
            }
            frame++;
            if (frame >= frames)
            {
                frame = 0;
            }
            animeTime = 0;
        }
        else
        {
            animeTime += Time.deltaTime;
        }

        Vector3 newPos = transform.position;
        newPos.x += hspeed * Time.deltaTime;
        newPos.y += vspeed * Time.deltaTime;
        transform.position = newPos;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);




    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("key"))
        {
            hasKey = true;
            // play sound
            GetComponent<AudioSource>().clip = KeySound;
            GetComponent<AudioSource>().Play();
            Destroy(key);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (collision.gameObject.name.Equals("NPC1_trigger")
                || collision.gameObject.name.Equals("npc1")
                || collision.gameObject.Equals(npc1Note1) || collision.gameObject.Equals(npc1Note0)
                )
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = NpcSound;
                audio.Play();
                if (!hasKey)
                {
                    npc1Note1.SetActive(false);
                    npc1Note0.SetActive(true);

                    npc1Note0.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);
                }
                else
                {

                    npc1Note0.SetActive(false);
                    npc1Note1.SetActive(true);

                    npc1Note1.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);
                }

            } else if (collision.gameObject.name.Equals("NPC2_trigger") || collision.gameObject.Equals(npc2Note0))
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = NpcSound;
                audio.Play();

                npc2Note0.SetActive(true);

                npc2Note0.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);
                

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.Equals("door") && hasKey)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = DoorSound;
            audio.Play();
            Destroy(door);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Finish"))
        {

            SceneManager.LoadScene("SuccessScene");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(npc2Note0)) {
            npc2Note0.SetActive(false);
        }
        else if (collision.gameObject.Equals(npc1Note1))
        {
            npc1Note1.SetActive(false);
        }
        else if (collision.gameObject.Equals(npc1Note0))
        {
            npc1Note0.SetActive(false);
        }
    }
}
