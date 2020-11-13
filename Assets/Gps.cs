using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

public class Gps : MonoBehaviour
{
  public Text text;
  private int currentRun;
  private bool needsStartGPS = true;

  private void RequestPermissions()
  {
    #if UNITY_ANDROID && !UNITY_EDITOR
    if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
    {
      Permission.RequestUserPermission(Permission.CoarseLocation);
    }
    if (Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) && !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
    {
      Permission.RequestUserPermission(Permission.FineLocation);
    }
    if (needsStartGPS && Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) && Permission.HasUserAuthorizedPermission(Permission.FineLocation))
    {
      Input.location.Start();
      Input.compass.enabled = true;
      Input.gyro.enabled = true;
      Input.compensateSensors = true;
      needsStartGPS = false;
    }
    #endif
  }

  void Start()
  {
    currentRun = PlayerPrefs.GetInt("current_run", 0);
    currentRun++;
    PlayerPrefs.SetInt("current_run", currentRun);
  }

  void Update()
  {
    RequestPermissions();
    text.text =
      "Current run = " + currentRun.ToString() + "\n" +
      "Permissions given = " + Input.location.isEnabledByUser.ToString() + "\n" +
      "GPS ready = " + (Input.location.status == LocationServiceStatus.Running).ToString() + "\n" +
      "Latitude = " + Input.location.lastData.latitude.ToString() + "\n" +
      "Longitude = " + Input.location.lastData.longitude.ToString() + "\n"
      ;
  }
}
