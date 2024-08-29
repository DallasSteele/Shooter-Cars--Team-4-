using UnityEngine;

public static class VibrateHandler
{
    private static AndroidJavaClass unityPlayer;
    private static AndroidJavaObject currentActivity;
    private static AndroidJavaObject vibrator;

    static VibrateHandler()
    {
        if (IsAndroid())
        {
            try
            {
                unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                if (vibrator == null)
                {
                    Debug.LogError("Vibrator service not found on device.");
                }
                else
                {
                    Debug.Log("Vibrator service initialized successfully.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to initialize vibrator: " + ex.Message);
            }
        }
    }

    public static void Vibration(long milliseconds = 220)
    {
        if (IsAndroid() && vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
        else
        {
            Debug.LogWarning("Using Handheld.Vibrate as fallback.");
            Handheld.Vibrate();
        }
    }

    public static void Cancel()
    {
        if (IsAndroid() && vibrator != null)
        {
            vibrator.Call("cancel");
        }
    }

    private static bool IsAndroid()
    {
#if UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }
}
