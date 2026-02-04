using UnityEngine;
using UnityEngine.UI;

public class TVChannelSwitcher : MonoBehaviour
{
    [Header("UI pogas")]
    public Button onOffButton;
    public Button nextChannelButton;
    public Button prevChannelButton;

    [Header("TV vizu?lie sl??i")]
    public GameObject darkness;     // melnums, kad TV OFF
    public GameObject idleScreen;   // static bilde/video

    [Header("Kan?li (katrs k? atseviš?a grupa/GameObject)")]
    public GameObject[] channels;   // piem: Channel1Group, Channel2Group...

    private bool isTVOn = false;
    private int currentChannelIndex = 0;

    void Start()
    {
        // Piesaist?m pogas (ja nav piesietas Inspector)
        if (onOffButton != null) onOffButton.onClick.AddListener(TogglePower);
        if (nextChannelButton != null) nextChannelButton.onClick.AddListener(NextChannel);
        if (prevChannelButton != null) prevChannelButton.onClick.AddListener(PrevChannel);

        // S?kum? TV ir OFF
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

        // Reset: kad iesl?dz TV, vienm?r r?da static
        ShowIdleOnly();

        SetChannelButtonsInteractable(true);
    }

    private void TurnOffTV()
    {
        isTVOn = false;

        // TV OFF: melnums redzams
        if (darkness != null) darkness.SetActive(true);

        // Pasl?pj idle + visus kan?lus
        HideAllChannels();
        if (idleScreen != null) idleScreen.SetActive(false);

        SetChannelButtonsInteractable(false);
    }

    private void SetChannelButtonsInteractable(bool value)
    {
        if (nextChannelButton != null) nextChannelButton.interactable = value;
        if (prevChannelButton != null) prevChannelButton.interactable = value;
    }

    private void HideAllChannels()
    {
        if (channels == null) return;
        for (int i = 0; i < channels.Length; i++)
        {
            if (channels[i] != null) channels[i].SetActive(false);
        }
    }

    private void ShowIdleOnly()
    {
        HideAllChannels();
        if (idleScreen != null) idleScreen.SetActive(true);

        // p?c iesl?gšanas m?s “neatrodamies kan?l?”
        currentChannelIndex = 0;
    }

    public void NextChannel()
    {
        if (!isTVOn) return;
        if (channels == null || channels.Length == 0) return;

        // ja pašlaik r?da Idle, tad ejam uz 1. kan?lu (index 0)
        if (idleScreen != null && idleScreen.activeSelf)
        {
            ShowChannel(0);
            return;
        }

        int next = GetActiveChannelIndex();
        next = (next + 1) % channels.Length;
        ShowChannel(next);
    }

    public void PrevChannel()
    {
        if (!isTVOn) return;
        if (channels == null || channels.Length == 0) return;

        // ja pašlaik r?da Idle, tad ejam uz p?d?jo kan?lu
        if (idleScreen != null && idleScreen.activeSelf)
        {
            ShowChannel(channels.Length - 1);
            return;
        }

        int prev = GetActiveChannelIndex();
        prev = (prev - 1 + channels.Length) % channels.Length;
        ShowChannel(prev);
    }

    private int GetActiveChannelIndex()
    {
        // ja kaut kas nav akt?vs k?rt?gi, fallback uz currentChannelIndex
        for (int i = 0; i < channels.Length; i++)
        {
            if (channels[i] != null && channels[i].activeSelf)
                return i;
        }
        return currentChannelIndex;
    }

    private void ShowChannel(int index)
    {
        index = Mathf.Clamp(index, 0, channels.Length - 1);
        currentChannelIndex = index;

        if (idleScreen != null) idleScreen.SetActive(false);
        HideAllChannels();

        if (channels[index] != null) channels[index].SetActive(true);
    }
}
