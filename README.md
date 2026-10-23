# Shape Sprint

A 3D, mobile-oriented runner built with **Unity and C#**. Switch between a cube and a sphere, move between three lanes, collect coins, and navigate shape-based obstacles across six authored levels.

## Gameplay video

[Watch Shape Sprint gameplay on YouTube](https://youtu.be/TKDkQiinLcs?si=J-WU7e8BZ7MtaALF)

## Overview

Shape Sprint combines automatic forward movement with quick decisions about position and shape. The player follows a spline-defined course while choosing a lane and changing form to navigate the track. Breakable obstacles reduce health, lethal hazards end the run, and reaching the finish saves progress and unlocks the next level.

The project includes a main menu, a level selection screen, six gameplay scenes, tutorial prompts, audio feedback, and completion effects. It is structured around Unity components, prefabs, serialized Inspector references, and scene-based progression.

## Features

- **Two player forms:** switch between cube and sphere prefabs while preserving position and rotation.
- **Three-lane movement:** move left or right within a bounded lane layout, with a two-unit offset per lane change.
- **Spline-based traversal:** Unity Splines drives movement along the course.
- **Following camera:** Cinemachine tracks the player's parent transform as the active shape changes.
- **Touch gestures:** horizontal swipes trigger lane changes through Unity's Input System.
- **Shape selection buttons:** UI actions explicitly select the cube or sphere.
- **Guided tutorial:** trigger-based prompts introduce shape selection and horizontal swipes, pausing gameplay while awaiting the required action.
- **Shape-based obstacles:** cube and circular obstacle components activate broken pieces and apply damage on collision.
- **Health feedback:** five starting health points, a health slider, and a red low-health indicator.
- **Collectibles:** rotating coins increment an on-screen counter and play a collection sound.
- **Progression:** completed levels unlock subsequent levels using local PlayerPrefs storage.
- **Game menus:** pause, resume, retry, main-menu navigation, game-over, and level-completion screens.
- **Loading UI:** asynchronous scene loading with a progress slider for routes handled by the loading component.
- **Audio and effects:** gameplay and victory music, obstacle and pickup sounds, vibration calls, player breakup effects, and physics-driven confetti.

## How to play1. Start from the main menu and open level selection.
2. Select an unlocked level. New progress begins at Level 1.
3. Follow the tutorial prompts where present.
4. Swipe horizontally to change lanes and use the shape buttons to select the appropriate form.
5. Collect coins and navigate obstacles while preserving health.
6. Reach the finish to save progress, view the completion screen, and return to level selection.

### Controls

| Action | Control | Implementation notes |
| --- | --- | --- |
| Move left | Swipe left | Left arrow is also implemented in `PlayerScript` for keyboard testing. |
| Move right | Swipe right | Right arrow is also implemented in `PlayerScript` for keyboard testing. |
| Select sphere | Sphere UI button | Calls `convertToSphere()`. |
| Select cube | Cube UI button | Calls `convertToCube()`. |
| Pause / resume | In-game menu buttons | Pausing sets `Time.timeScale` to zero and disables the touch action collection. |
| Retry | Retry button | Reloads the active gameplay scene. |
| Toggle audio | Volume button | Toggles music and sound-effect source muting. |

The input asset also defines a double-tap action, and scripts contain a shape-toggle handler. However, the current `InputManager` does not connect that action to its event, so double-tap switching should be treated as unfinished. Vertical swipes are detected and logged but do not currently perform a gameplay action.

## Getting started

### Requirements

- Unity Hub and Unity Editor **2022.3.37f1**.
- A C# editor such as Visual Studio or Rider for script editing.
- For Android builds: the matching Unity Android Build Support module, SDK, NDK, and OpenJDK.

### Open the project

1. Clone or download this repository.
2. In Unity Hub, choose **Add project from disk** and select the repository root containing `Assets`, `Packages`, and `ProjectSettings`.
3. Open it with Unity 2022.3.37f1.
4. Allow Unity to import assets and restore packages from `Packages/manifest.json`.
5. Open `Assets/Scenes/MainMenu.unity` and enter Play mode.

Start from the main menu for the intended progression flow. Running a level directly can bypass initialization performed by the menus.

### Testing touch input in the Editor

The input bindings target a touchscreen. Use Unity's Device Simulator with touch simulation, or test on a touch-capable device. Mouse input in the ordinary Game view is not explicitly bound in `PlayerActions.inputactions`.

The arrow keys provide lane movement for basic Editor checks, but tutorial continuation can require touch input and shape-button interaction. Keyboard movement reads the legacy input API directly, so it does not follow the same enable/disable path as touch controls.
## Scenes and navigation

All eight application scenes are enabled in `ProjectSettings/EditorBuildSettings.asset`, in this order:

| Build index | Scene | Purpose |
| --- | --- | --- |
| 0 | `Assets/Scenes/MainMenu.unity` | Application entry scene |
| 1 | `Assets/Scenes/Level Menu.unity` | Level selection and unlock checks |
| 2 | `Assets/Scenes/Level 1.unity` | First gameplay level |
| 3 | `Assets/Scenes/Level 2.unity` | Second gameplay level |
| 4 | `Assets/Scenes/Level 3.unity` | Third gameplay level |
| 5 | `Assets/Scenes/Level 4.unity` | Fourth gameplay level |
| 6 | `Assets/Scenes/Level 5.unity` | Fifth gameplay level |
| 7 | `Assets/Scenes/Level 6.unity` | Sixth gameplay level |

Typical flow:

```text
Main Menu -> Level Menu -> Selected Level
                              |
                              +-> Pause -> Resume / Retry / Main Menu
                              +-> Game Over -> Retry / Main Menu
                              +-> Finish -> Save Progress -> Completion UI -> Level Menu
```

Some navigation uses `Loading.LoadLevel()` and asynchronous loading; other routes call `SceneManager.LoadScene()` directly.

## Project structure

```text
Assets/
  Animation/          Animation assets
  Editor/             Editor resources
  Imported Packages/  Imported third-party content
  Materials/          Materials used by the game
  PreFabs/            Reusable game objects
  ProBuilder Data/    ProBuilder resources
  Scenes/             Menus and six gameplay scenes
  Scripts/            Game logic
  Sound/              Audio resources
  Stylized Wood Textures/  Imported texture package
  TutorialInfo/       Unity template readme resources
  UI/                 Interface resources
  PlayerActions.inputactions  Touch action definitions
Packages/
  manifest.json       Package dependencies
ProjectSettings/      Unity project and build configuration
```

Unity generates folders such as `Library`, `Temp`, `Logs`, and `obj` locally. They are not required source assets. Preserve Unity `.meta` files alongside their assets so scene and prefab references remain intact.

## Script architecture

| Script | Responsibility |
| --- | --- |
| `PlayerScript.cs` | Active shape creation, explicit shape selection, lane bounds, camera target assignment, and death delegation. |
| `Input Manager.cs` (`InputManager`) | Creates the action collection, exposes touch events, and enables/disables touch controls. |
| `PlayerActions.cs` | Generated C# wrapper for `PlayerActions.inputactions`; regenerate through Unity after editing the input asset. |
| `InputDetection.cs` | Tracks touch start/end positions and times, classifies swipe direction, and requests lane changes. |
| `Tutorial.cs` | Coordinates tutorial prompts, button availability, swipe steps, and paused instruction screens. |
| `trigger1.cs` | Activates tutorial steps when a collider exits a tutorial trigger. |
| `CubeScript.cs`, `SphereScript.cs` | Handle lethal collisions, activate breakup pieces, play failure feedback, and request game over. |
| `SphereScript2.cs` | Rotates a sphere object. |
| `CubeObstacle.cs`, `CircleObstacle.cs` | Handle breakable obstacles, damage, broken pieces, and successful-passage feedback. |
| `ObstacleCollider.cs`, `CircleObstacleCollider.cs` | Forward player collisions to their obstacle components. |
| `SpikesScript.cs` | Rotates a hazard at a configurable speed. |
| `Health.cs` | Maintains health and the runtime coin count; updates the HUD and invokes death at zero health. |
| `CoinScript.cs`, `Coin2.cs` | Rotate collectible coins, remove them on trigger entry, and update collection feedback. |
| `LevelManager.cs` | Saves completed-level progression and coins, and routes menu navigation. |
| `LevelScene.cs` | Initializes first-launch preferences and enables unlocked level buttons. |
| `Menu.cs` | Manages pause, resume, retry, mute, game-over, and completion UI. |
| `Loading.cs` | Loads scenes asynchronously and updates a loading slider. |
| `Music.cs` | Plays gameplay music and switches to victory music. |
| `SoundEffects.cs` | Provides named methods for gameplay sound effects. |
| `TurnScript.cs` | Plays a sound when a turn trigger is entered. |
| `ConfettiMaker.cs` (`Finish`) | Handles finish triggers, victory audio, repeated confetti spawning, and delayed progress saving. |
| `ConfettiScript.cs` | Randomizes confetti size and impulse, then destroys each piece after one second. |
| `Utilities.cs` | Supplies the coordinate helper used by touch processing; currently retains screen X/Y coordinates. |

### Component connections

Touch input flows from `PlayerActions` through `InputManager` to `InputDetection` or `Tutorial`, then into `PlayerScript`. Obstacle collisions update `Health`, which requests death when health reaches zero. Player shape scripts pass failure handling to `Menu`. The finish component saves through `LevelManager` and opens the completion UI through `Menu`.

Many components locate dependencies through tags such as `Manager`, `Player`, `Health`, `Canvas`, `Music`, `SoundEffects`, and `Enemy`. Other connections are serialized in scenes and prefabs. Keep these tags and Inspector assignments consistent when modifying a level. Obstacle feedback also expects a child named `Perfect` under the tagged canvas, with an Animator containing a `Perfect` trigger.

## Health, coins, and saved progress

- A run starts with **five health points**. Each breakable-obstacle damage event subtracts one point and reduces the health slider by `0.2`.
- At one remaining health point, the health fill changes to red. At zero, the active shape's death routine runs.
- Colliding with an object tagged `Enemy` invokes death directly, regardless of remaining health.
- Coin pickups increment the runtime coin count and update a TextMeshPro label.
- Completing the currently unlocked frontier level increments the saved `Level` value; replaying an earlier level does not advance it again.
- Coins are written to PlayerPrefs when the finish flow saves progress.

| PlayerPrefs key | Use |
| --- | --- |
| `FirstLaunch` | Marks first initialization in the level menu. If absent, that script clears PlayerPrefs before setting the flag. |
| `Level` | Highest unlocked level, defaulting to `1`. |
| `Coins` | Coin total written during completion saving. |

**Current persistence limitation:** the code writes `Coins` to PlayerPrefs, but does not restore that saved value into the static runtime coin counter on startup. Coin restoration across application sessions is therefore incomplete. PlayerPrefs is local device storage; the project does not implement cloud synchronization.

## Building

1. Open **File > Build Settings** in Unity 2022.3.
2. Confirm the eight scenes above are enabled, with `MainMenu` first.
3. Select the desired platform and install its Unity build-support module if needed.
4. For Android, switch to Android and review Player Settings before building.
5. Choose **Build** or **Build and Run**.

The checked-in Android configuration uses application identifier `com.HananaAsif.shape_sprint_the_procrastinators`, version `1.0`, minimum API level `22`, and automatic target API selection. These are repository settings, not confirmation that a particular device or release build has been tested. The input handling setting enables both the legacy Input Manager and the new Input System.

### Build caveat

`CubeScript.cs`, `SphereScript.cs`, and `SphereScript2.cs` import `UnityEditor` from runtime script files. Those imports should be removed if unused, or editor-only code should be guarded or moved to an Editor assembly, before producing a standalone player build. A build has not been validated as part of this documentation update.

## Current limitations and maintenance notes

These notes describe the checked-in implementation and are useful when extending or debugging it:

- **Repeated input subscriptions:** `InputManager.Update()` adds touch callbacks every frame. Register callbacks once during initialization or enable, and unregister them during teardown, to avoid accumulating handlers.
- **Double-tap wiring:** the action and toggle handlers exist, but `InputManager` does not subscribe the action to `DoubleTapStarted()`.
- **Shape resizing is unfinished:** size-related fields and commented code remain in `PlayerScript`; its active resizing coroutine only yields once. Cube/sphere switching is implemented, continuous deformation is not.
- **Keyboard pause shortcut:** the Space-key branch pauses the spline before checking `IsPlaying`, so it is not a reliable pause toggle. Use the UI pause/resume flow.
- **Retry time scale:** `Menu.Retry()` reloads the scene without restoring `Time.timeScale`. A retry initiated while paused can carry a zero time scale into the reloaded scene.
- **Final-level unlock bounds:** completing Level 6 can save `Level = 7`, while `LevelScene` indexes its button array up to the saved value. Clamp unlock iteration to available levels/buttons before extending or testing final-level progression.
- **Trigger filtering:** several pickup, finish, tutorial, and feedback triggers do not check the entering object's tag. Configure collision layers carefully or add explicit player filtering when extending these systems.
- **Confetti spawning:** the finish component spawns confetti every frame after activation. Consider a bounded duration, fixed spawn rate, or pooling for more predictable performance.
- **Filename/class mismatch:** `ConfettiMaker.cs` declares `Finish`. Check the component's scene/prefab attachment when modifying it, and align the names if necessary.

## Suggested manual checks

When changing gameplay, verify the following in Unity and on the target device:

1. Main-menu navigation and first-launch Level 1 availability.
2. Tutorial shape selection, left/right swipe steps, and continuation prompts.
3. Lane bounds and shape replacement while traversing the spline.
4. Correct obstacle passage, obstacle damage, low-health feedback, and lethal collisions.
5. Coin collection, HUD updates, and completion saving.
6. Pause, resume, retry, audio toggling, and main-menu return.
7. Level unlocking after completion, especially completion of Level 6.
8. Restarting the application to check level persistence and the coin-restoration limitation.
9. A target-platform build, including touch input, camera behavior, audio, and scene transitions.
