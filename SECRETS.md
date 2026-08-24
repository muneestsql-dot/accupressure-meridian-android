Secrets required for CI builds

This file explains the repository secrets you may add to enable GitHub Actions CI to produce Android APK/AAB builds automatically.

1) UNITY_LICENSE (required for Unity activation on CI)
- Description: Base64-encoded Unity license file (.ulf) exported from your Unity Editor.
- How to export and encode:
  - In Unity Editor: Help -> Manage License -> (Export) -> Save the .ulf file.
  - Base64-encode the file content:
    - macOS / Linux:
      base64 -w 0 unity_license.ulf > unity_license.b64
    - Windows (PowerShell):
      [Convert]::ToBase64String([IO.File]::ReadAllBytes('unity_license.ulf')) | Out-File -Encoding ascii unity_license.b64
  - Copy the contents of unity_license.b64 and save it to the GitHub secret named: UNITY_LICENSE

2) (Optional) ANDROID_KEYSTORE and signing secrets (for a release-signed APK/AAB)
If you want the CI build to produce a signed release APK/AAB, provide the following secrets.

- ANDROID_KEYSTORE
  - Description: base64-encoded keystore (.jks or .keystore)
  - Encode similarly:
    base64 -w 0 my-release-key.jks > keystore.b64
  - Save the contents of keystore.b64 as secret: ANDROID_KEYSTORE

- ANDROID_KEYSTORE_PASSWORD
  - Description: password for the keystore file

- ANDROID_KEY_ALIAS
  - Description: alias name of the key inside the keystore

- ANDROID_KEY_PASSWORD
  - Description: password for the key alias (may be same as keystore password)

3) How the workflow behaves
- If UNITY_LICENSE is provided, the workflow will activate the Unity Editor on the runner before building.
- If keystore-related secrets (ANDROID_KEYSTORE and the password/alias secrets) are provided, the build step will attempt to sign the APK/AAB with the supplied keystore; otherwise a debug-signed APK will be produced.

4) Setting secrets
- Go to: https://github.com/<your-username>/<repo>/settings/secrets/actions
- Click "New repository secret" and paste the base64 string or password value.

5) Triggering a build
- After adding secrets, go to the Actions tab and trigger the "Unity Android Build" workflow via the workflow_dispatch button, or push to main to trigger automatically.
- After the workflow runs, download the artifact from the job summary (artifact name: accupressure-android-build).

Notes and troubleshooting
- If the workflow fails during activation, ensure your UNITY_LICENSE secret contains the full base64 of the exported .ulf file.
- If the build fails due to Unity compile errors, open the project locally in Unity Editor and fix compile errors first.
- If you prefer not to store keystore in the repo secrets, you can omit the signing secrets and sign locally instead.
