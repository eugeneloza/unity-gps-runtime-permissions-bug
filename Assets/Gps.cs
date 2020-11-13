using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

public class Gps : MonoBehaviour
{
  public Text text;
  private int currentRun;
  
  #if UNITY_ANDROID && !UNITY_EDITOR
  private void RequestPermissions()
  {
    if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) || !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
    {
      Permission.RequestUserPermission(Permission.CoarseLocation);
      Permission.RequestUserPermission(Permission.FineLocation);
    }
    Input.location.Start();
    Input.compass.enabled = true;
    Input.gyro.enabled = true;
    Input.compensateSensors = true;
  }
  #endif

  void Start()
  {
    #if UNITY_ANDROID && !UNITY_EDITOR
    RequestPermissions();
    #endif
    currentRun = PlayerPrefs.GetInt("current_run", 0);
    currentRun++;
    PlayerPrefs.SetInt("current_run", currentRun);
  }

  void Update()
  {
    #if UNITY_ANDROID && !UNITY_EDITOR
    if (!Input.location.isEnabledByUser)
    {
      RequestPermissions();
    }
    #endif
    text.text =
      "Current run = " + currentRun.ToString() + "\n" +
      "Permissions given = " + Input.location.isEnabledByUser.ToString() + "\n" +
      "GPS ready = " + (Input.location.status == LocationServiceStatus.Running).ToString() + "\n" +
      "Latitude = " + Input.location.lastData.latitude.ToString() + "\n" +
      "Longitude = " + Input.location.lastData.longitude.ToString() + "\n"
      ;
  }
}
