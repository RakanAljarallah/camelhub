using UnityEngine;

public class guideInfo : MonoBehaviour
{
    [SerializeField] private TipWindow mytipWindow;
    [SerializeField] private string message;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void opentipWindow()
    {
        mytipWindow.gameObject.SetActive(true);
        mytipWindow.skipButton.onClick.AddListener(skipClicked);
        mytipWindow.messageText.text = message;
    }

    private void skipClicked()
    {
        mytipWindow.gameObject.SetActive(false);
    }
}
