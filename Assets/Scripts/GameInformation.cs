using UnityEngine;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{
    public Fighter player1;
    public Fighter player2;
    public Text player1HealthText;
    public Text player2HealthText;
    public Text player1NameText;
    public Text player2NameText;

    // Start is called before the first frame update
    void Start()
    {
        player1NameText.text = player1.FighterData.name;
        player2NameText.text = player2.FighterData.name;
    }

    // Update is called once per frame
    void Update()
    {
        player1HealthText.text = player1.health.ToString();
        player2HealthText.text = player2.health.ToString();
    }
}
