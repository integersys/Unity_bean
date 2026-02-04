using UnityEngine;
using UnityEngine.UI;

public class TVChannelSwitcher : MonoBehaviour
{
    [Header("UI pogas")]
    public Button onOffButton;
    public Button nextChannelButton; // >>
    public Button prevChannelButton; // <<

    [Header("TV vizuālie slāņi")]
    public GameObject darkness;     // melnums, kad TV OFF
    public GameObject idleScreen;   // static bilde/video

    [Header("1. kanāls (objekti)")]
    public GameObject cheatingWoman;
    public GameObject secretLover;
    public GameObject bed;
    public GameObject mrBeanSad;
    public GameObject channel1Background;

    [Header("2. kanāls (objekti)")]
    public GameObject channel2Background;
    public GameObject tante;
    public GameObject cat;
    public GameObject lacis;

    // Ja vēlāk gribi 3+ kanālus, varēsim atgriezties pie šī
    [Header("Papildu kanāli (pašlaik netiek lietoti)")]
    public GameObject[] extraChannels;

    private bool isTVOn = false;

    private enum TVState { Idle, Channel1, Channel2 }
    private TVState state = TVState.Idle;

    void Start()
    {
        if (onOffButton != null) onOffButton.onClick.AddListener(TogglePower);
        if (nextChannelButton != null) nextChannelButton.onClick.AddListener(NextChannel);
        if (prevChannelButton != null) prevChannelButton.onClick.AddListener(PrevChannel);

        TurnOffTV();
    }

    public void TogglePower()
    {
        if (isTVOn) TurnOffTV();
        else TurnOnTV();
    }

    private void TurnOnTV()
    {
        isTVOn = true;

        if (darkness != null) darkness.SetActive(false);

        // Kad ieslēdz TV — vienmēr static
        ShowIdleOnly();

        SetChannelButtonsInteractable(true);
    }

    private void TurnOffTV()
    {
        isTVOn = false;

        if (darkness != null) darkness.SetActive(true);

        HideAllContent();
        if (idleScreen != null) idleScreen.SetActive(false);

        SetChannelButtonsInteractable(false);
    }

    private void SetChannelButtonsInteractable(bool value)
    {
        if (nextChannelButton != null) nextChannelButton.interactable = value;
        if (prevChannelButton != null) prevChannelButton.interactable = value;
    }

    // -------------------- Navigācija --------------------

    public void NextChannel()
    {
        if (!isTVOn) return;

        // Idle >> -> Channel1
        if (state == TVState.Idle)
        {
            ShowChannel1();
            return;
        }

        // Kanālos: jebkura poga pārslēdz uz otru kanālu
        ToggleBetweenChannel1And2();
    }

    public void PrevChannel()
    {
        if (!isTVOn) return;

        // Idle << -> Channel2
        if (state == TVState.Idle)
        {
            ShowChannel2();
            return;
        }

        // Kanālos: jebkura poga pārslēdz uz otru kanālu
        ToggleBetweenChannel1And2();
    }

    private void ToggleBetweenChannel1And2()
    {
        if (state == TVState.Channel1) ShowChannel2();
        else if (state == TVState.Channel2) ShowChannel1();
        else ShowChannel1(); // drošības fallback
    }

    // -------------------- Rādīšanas metodes --------------------

    private void ShowIdleOnly()
    {
        HideAllContent();
        if (idleScreen != null) idleScreen.SetActive(true);

        state = TVState.Idle;
    }

    private void ShowChannel1()
    {
        HideAllContent();
        if (idleScreen != null) idleScreen.SetActive(false);

        SetActiveSafe(cheatingWoman, true);
        SetActiveSafe(secretLover, true);
        SetActiveSafe(bed, true);
        SetActiveSafe(mrBeanSad, true);
        SetActiveSafe(channel1Background, true);

        state = TVState.Channel1;
    }

    private void ShowChannel2()
    {
        HideAllContent();
        if (idleScreen != null) idleScreen.SetActive(false);

        SetActiveSafe(channel2Background, true);
        SetActiveSafe(tante, true);
        SetActiveSafe(cat, true);
        SetActiveSafe(lacis, true);

        state = TVState.Channel2;
    }

    // -------------------- Palīgfunkcijas --------------------

    private void HideAllContent()
    {
        // Izslēdz 1. kanālu
        SetActiveSafe(cheatingWoman, false);
        SetActiveSafe(secretLover, false);
        SetActiveSafe(bed, false);
        SetActiveSafe(mrBeanSad, false);
        SetActiveSafe(channel1Background, false);

        // Izslēdz 2. kanālu
        SetActiveSafe(channel2Background, false);
        SetActiveSafe(tante, false);
        SetActiveSafe(cat, false);
        SetActiveSafe(lacis, false);

        // Ja gadījumā kaut kas extra ir bijis ieslēgts, drošībai izslēdzam
        if (extraChannels != null)
        {
            for (int i = 0; i < extraChannels.Length; i++)
            {
                if (extraChannels[i] != null) extraChannels[i].SetActive(false);
            }
        }
    }

    private void SetActiveSafe(GameObject obj, bool value)
    {
        if (obj != null) obj.SetActive(value);
    }
}
