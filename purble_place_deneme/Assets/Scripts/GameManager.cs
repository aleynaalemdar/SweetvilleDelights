using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject paper, TradeMill;
    public CakeLayer layer;

    [HideInInspector]
    public Cake sampleCake;

    [HideInInspector]
    public Cake curCake;

    public GameObject OvalcakeMoldPrefab, KarecakeMoldPrefab, HeartcakeMoldPrefab;

    private GameObject activeCakePrefab, secondCakePrefab, menuPrefab;

    public GameObject sampleSaucePrefab, sampleToppingPrefab;

    public Button backButton, nextButton;

    public Button StarButton, HeartButton, FlowerButton;

    public Button OvalButton, KareButton, KalpButton;

    public Button RedSosButton, PSosButton, DarkSosButton;

    public Button YellowSosButton, PurpleSosButton, BlueSosButton;

    public Button ReadyButton, Xbutton, Xbutton1, Xbutton2, addLayerButton;

    public Color selectedColor = Color.white;

    public GameObject currentToppingPrefab, currentSaucePrefab, secondSaucePrefab, secondToppingPrefab;

    public GameObject HeartToppingPrefab, StarToppingPrefab, FlowerToppingPrefab;
  
    public GameObject PurpleSosPrefab, RedSosPrefab, DarkSosPrefab;

    public GameObject StrawberryPrefab, RasPrefab, CherryPrefab, fruitPrefab;
  
    private float transitionDuration = 1.0f;

    private Vector3 initialPaperPosition;
    public GameObject WelcomePage;
    public GameObject LostPage;

    public float scrollSpeed = 1f;
    private Renderer rend;
    private Material material;


    public TMP_Text CheckText, FaiulureText, ScoreText;
    public int currState=0;

    private List<GameObject> instantiatedCakes = new List<GameObject>();

    public GameObject lacePrefab,Lace_white,Lace_red,Lace_Purple;


    private void Awake()
    {
        sampleCake = new Cake();
        sampleCake.InitializeCake();

        InstantiateSampleCakeLayers(sampleCake);

        curCake = new Cake();

    }

    private void Start()
    {
        initialPaperPosition = paper.transform.localPosition;

        paper.SetActive(true);

        StartCoroutine(MovePaperAndCake(initialPaperPosition, paper.transform.localPosition + Vector3.right * 3.5f, paper));
        startCake();
        statehandling();

    }
    public void StartPage()
    {
        WelcomePage.SetActive(false);
    }
    private void statehandling()
    {
        if (currState == 0 || currState==5 )
        {
            OvalButton.interactable = false;
            KareButton.interactable = false;
            KalpButton.interactable = false;

            YellowSosButton.interactable = false;
            PurpleSosButton.interactable = false;
            BlueSosButton.interactable = false;

            PSosButton.interactable = false;
            RedSosButton.interactable = false;
            DarkSosButton.interactable = false;

            HeartButton.interactable = false;
            StarButton.interactable = false;
            FlowerButton.interactable = false;

            addLayerButton.gameObject.SetActive(false);

        }
        if (currState == 1)
        {
            OvalButton.interactable = true;
            KareButton.interactable = true;
            KalpButton.interactable = true;

            addLayerButton.gameObject.SetActive(false);


        }
        if (currState == 2)
        {
            YellowSosButton.interactable = true;
            PurpleSosButton.interactable = true;
            BlueSosButton.interactable = true;

            addLayerButton.gameObject.SetActive(false);

        }
        if (currState == 3)
        {
            PSosButton.interactable = true;
            RedSosButton.interactable = true;
            DarkSosButton.interactable = true;

            addLayerButton.gameObject.SetActive(false);

        }
        if (currState == 4)
        {
            HeartButton.interactable = true;
            StarButton.interactable = true;
            FlowerButton.interactable = true;

            addLayerButton.gameObject.SetActive(true);

        }
    }

    private void waitForstate()
    {
        StartCoroutine(waitedState());
    }

    private IEnumerator waitedState()
    {
        yield return new WaitForSeconds(2f);
    }

    GameObject cakePrefab;
    GameObject instantiatedCake;

    float placementFactor = 0.4f;
    GameObject samplelacePrefab;

    private void InstantiateSampleCakeLayers(Cake cake)
    {
        float yOffset = 0.0f;
        float baseScale = 0.8f; // Initial scale value
        float scaleFactorMultiplier = 0.7f;

        samplelacePrefab = Instantiate(GetCakeLace(),new Vector3(-9.62f, 5.96f, 3.76f), Quaternion.identity);

        samplelacePrefab.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);
        samplelacePrefab.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));


        foreach (CakeLayer layer in cake.cakeLayers)
        {
            GameObject cakePrefab = OvalcakeMoldPrefab;
            GameObject instantiatedCake = Instantiate(cakePrefab, new Vector3(-9.6f, 6.53f + yOffset, 3.77f), Quaternion.identity);

            instantiatedCakes.Add(instantiatedCake);

            ApplyLayerProperties(layer, instantiatedCake);

            instantiatedCake.transform.position += yOffset * Vector3.up;

            instantiatedCake.transform.localScale = new Vector3(baseScale, baseScale, baseScale);

            yOffset += placementFactor;

            baseScale *= scaleFactorMultiplier;

            ApplySauceandTopping(layer.cakeSauce, layer.cakeTop, instantiatedCake);
        }
    }

    GameObject randomCakeLace;

    private GameObject GetCakeLace()
    {
        int sampleCakeLace = Random.Range(0, 3);
        switch (sampleCakeLace)
        {
            case 0:
                randomCakeLace = Lace_white;
                sampleCake.sampleCakeLace=0;
                break;
            case 1:
                randomCakeLace = Lace_red;
                sampleCake.sampleCakeLace =1;
                break;
            case 2:
                randomCakeLace = Lace_Purple;
                sampleCake.sampleCakeLace =2;
                break;

        }
        return randomCakeLace;
    }


    private GameObject GetSaucePrefab(CakeLayer.CakeSauce sauce)
    {
        switch (sauce)
        {
            case CakeLayer.CakeSauce.Purple:
                return PurpleSosPrefab;
            case CakeLayer.CakeSauce.Red:
                return RedSosPrefab;
            case CakeLayer.CakeSauce.Dark:
                return DarkSosPrefab;
            default:
                return null;
        }
    }


    private GameObject GetToppingPrefab(CakeLayer.CakeTop topping)
    {
        switch (topping)
        {
            case CakeLayer.CakeTop.Heart:
                return HeartToppingPrefab;
            case CakeLayer.CakeTop.Star:
                return StarToppingPrefab;
            case CakeLayer.CakeTop.Flower:
                return FlowerToppingPrefab;
            default:
                return null;
        }
    }


    private void ApplyLayerProperties(CakeLayer layer, GameObject instantiatedCake)
    {
        Renderer cakeRenderer = instantiatedCake.GetComponent<Renderer>();
        if (cakeRenderer != null)
        {
            cakeRenderer.material.color = GetColorFromCakeColor(layer.cakeColor);
        }

         instantiatedCake.transform.localScale = layer.ScaleFactor;

        instantiatedCake.transform.rotation = Quaternion.Euler(layer.RotationAngles);

    }


    private void ApplySauceandTopping(CakeLayer.CakeSauce sauce, CakeLayer.CakeTop top, GameObject instantiatedCake)
    {
        GameObject saucePrefab = GetSaucePrefab(sauce);

        if (saucePrefab != null)
        {
            sampleSaucePrefab = Instantiate(saucePrefab, instantiatedCake.transform);
            sampleSaucePrefab.transform.localPosition = new Vector3(-0.04f, 0.34f, -0.03f);
            sampleSaucePrefab.transform.localScale = new Vector3(0.93f, 0.85f, 0.93f);
        }

        
        GameObject toppingPrefab = GetToppingPrefab(top);
        if (toppingPrefab != null)
        {
            sampleToppingPrefab = Instantiate(toppingPrefab, instantiatedCake.transform);
            sampleToppingPrefab.transform.localPosition = new Vector3(0.26f, 0.52f, -2.66f);
            sampleToppingPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }
    }

    private IEnumerator MovePaperAndCake(Vector3 initialPos, Vector3 finalPos, GameObject gameObject)
    {
        float elapsedTime = 0f;
        float initialOffsetX = Time.time;
        Material treadmillMaterial = TradeMill.GetComponent<Renderer>().material; 

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            Vector3 newPos = Vector3.Lerp(initialPos, finalPos, t);
            gameObject.transform.localPosition = newPos;

          
            float offsetX = initialOffsetX + t * scrollSpeed;
            treadmillMaterial.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.localPosition = finalPos;

        treadmillMaterial.SetTextureOffset("_MainTex", new Vector2(initialOffsetX + 1, 0));
        waitForstate();
        currState++;
        statehandling();
    }

    public void startCake()
    {
        StartCoroutine(DelayedCake());
    
    }

    private IEnumerator DelayedCake()
    {
        yield return new WaitForSeconds(1.3f);
        activeCakePrefab = Instantiate(OvalcakeMoldPrefab, new Vector3(0, 0, 0), Quaternion.identity, paper.transform);
        activeCakePrefab.transform.localPosition = new Vector3(-0.18f, 0.59f, -1.69f);

    }

    public void SwitchToCake(int sayi)
    {

        StartCoroutine(DelayedSwitchToCake(sayi));
    }

    public GameObject lacePrefab2;
    

    private IEnumerator DelayedSwitchToCake(int sayi)
    {
        curCake.ChangeShape();
        switch (sayi)
            {
                case 0:
                lacePrefab2 = Instantiate(Lace_white, new Vector3(0, 0, 0), Quaternion.identity, paper.transform);
               curCake.currCakeLace = 0;
                    break;
                case 1:
                lacePrefab2 = Instantiate(Lace_red, new Vector3(0, 0, 0), Quaternion.identity, paper.transform);
                curCake.currCakeLace = 1;
                break;
                case 2:
                lacePrefab2 = Instantiate(Lace_Purple, new Vector3(0, 0, 0), Quaternion.identity, paper.transform);
                curCake.currCakeLace = 2;
                break;
                default:
                    break;

            }

            OvalButton.interactable=false;
            KareButton.interactable=false;
            KalpButton.interactable=false;
            lacePrefab2.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            lacePrefab2.transform.localPosition = new Vector3(-0.22f, 0.14f, -0.43f);


            yield return new WaitForSeconds(1f);

            StartCoroutine(MovePaperAndCake(paper.transform.localPosition, paper.transform.localPosition + Vector3.right * 2.6f, paper));
 
    }

    public void SelectColor(int sayi)
    {
        StartCoroutine(DelayedSelectColor(sayi));
    }

    private IEnumerator DelayedSelectColor(int sayi)
    {
        curCake.ChangeColor(sayi);

        selectedColor = GetColorFromCakeColor((CakeLayer.CakeColor)sayi);

        if (secondCakePrefab != null)
        {
            ApplySelectedColorToPrefab(secondCakePrefab);

            YellowSosButton.interactable = false;
            PurpleSosButton.interactable = false;
            BlueSosButton.interactable = false;
        }
        else if (activeCakePrefab != null)
        {
            ApplySelectedColorToPrefab(activeCakePrefab);

            YellowSosButton.interactable = false;
            PurpleSosButton.interactable = false;
            BlueSosButton.interactable = false;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(MovePaperAndCake(paper.transform.localPosition, paper.transform.localPosition + Vector3.right * 2.85f, paper));
    }

    private Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    private Color GetColorFromCakeColor(CakeLayer.CakeColor cakeColor)
    {
        switch (cakeColor)
        {
            case CakeLayer.CakeColor.Yellow:
                return HexToColor("#FD9A9C");
            case CakeLayer.CakeColor.Magenta:
                return HexToColor("#E198FA");
            case CakeLayer.CakeColor.Blue:
                return HexToColor("#6CB9C0");
            default:
                return Color.white;
        }
    }


    private void ApplySelectedColorToPrefab(GameObject prefab)
    {
        Renderer cakeRenderer = prefab.GetComponent<Renderer>();
        if (cakeRenderer != null)
        {
            cakeRenderer.material.color = selectedColor;
        }
    }


    public void SelectSauce(int sayi)
    {
        StartCoroutine(DelayedSelectSauce(sayi));
    }

    private IEnumerator DelayedSelectSauce(int sayi)
    {
        curCake.ChangeSauce(sayi);
        if (activeCakePrefab != null && secondCakePrefab == null)
        {
            switch (sayi)
            {
                case 0:
                    currentSaucePrefab = Instantiate(PurpleSosPrefab, activeCakePrefab.transform);
                    break;
                case 1:
                    currentSaucePrefab = Instantiate(RedSosPrefab, activeCakePrefab.transform);
                    break;
                case 2:
                    currentSaucePrefab = Instantiate(DarkSosPrefab, activeCakePrefab.transform);
                    break;
                default:
                    break;
            }
                currentSaucePrefab.transform.localPosition = new Vector3(-0.1f, 0.36f, 0.51f);
       
                PSosButton.interactable = false;
                RedSosButton.interactable = false;
                DarkSosButton.interactable = false;
        }

        if (secondCakePrefab != null)
        {

            switch (sayi)
            {
                case 0:
                    secondSaucePrefab = Instantiate(PurpleSosPrefab, activeCakePrefab.transform);
                    break;
                case 1:
                    secondSaucePrefab = Instantiate(RedSosPrefab, activeCakePrefab.transform);
                    break;
                case 2:
                    secondSaucePrefab = Instantiate(DarkSosPrefab, activeCakePrefab.transform);
                    break;
                default:
                    break;
            }
            secondSaucePrefab.transform.localPosition = new Vector3(-0.12f, 2.22f, 0.07f);
            secondSaucePrefab.transform.localScale = new Vector3(0.63f, 0.7f, 0.63f);

            PSosButton.interactable = false;
            RedSosButton.interactable = false;
            DarkSosButton.interactable = false;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(MovePaperAndCake(paper.transform.localPosition, paper.transform.localPosition + Vector3.right * 2.4f, paper));
    }

    public void SelectTopping(int sayi)
    {
        curCake.ChangeTopping(sayi);

        if (activeCakePrefab != null && secondCakePrefab == null)
        {
        switch (sayi)
        {
            case 0:
                currentToppingPrefab = Instantiate(HeartToppingPrefab, activeCakePrefab.transform);
                    currentToppingPrefab.transform.localPosition = new Vector3(-0.25f, 0.41f, -1.23f);
                    break;
            case 1:
                currentToppingPrefab = Instantiate(StarToppingPrefab, activeCakePrefab.transform);
                    currentToppingPrefab.transform.localPosition = new Vector3(-0.73f, 1.73f, -3.69f);
                    break;
            case 2:
                currentToppingPrefab = Instantiate(FlowerToppingPrefab, activeCakePrefab.transform);
                    currentToppingPrefab.transform.localPosition = new Vector3(-0.73f, 1.79f, -3.69f);
                    break;
            default:
                break;
                
        }
            HeartButton.interactable = false;
            StarButton.interactable = false;
            FlowerButton.interactable = false;
        }

        if (secondCakePrefab != null)
        {

            switch (sayi)
            {
                case 0:
                    secondToppingPrefab = Instantiate(HeartToppingPrefab, activeCakePrefab.transform);
                    break;
                case 1:
                    secondToppingPrefab = Instantiate(StarToppingPrefab, activeCakePrefab.transform);
                    break;
                case 2:
                    secondToppingPrefab = Instantiate(FlowerToppingPrefab, activeCakePrefab.transform);
                    break;
                default:
                    break;
            }

            secondToppingPrefab.transform.localPosition = new Vector3(-0.19f, 2.03f, -0.77f);

            HeartButton.interactable = false;
            StarButton.interactable = false;
            FlowerButton.interactable = false;
        }

    }

    public void finishLine()
    {
        StartCoroutine(MovePaperAndCake(paper.transform.localPosition, paper.transform.localPosition + Vector3.right * 6.7f, paper));
        StartCoroutine(CheckCakeMatch());
    }

    private void DestroyInstantiatedCakes()
    {
        foreach (GameObject cake in instantiatedCakes)
        {
            Destroy(cake);
        }
        instantiatedCakes.Clear(); 
    }


    public void ResetGame()
    {
        Destroy(activeCakePrefab);
        Destroy(currentSaucePrefab);
        Destroy(currentToppingPrefab);
        Destroy(lacePrefab2);

        paper.SetActive(false);
        paper.transform.localPosition = new Vector3(-8.8f, 4.48f, -0.89f);
        paper.SetActive(true);
        StartCoroutine(MovePaperAndCake(initialPaperPosition, paper.transform.localPosition + Vector3.right *3.6f, paper));
  
        Destroy(sampleSaucePrefab);
        Destroy(sampleToppingPrefab);
        DestroyInstantiatedCakes();
        Destroy(secondCakePrefab);
        Destroy(secondSaucePrefab);
        Destroy(secondSaucePrefab);
        Destroy(samplelacePrefab);
        curCake.currCakeLace = -1;
        sampleCake.sampleCakeLace = -1;



        StartCoroutine(waitforNewCake());

    }

    public IEnumerator waitforNewCake()
    {
        yield return new WaitForSeconds(1f);
        sampleCake = new Cake();
        sampleCake.InitializeCake();
        InstantiateSampleCakeLayers(sampleCake);
        curCake = new Cake();
        startCake();

    }

    int scoreCount = 0;
    int loseCount = 0;

    public void addLayertoCurrentCake()
    {
        currState = 1;
        curCake.currentCakeLayer++;
        paper.transform.localPosition = new Vector3(-8.8f, 4.48f, -0.89f);
        StartCoroutine(MovePaperAndCake(paper.transform.localPosition, paper.transform.localPosition + Vector3.right * 6.1f, paper));
        StartCoroutine(waitforSecondCake());
       
    }

    public IEnumerator waitforSecondCake()
    {
        yield return new WaitForSeconds(1f);
        secondCakePrefab = Instantiate(OvalcakeMoldPrefab, new Vector3(0, 0, 0), Quaternion.identity, paper.transform);
        secondCakePrefab.transform.localPosition = new Vector3(0, 1.26f, 0);
        secondCakePrefab.transform.localScale= new Vector3(2.2f,0.54f,2.2f);
        curCake.ChangeShape();
    }

    public IEnumerator CheckCakeMatch()
    {
        bool isMatch = true;
        yield return new WaitForSeconds(1f);
        if (curCake.cakeLayers.Count != sampleCake.cakeLayers.Count)
        {
           
            isMatch = false;
            FaiulureText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            FaiulureText.gameObject.SetActive(false);
            Debug.LogError("aa");
            isLose();

        }
        else
        {
            for (int i = 0; i < curCake.cakeLayers.Count; i++)
            {
                if (!CakeLayerMatches(curCake.cakeLayers[i], sampleCake.cakeLayers[i]))
                {
                    isMatch = false;
                    FaiulureText.gameObject.SetActive(true);
                    isLose();
                    yield return new WaitForSeconds(1f);
                    FaiulureText.gameObject.SetActive(false);
                    Debug.LogError("bb");

                    break; 
                }
            }
        }
        yield return new WaitForSeconds(1f);

        if (isMatch)
        {
            CheckText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            CheckText.gameObject.SetActive(false);
            scoreCount++;
            ScoreText.text = scoreCount.ToString();
            Debug.LogError("cc");

        }
        yield return new WaitForSeconds(1f);
        currState = 0;
        ResetGame();
    }

    public void isLose()
    {
        loseCount++;
        if (loseCount == 1)
        {
            Xbutton.gameObject.SetActive(true);
        }
        if (loseCount == 2)
        {
            Xbutton1.gameObject.SetActive(true);
        }
        if (loseCount == 3)
        {
            Xbutton2.gameObject.SetActive(true);
            LostPage.SetActive(true);
        }
    }

    private bool CakeLayerMatches(CakeLayer layer1, CakeLayer layer2)
    {
        bool matching;
        matching = (layer1.cakeShape == layer2.cakeShape ) && (layer1.cakeColor == layer2.cakeColor) && (layer1.cakeSauce == layer2.cakeSauce) && (layer1.cakeTop == layer2.cakeTop) && (curCake.currCakeLace== sampleCake.sampleCakeLace);
        return matching;

      
    }

    public void TryAgain()
    {
        Xbutton.gameObject.SetActive(false);
        Xbutton1.gameObject.SetActive(false);
        Xbutton2.gameObject.SetActive(false);
        LostPage.SetActive(false);
        scoreCount = 0;
        ScoreText.text = scoreCount.ToString();
        loseCount = 0;

    }
}

