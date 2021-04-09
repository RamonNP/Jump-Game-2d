using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppRating : MonoBehaviour
{
     private const string AndroidRatingURI = "http://play.google.com/store/apps/details?id={0}";
    private const string iOSRatingURI     = "itms://itunes.apple.com/us/app/apple-store/{0}?mt=8";
 
    [Tooltip("iOS App ID (number), example: 1122334455")]
    public string iOSAppID="";
 
    private string url;

    public GameObject rateMe;
 
    // Initialization
    void Start () {
        #if UNITY_IOS
        if (!string.IsNullOrEmpty (iOSAppID)) {
            url = iOSRatingURI.Replace("{0}",iOSAppID);
        }
        else {
            Debug.LogWarning ("Please set iOSAppID variable");
        }
 
        #elif UNITY_ANDROID
        url = AndroidRatingURI.Replace("{0}",Application.identifier);
        #endif
        AbrirRate();
    }
    private void AbrirRate() {
        int qtd  = AppDAO.getInstance().loadInt(AppDAO.RATE);
        if(qtd >= 7){
            qtd = 0;
            rateMe.SetActive(true);
        } else {
            qtd++;
        }
        print(qtd);
        AppDAO.getInstance().saveInt(AppDAO.RATE, qtd);
    }
 
    /// <summary>
    /// Open rating url
    /// </summary>
    public void Open ()
    {
        if (!string.IsNullOrEmpty (url)) {
            Application.OpenURL (url);
        } else {
            Debug.LogWarning ("Unable to open URL, invalid OS");
        }
    }
}
