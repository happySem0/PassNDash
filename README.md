# PassNDash

Pass ‘n’ Dash is a fast-paced, 2-player co-op action game where players must work together to pass a ball, dodge obstacles, and outlast relentless chasers in a chaotic, enclosed arena.

🧠 Concept Overview
Genre: 2D Co-op Action / Arena Dodge Game

Players: 2 (local co-op)

Engine: Unity

Perspective: Top-down or 2D side view (depending on your final design)

🕹️ Gameplay Summary
Two players are placed on opposite sides of a divided field, separated by a fence or barrier. They must collaborate under pressure, passing a ball back and forth while avoiding AI-controlled animals (like dogs) that chase the ball holder.

If a dog catches the ball, the team loses.

The goal is to survive long enough for all chasing animals to run out of stamina.

⚽ Key Features
🔁 Split Field Co-op – Players are separated and must pass across a central barrier.

🐕 Chasing Enemies – AI animals pursue the ball, not the players.

🧱 Physics-Based Movement – Players and the ball use realistic physics; the ball bounces off walls.

💡 Obstacle-Filled Arenas – Each level adds more obstacles to create complexity.

🔋 Stamina Mechanics – Enemies have stamina meters. Exhaust them to win.

📈 Progressive Difficulty – Levels increase in difficulty with:

More/different obstacles

More/faster/more intelligent enemies

Higher stamina values

New hazards

🌪️ Chaos Mode
After Level 10, a special Chaos Mode unlocks:

Multiple enemies

Randomized hazards

Power-ups and modifiers

Optional INF BALLS mode for absurd fun

🧪 Development Structure
✔️ Unity (C#)

✔️ Git for version control

✔️ GitHub for remote hosting

✔️ Project follows PascalCase naming convention

📁 Folder Structure (Assets)
mathematica
Copy
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
│   ├── UI/
├── Animations/
├── Fonts/
├── Resources/
🔧 Version Control Notes
Git used for local and remote version control.

Remote repo connected via GitHub.

Branching strategy encouraged (e.g., feature/player-movement, bugfix/ball-stuck).

Unity .gitignore in use to ignore unnecessary files.

## Building and Running

1. Open the project in **Unity 2021.3 LTS** or a newer version.
2. Press **Play** in the Unity Editor to try the game.
3. To create a standalone build, open **File > Build Settings**, choose your target platform, and press **Build**.

---

This project originally started from Unity's URP template.
