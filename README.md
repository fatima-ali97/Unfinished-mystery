# 🎮 Unfinished Mystery

A 3D narrative-driven mystery puzzle game built around a time-loop system where knowledge, rather than items, drives progression.

---

## 🧾 Game Overview

Unfinished Mystery is a narrative-driven puzzle game where players are trapped in a time loop and must solve interconnected puzzles to escape. Each level presents a unique character, environment, and hidden truth that must be uncovered through observation, logic, and memory.

---

## 🔗 Project Links

- [Unfinished Mystery Repository](https://github.com/fatima-ali97/Unfinished-mystery.git)  
- [Milanote Board](https://app.milanote.com/1VYf2L16ikrc9I/game-development-project?p=uQPTEodVQDJ)  
- [Trello Scrum Board](https://trello.com/b/YtySt35G/scrum-board)  

---

## 👥 Group Members

| Name | Student ID |
|------|------------|
| Fatema Ebrahim | 202304056 |
| Fatema Maitham | 202304661 |
| Maram Shubbar | 202305590 |
| Kawther Abdulla | 202302702 |
| Mohamed Abdali | 202304651 |

---

## 🧩 Levels

| Level | Role | Description | Developer |
|------|------|-------------|-----------|
| Level 1 | Professor | Solve mathematical puzzles in a personal office to uncover hidden academic secrets. | Fatema Ebrahim |
| Level 2 | Detective | Investigate an abandoned apartment and connect evidence to reveal a missing case. | Fatema Maitham |
| Level 3 | Projectionist | Use film reels and projection equipment to reconstruct events in a cinema. | Maram Shubbar |
| Level 4 | Photographer | Analyze photos and documents in an archive room to uncover manipulated truth. | Kawther Abdulla |
| Level 5 | Doctor | Explore a lab and solve complex puzzles to expose unethical experiments. | Mohamed Abdali |

---

## 🎯 Game Highlights
 
- **Narrative-Driven Gameplay**: The game focuses on storytelling through exploration, clues, and puzzle solving.
- **Level-Based Storytelling**: Each level presents a different character, environment, and hidden truth, while all levels together build the full mystery.
 
---

## ⭐ Features

### Advanced Features

1. **Time Loop Reset System**  
   The environment resets every 5 minutes when the player fails to escape. Instead of allowing items to carry over between attempts, the system encourages players to learn from each loop and progress through observation, memory, and understanding.

2. **Hint System**  
   The hint system changes according to the number of loops the player has used. Early attempts provide no hints so that players can explore independently. Later attempts offer subtle guidance, while the final loops provide more direct help to support progression without removing the challenge completely.


### Core Features

1. **Rotating Character Roles**  
   Each level places the player in a different role, such as a professor, detective, projectionist, photographer, or doctor. Every character has a unique story, hidden truth, and puzzle style.

2. **5-Minute Timer**  
   A visible countdown timer creates constant pressure during gameplay. The player must explore, discover clues, and solve puzzles before time runs out and the loop resets.

3. **Limited Attempts**  
   Each level gives the player a fixed number of loops. This prevents players from relying on endless retries and encourages more careful and strategic decision-making.

4. **Interactive Object System**  
   Players can interact with drawers, books, papers, devices, cabinets, and other objects in the environment to reveal clues and move the mystery forward.

5. **Connected Puzzle Chain**  
   The puzzles are designed as a sequence of linked steps. Solving one part reveals the next clue, creating a natural and structured progression through the mystery.

6. **Memory Journal**  
   Clues and discoveries from previous loops are recorded in a memory journal that the player can review at any time. This allows progress to carry forward through knowledge rather than physical items.

7. **Temporary Items**  
   Some items can be collected and used during the current loop, but they disappear when the room resets. This reinforces the idea that learning, not inventory, is the main form of progression.

8. **Escalating Tension**  
   As the timer approaches zero, visual and audio effects become more intense. This increases pressure and makes the final moments of each loop feel more urgent.

9. **Performance Scoring**  
   After completing a level, the player is shown the number of loops used and receives a star rating based on performance. Finishing the full game unlocks a completion badge.

10. **Final Action Escape**  
   Escaping a level is not always achieved by simply opening a door. In some levels, the player must perform a final action or make an important choice to properly break the loop and conclude the story.

---

## 🎮 Core Mechanics

- Time-loop system (environment resets every loop)  
- Puzzle chains (multi-step problem solving)  
- Knowledge-based progression (no item carry-over)  
- Environmental storytelling  
- Interactive objects and hidden clues  

---

## 🎯 Design Highlights

- Genre: Mystery, Puzzle, Narrative  
- Target Audience: Players aged 15+  

### Unique Selling Points

- Time-loop integrated gameplay  
- Knowledge instead of inventory progression  
- Multiple characters and environments  
- Strong narrative through environment  

---

## 🛠️ Tools & Technologies

- Engine: Unity (6000.1.13f1)  
- Language: C#  
- Audio: Audacity  
- Version Control: GitHub  
- Planning: Trello + Milanote  

---

## ⚙️ Setup

1. Clone the repository:

```bash
git clone https://github.com/fatima-ali97/Unfinished-mystery.git
```
2. Open the project in 6000.1.13f1   
3. Load scene: MainMenu  
4. Press Play  
---

## 🎮 How to Play

Controls are designed for keyboard and mouse.

### 🎹 Controls

| Action | Key |
|--------|-----|
| Move | W / A / S / D or Arrow Keys|
| Look Around | Mouse |
| Interact / Inspect / Read | E |
| Open Memory Journal | Tab |
| Pause Menu | Esc |
| Inventory Slots (Temporary Items) | 0 – 9 |

---

## 🎯 Objectives

- Escape each level before the loop ends  
- Solve interconnected puzzle chains  
- Discover hidden clues  
- Understand each character’s story  
- Progress using knowledge from previous loops  

---

## ⚙️ How It Works

- The game runs on a time-loop system  
- Each loop resets the environment  
- Players have limited attempts per level  
- Items do not persist between loops  
- Knowledge is the only progression tool  
- Clues are hidden in environment, sound, and visuals  

---

## 🔄 Game Flow

1. Player wakes up in a level  
2. Explores environment  
3. Finds clues  
4. Solves puzzles  
5. Loop resets if time runs out  
6. Player retries with knowledge  
7. Player escapes level  

---

## 💡 Hint System

- Loops 1–3: No hints  
- Loops 4–6: Subtle hints  
- Loops 7–10: Direct hints  

---

## ⚠️ Tips & Warnings

- Pay attention to small details  
- Sound and lighting are important clues  
- Manage time carefully  
- Failure is part of learning  

---

## 🧠 Win / Lose Conditions

**Win Condition:** 
Solve all puzzles and escape before the final loop ends.  

**Lose Condition:** 
Fail to escape within the allowed loops.  

---

## 📂 Project Structure
 
The project is mainly organized inside the **Assets/** folder, which contains the core game content, scripts, scenes, and resources used in development.
 
```text
Assets/                                      // main Unity project folder; everything goes inside here
├── Animations/                              // all animation-related files
│   ├── Characters/                          // player/NPC walk, idle, interact, reaction animations
│   ├── Effects/                             // flicker, pulse, warning, loop-reset, screen effect animations
│   └── UI/                                  // button hover, panel open/close, fade, menu transition animations
├── Audio/                                   // all audio files
│   ├── Ambient/                             // room hum, projector noise, lab buzz, background atmosphere sounds
│   ├── Music/                               // menu music, loading music, level background music, tense music
│   └── SFX/                                 // clicks, footsteps, drawers, doors, puzzle sounds, pickup sounds
├── Fonts/                                   // font files and TextMesh Pro font assets used in menus and UI text
├── Materials/                               // Unity materials for walls, props, objects, glow effects, UI materials
├── Models/                                  // all 3D model files
│   ├── Characters/                          // player model, NPC models, character meshes
│   ├── Environment/                         // walls, floors, ceilings, shelves, room parts, doors, big structures
│   └── Props/                               // books, mugs, reels, lamps, desks, drawers, tools, keys, laptops
├── Prefabs/                                 // reusable ready-made Unity objects
│   ├── Characters/                          // player prefab, NPC prefab, complete reusable character setups
│   ├── InteractiveObjects/                  // drawers, clues, locks, switches, notes, puzzle objects with scripts
│   ├── LevelPieces/                         // grouped reusable room chunks like office corners, shelf sets, lab stations
│   ├── Props/                               // reusable object prefabs like desk, chair, projector, cabinet, book stack
│   └── UI/                                  // reusable UI objects like panels, HUD pieces, buttons, popup screens
├── Scenes/                                  // all scene-related folders
│   ├── Levels/                              // gameplay level scenes and their related scene-specific files
│   │   ├── Level1/                          // professor office level scene and files for level 1
│   │   ├── Level2/                          // detective apartment level scene and files for level 2
│   │   ├── Level3/                          // cinema level scene and files for level 3
│   │   ├── Level4/                          // archive library level scene and files for level 4
│   │   └── Level5/                          // laboratory level scene and files for level 5
│   └── UI/                                  // front-end and UI scene folders
│       ├── Loading/                         // loading screen scene and any files related to loading
│       ├── MainMenu/                        // main menu scene and its related files
│       └── PauseMenu/                       // pause menu scene/panel-related files, depending on how you set it up
├── ScriptableObjects/                       // ScriptableObject data assets like level data, clue data, settings, configs
├── Scripts/                                 // all C# scripts
│   ├── Core/                                // shared helpers, utility scripts, startup/boot logic, common systems
│   ├── HintSystem/                          // hint logic, loop-based hint progression, unlock conditions
│   ├── Interaction/                         // inspect/use/click/interact object scripts
│   ├── Journal/                             // memory journal, notes, clue records across loops
│   ├── LevelSpecific/                       // scripts used only for special mechanics in certain levels
│   ├── Managers/                            // GameManager, AudioManager, SceneManager, UIManager, central control scripts
│   ├── Player/                              // movement, look, camera, player controller scripts
│   ├── Puzzle/                              // puzzle logic, code locks, clue matching, puzzle chain scripts
│   ├── TimeLoop/                            // timer, loop count, reset system, time-up handling
│   └── UI/                                  // menu logic, button behavior, settings logic, HUD scripts
├── Settings/                                // input actions, configuration assets, gameplay settings, project config assets
├── Shaders/                                 // custom shaders and shader-related files for special visual rendering
├── SourcedAssets/                           // imported third-party assets, asset packs, outside resources, downloaded content
├── Sprites/                                 // 2D images used directly in Unity
│   ├── Icons/                               // pause, settings, volume, clue, journal, small symbol icons
│   ├── Illustrations/                       // decorative art, character cards, story art, loading illustrations
│   └── UI/                                  // menu backgrounds, bars, buttons, frames, screen art, UI images
├── Textures/                                // image files used on 3D materials/models like wood, paper, wall, floor textures
├── TutorialInfo/                            // readme/demo/tutorial files from imported assets; mostly package helper content
└── VFX/                                     // particle systems and visual effects like glow, sparks, dust, warning pulses
```
 
---
 

## 🎯 Vision

Unfinished Mystery delivers a tense and immersive experience where players rely on logic, observation, and memory to uncover hidden truths.
