# InkDialogModerator
An implimentation of Ink in Unity using events.

Verified working on Unity 2021.3.29f1 and Ink Integration for Unity 1.1.8

[Ink Integration for Unity](https://assetstore.unity.com/packages/tools/integration/ink-integration-for-unity-60055) is required for these scripts to work.

# Usage
* Pull the repo into your Scripts folder.
* Remove the `/Cinemachine` and `/Rewired` directories if you are not using those plugins.
* Add a `InkDialogModerator`, `DebugDialogReadout`, and `ContinueControllerUnityInput` Components somewhere in your scene.
* Write a short ink story and add it to the Moderator.
* Set the `sceneStartsInDialog` variable to 0
* Press Play.
* Press Space to move to the next line of dialog.
* View the dialog in your console.
