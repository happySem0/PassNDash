# PassNDash

Pass ‘n’ Dash is a fast-paced 2-player co-op action game where players work together to pass a ball, dodge obstacles, and outlast relentless chasers in a chaotic arena.

## Concept Overview

|                | Details                                |
| -------------- | -------------------------------------- |
| **Genre**      | 2D Co-op Action / Arena Dodge Game     |
| **Players**    | 2 (local co-op)                        |
| **Engine**     | Unity                                  |
| **Perspective**| Top-down or 2D side view               |

## Gameplay Summary

Two players are placed on opposite sides of a divided field separated by a fence. They must collaborate under pressure, passing a ball back and forth while avoiding AI-controlled animals (like dogs) that chase the ball holder. If a dog catches the ball, the team loses. Survive long enough for all chasing animals to run out of stamina to win.

## Key Features

- **Split Field Co-op** – players pass across a central barrier.
- **Chasing Enemies** – AI animals pursue the ball, not the players.
- **Physics-Based Movement** – players and the ball bounce off walls.
- **Obstacle-Filled Arenas** – each level adds new obstacles.
- **Stamina Mechanics** – enemies have stamina meters you must exhaust.
- **Progressive Difficulty** with:
  - More or different obstacles
  - Faster or smarter enemies
  - Higher stamina values
  - New hazards

## Chaos Mode

Unlocked after Level 10:

- Multiple enemies
- Randomized hazards
- Power-ups and modifiers
- Optional **INF BALLS** mode for absurd fun

## Development Structure

- Unity (C#)
- Git for version control
- GitHub for remote hosting
- Project follows PascalCase naming convention

## Folder Structure (Assets)

```text
Assets/
├── Art/
├── Audio/
├── Materials/
├── Prefabs/
├── Scenes/
├── Scripts/
│   ├── Player/
│   ├── Ball/
│   ├── AI/
│   ├── Managers/
│   └── UI/
├── Animations/
├── Fonts/
└── Resources/
```

## Version Control Notes

- Git used for local and remote version control.
- Remote repo connected via GitHub.
- Branching strategy encouraged (e.g., `feature/player-movement`, `bugfix/ball-stuck`).
- Unity `.gitignore` in use to ignore unnecessary files.

## Building and Running

1. Open the project in **Unity 2021.3 LTS** or a newer version.
2. Press **Play** in the Unity Editor to try the game.
3. To create a standalone build, open **File > Build Settings**, choose your target platform, and press **Build**.

---

This project originally started from Unity's URP template.
