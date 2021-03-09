using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private PlayerController playerController;
    public Text txtQtdRecord;
    public Text txtQtdScore;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        int record = AppDAO.getInstance().loadInt(AppDAO.SCORE_TOTAL);
        txtQtdRecord.text = record.ToString().PadLeft(5, '0');
    }

    // Update is called once per frame
    void Update()
    {
        txtQtdScore.text = playerController.altitude.ToString().PadLeft(5, '0');
    }
}
