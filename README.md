# unity-gps-runtime-permissions-bug
A minimal testcase to reproduce possible bug in Android runtime permissions in Unity 3D

To reproduce the bug:
1. Download the project, open in Unity 2020.1.13f1 and build for Android (or use binary in release).
2. Install it on the Android device. Make sure location service is enabled in phone settings.
3. Run the app. Give permissions to access location information when prompted. GPS location is not available.
4. Restart the app. GPS location is not available.
5. Restart the app. GPS location is available and will be available for all subsequent runs.

Fixed through  https://forum.unity.com/threads/runtime-permissions-do-not-work-for-gps-location-first-two-runs.1005001/#post-6520438 thanks to @Tomas1856