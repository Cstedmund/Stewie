using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CnControls;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Private Members
    private Animator animator;
    private float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private UIBar remetalBar;
    private UIBar paycheBar;
    private CharacterController characterController;
    private int startSpirit;
    private int Timer;
    private float dTimer;
    private int sindex=0;
    private string[] tips = {"Tips: Psyche level will drop with time, \nrefilling under lamps can ensure survival", "Tips: Larger devils will absorb psyche \nfrom Stewie, keep away from them!", "Tips: Collect all the rememtal \nto unlock Stewie's memory" , "Tips: Go to the park beside the lake, \nkeep walking up when arriving the park.", "Tips: Devils will chase after you to \nstop you from escaping the animal underworld", "Tips: Remember to refill your psyche, \nLarge devils will absorb your psyche." };
    #endregion

    #region Public Member
    public float Speed = 7.0f;
    public float RotationSpeed = 40f;
    public GameObject Hand;
    public HUD Hud;
    public float JumpSpeed = 7.0f;
    public levelChanger levelchange;
    public GameObject dog;
    public GameObject Dad;
    public GameObject cam1;
    public Text Story;
    public Text Tips;
    public GameObject FinalSceneMusic;
    public bool played = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        dTimer = 0;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        remetalBar = Hud.transform.Find("Bars_Panel/remetalUI").GetComponent<UIBar>();
        remetalBar.Min = 0;
        remetalBar.Max = 100;
        remetalBar.SetValue(Crystal);


        paycheBar = Hud.transform.Find("Bars_Panel/psycheUI").GetComponent<UIBar>();
        paycheBar.Min = 0;
        paycheBar.Max = Spirit;
        startSpirit = Spirit;
        paycheBar.SetValue(Spirit);

        if (dog.activeSelf)
        { Dad.SetActive(true); }
        

        levelchange = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<levelChanger>();
    }
   

    #region Inventor

    private void SetItemActive(crystals item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }

    #endregion

    #region Spirit & Crystal

    [Tooltip("Amount of Crystal")]
    public int Crystal = 5;

    [Tooltip("Amount of Spirit")]
    public int Spirit = 100;

    [Tooltip("Rate in seconds in which the hunger increases")]
    public float decreaseSpiritRate = 0.4f;

    public void DecreaseSpirit()
    {
        Spirit--;
        if (Spirit <= 20 && Spirit > 1 && !IsDead)
        {
            levelchange.blurScreen(true);
        }
        else if(Spirit>20)
        {
            levelchange.blurScreen(false);
        }
        if (Spirit < 0)
        {
            Spirit = 0;
        }
        paycheBar.SetValue(Spirit);
        if (IsDead)
        {
            Debug.Log("playerdead");
            CancelInvoke();
            animator.SetTrigger("death");
            levelchange.FadeToLevel(0);
        }
    }

    public bool IsDead
    {
        get
        {
            return Spirit == 0;
        }
    }

    public void CollectCrystal(int amount)
    {
        amount = 5;
        if (this.gameObject.name == "puppeeeeth")
        {
            amount = 100;
        }
        Crystal += amount;
        if (Crystal > 100)
        {
            Crystal = 100;
        }
        remetalBar.SetValue(Crystal);
    }
    public void Boostsprite(int amount)
    {
        Spirit += amount;
        if (Spirit >= 100)
            Spirit = 100;
        paycheBar.SetValue(Spirit);

    }
    #endregion


    void FixedUpdate()
    {
        if (!IsDead)
        {
        }
    }

    private bool isControlEnabled = true;
	
    public void EnableControl()
    {
        isControlEnabled = true;
    }
    public void DisableControl()
    {
        isControlEnabled = false;
    }
    void Clear()
    {
        Tips.text = tips[sindex];
        Timer += (int)(Time.deltaTime * 10) + 1;
        if (Timer > 100) { sindex++; Timer = 0; }
        
    }

    // Update is called once per frame
    void Update()
    {  
        dTimer += Time.deltaTime * 0.5f;
         Debug.Log(decreaseSpiritRate);
        if (dTimer>= decreaseSpiritRate)
        { DecreaseSpirit();
            dTimer = 0;
        }
     
        if (this.gameObject.name == "darkling_ball"&& !played)
        {            
            if (sindex >2) { sindex=0;  }
           
        }
        else if (this.gameObject.name == "darkling_ball"&&played)
        {
            if (sindex < 3 || sindex >3) { sindex = 3; }
             }
        else
        {

            if (sindex <4 ||sindex > 5) { sindex = 4; }
            
        }
        Clear();
        if (remetalBar.TxtHealth.text == "100 %")
        {
            if (!played)
            {
                FinalSceneMusic.SetActive(true);

                dog.SetActive(true);
                DisableControl();
                EnableControl();
                Story.text = "so.. I died and turned into a devil..\nbut I still haven't found him.";
                Tips.text = "Tips: Go to the park beside the lake after collect all crystals";
                played = true;
            }
            

        }
        if (!IsDead && isControlEnabled)
        {
            if (mInteractItem != null && Input.GetKeyDown(KeyCode.F))
            {
                mInteractItem.OnInteractAnimation(animator);
            }

            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            h = CnInputManager.GetAxis("Horizontal");
            v = CnInputManager.GetAxis("Vertical");

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

            if (characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                if (Input.GetButton("Jump")|| CnInputManager.GetButton("Jump"))
                {
                    animator.SetBool("is_in_air", true);
                    _moveDirection.y = JumpSpeed;

                }
                else
                {
                    animator.SetBool("is_in_air", false);
                    animator.SetBool("run", move.magnitude > 0);
                }
            }

            _moveDirection.y -= Gravity * Time.deltaTime;

            characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

    private crystals mInteractItem = null;

    private void OnTriggerEnter(Collider other)
    {
        crystals item = other.GetComponent<crystals>();

        if (item != null)
        {
            if (item.CanInteract(other))
            {

                mInteractItem = item;

                Hud.OpenMessagePanel(mInteractItem);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        crystals item = other.GetComponent<crystals>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mInteractItem = null;
        }
    }
}
