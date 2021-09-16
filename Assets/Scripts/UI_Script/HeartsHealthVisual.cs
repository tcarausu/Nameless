using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//https://www.youtube.com/watch?v=xWCJfE_sAXE zelda hearts
public class HeartsHealthVisual : MonoBehaviour
{

    public static HeartsHealthSystem heartsHealthSystemStatic;

    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private AnimationClip animationfullHeartClip;


    private List<HeartImage> heartImageList;
    private HeartsHealthSystem heartsHealthSystem;

    public GameObject backToMenu;

    public List<HeartImage> getHeartImageList()
    {
        return heartImageList;
    }

    private bool isHealing;
    public bool isDeadPlayer{ get; set; }

    private void Awake()
    {
        heartImageList = new List<HeartImage>();

        isDeadPlayer = false;
    }
    void Start()
    {
        FunctionPeriodicForUI.Create(healingAnimationPeriodic, 0.05f);
        HeartsHealthSystem healthSystem = new HeartsHealthSystem(5);
        setHeartSystem(healthSystem);
    }

    private void setHeartSystem(HeartsHealthSystem heartsHealthSystem)
    {
        this.heartsHealthSystem = heartsHealthSystem;
        heartsHealthSystemStatic = heartsHealthSystem;

        List<HeartsHealthSystem.Heart> heartImageList = heartsHealthSystem.getHeartList();

        Vector2 heartAnchorPosition = new Vector2(0, 0);

        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartsHealthSystem.Heart heart = heartImageList[i];
            createHeartImage(heartAnchorPosition).setHeartFragments(heart.getFragmentAmount());
            heartAnchorPosition += new Vector2(66, 0);

        }

        heartsHealthSystem.onDamaged += heartsHealthSystem_onDamaged;
        heartsHealthSystem.onHealed += heartsHealthSystem_onHealed;
        heartsHealthSystem.onDead += heartsHealthSystem_onDead;
    }

    private void heartsHealthSystem_onDamaged(object sender, System.EventArgs e)
    {
        //hearts HealthSystem system is damaged
        refreshAllHearts();
    }

    private void heartsHealthSystem_onHealed(object sender, System.EventArgs e)
    {
        //hearts HealthSystem system is healed
        isHealing = true;

        StartCoroutine(isHealingHearts());
    }
    IEnumerator isHealingHearts()
    {
        healingAnimationPeriodic();
        yield return new WaitForSeconds(.5f);
    }

    private void heartsHealthSystem_onDead(object sender, System.EventArgs e)
    {
        //Make animation where player dissapears and poof

        Debug.Log("Dead");

        //Time.timeScale = 0f;

        StartCoroutine(waitForLoadingScreen());
    }


    IEnumerator waitForLoadingScreen()
    {
        yield return new WaitForSeconds(.5f);

        isDeadPlayer = true;

        PauseMenu typeMenu = FindObjectsOfType<PauseMenu>()[0];

        typeMenu.DeadGame();

        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


    private void refreshAllHearts()
    {
        List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.getHeartList();

        for (int i = 0; i < heartImageList.Count; i++)
        {

            HeartImage heartImage = heartImageList[i];

            HeartsHealthSystem.Heart heart = heartList[i];
            heartImage.setHeartFragments(heart.getFragmentAmount());
        }
    }


    private void healingAnimationPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;
            List<HeartsHealthSystem.Heart> heartList = heartsHealthSystem.getHeartList();

            for (int i = 0; i < heartImageList.Count; i++)
            {
                HeartImage heartImage = heartImageList[i];

                HeartsHealthSystem.Heart heart = heartList[i];

                if (heartImage.getFragmentAmount() != heart.getFragmentAmount())
                {

                    heartImage.addHeartVisualFragmet();

                    if (heartImage.getFragmentAmount() == HeartsHealthSystem.MAX_FRAGMENT_AMOUNT)
                    {
                        //this heart was fully healed
                        heartImage.playHeartFullAnimation();
                    }

                    fullyHealed = false;
                    break;
                }
            }

            if (fullyHealed)
            {
                isHealing = false;
            }
        }
    }

    private HeartImage createHeartImage(Vector2 anchoredPosition)
    {

        //create Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));

        //do transform
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        //set positon
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(66, 61);


        heartGameObject.GetComponent<Animation>().AddClip(animationfullHeartClip, "heartFull");

        //set sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = fullHeartSprite;

        //set sprite
        HeartImage heartImage = new HeartImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        heartImageList.Add(heartImage);


        return heartImage;
    }




    //________________________________________Inner Class HeartImage________________________________________________\\


    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartsHealthVisual visual;
        private Animation animation;

        public HeartImage(HeartsHealthVisual visual, Image heartImage, Animation animation)
        {
            this.heartImage = heartImage;
            this.visual = visual;
            this.animation = animation;
        }

        public void setHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: heartImage.sprite = visual.emptyHeartSprite; break;
                case 1: heartImage.sprite = visual.halfHeartSprite; break;
                case 2: heartImage.sprite = visual.fullHeartSprite; break;
            }
        }
        internal int getFragmentAmount()
        {
            return fragments;
        }

        public void addHeartVisualFragmet()
        {
            setHeartFragments(fragments + 1);
        }

        internal void playHeartFullAnimation()
        {
            animation.Play("heartFull", PlayMode.StopAll);
        }
    }

    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    heartsHealthSystem.damage(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    heartsHealthSystem.damage(2);
        //}
        //else 
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            heartsHealthSystem.damage(3);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            heartsHealthSystem.heal(3);
        }
    }
}
