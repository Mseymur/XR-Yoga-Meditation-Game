# AuraVibes 🧘‍♀️✨
### *AR for orientation, VR for depth.*

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=for-the-badge&logo=unity)
![Meta Quest](https://img.shields.io/badge/Meta%20Quest-VR%20%2F%20MR-blue?style=for-the-badge&logo=meta)
![XR Interaction Toolkit](https://img.shields.io/badge/XR%20Interaction-Toolkit-crimson?style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-Android%20%28Quest%29-lightgrey?style=for-the-badge&logo=android)

**AuraVibes** is a seamless Mixed Reality (MR) application designed to bridge the gap between your physical environment and a serene virtual escape. 

By starting in **Augmented Reality (AR)**, users can comfortably orient themselves, place their yoga mat, and configure their session before transitioning into a fully immersive **Virtual Reality (VR)** environment for deep focus and relaxation.

> **Project Goal:** To create a distraction-free, "comfort-first" XR experience that teaches foundational yoga poses and offers a meditative space with ambient soundscapes.

## 🎯 Core Goals

1. Deliver a **peaceful, distraction-free practice** space in VR.
2. Make sessions **easy to follow**: clear visuals, replay/previous controls, readable UI in VR.
3. Build & document a **Quest-ready pipeline** (Unity → Android/IL2CPP) and the lessons learned.

---

## ✨ Key Features

*   **Seamless XR Transition:** Begin in your living room with Passthrough AR to set up your space, then dissolve into a high-altitude mountain lake for your practice.
*   **Virtual Instructor:** Follow a 3D avatar instructor with rigged animations for clear, visual pose guidance.
*   **Guided Breathing:** Audio-visual cues help synchronize your breath with your movements.
*   **Custom Environments:** Choose from multiple calm settings (currently featuring "Serene Lake" and "Dojo").
*   **Fail-Safe UI:** World-space menus designed for VR comfort, featuring "Go Back" and "Replay Step" options to prevent frustration.

## 🛠️ Tech Stack

*   **Engine:** Unity 2022.3 LTS
*   **XR Framework:** OpenXR + Meta XR Core SDK
*   **Interaction:** XR Interaction Toolkit (XRI)
*   **Animation:** Mixamo + Custom Retargeting
*   **Design Tools:** Figma (UI/UX)
*   **AI Assistants:** ChatGPT (Coding support)

---

## � UX Decisions (Comfort & Clarity)

*   **Simple, legible world-space UI:** Large hit-targets, readable at VR distances.
*   **Session safety nets:** **Back** and **Replay step** are always available (users can recover after input hiccups).
*   **Ambient soundscapes:** Minimal but effective; encourages breathing & focus.
*   **Fail-soft navigation:** Joystick-friendly flows; no precision aiming required to proceed.

## 🧱 Development Process

1.  **Foundations**: Created a Quest-ready Unity project, XR setup, controller input.
2.  **Environment Pass**: Blocked out maps, basic lighting, performance budgets.
3.  **Character & Poses**: Rig setup, pose states/animations, timing for guidance.
4.  **UI**: World-space interface, session controls, accessible nav patterns.
5.  **Audio**: Ambient loops; volume controls; fade in/out between scenes.
6.  **Polish**: Iterate on timing, readability, and comfort settings.

---

## 🚀 Getting Started

### Prerequisites

*   Meta Quest 2, 3, or Pro
*   Unity Hub with Android Build Support (OpenJDK & Android SDK/NDK)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Mseymur/XR-Yoga-Meditation-Game.git
    ```
2.  **Open in Unity:**
    *   Add the project to Unity Hub.
    *   Open the project (wait for package resolution).
3.  **Build & Run:**
    *   Connect your Quest via USB.
    *   Go to **File > Build Settings**, select **Android**, and click **Build and Run**.

---

## 🐛 Challenges & Lessons Learned (Technical)

> Common issues encountered during Unity XR/Quest development.

*   **SDK Fragmentation / Package Drift**
    *   *Issue:* OpenXR vs. legacy Oculus Integration differences; feature sets don’t map 1:1.
    *   *Mitigation:* Lock package versions; prefer one XR path (OpenXR + Meta feature group); keep a “known-good” manifest.
*   **Controller Input Edge Cases**
    *   *Issue:* UI ray vs. direct interactors, hover/click focus conflicts, and canvas physics settings.
    *   *Mitigation:* Explicit UI input module config; consistent action maps; simplified canvases.
*   **Android/Gradle/IL2CPP Pipeline**
    *   *Issue:* Misaligned SDK/NDK/Gradle versions causing build failures; long IL2CPP times.
    *   *Mitigation:* Pin Editor + Android tools; cache builds; keep Player Settings minimal.
*   **Performance on Device**
    *   *Issue:* Overdraw from UI, unbaked lighting on mobile, too many real-time lights/shadows.
    *   *Mitigation:* Baked lighting where possible, reduce transparent materials, occlusion culling, ASTC compression.

## 📈 Roadmap

- [x] **Beginner Yoga Class**
- [x] **Environment selection**
- [x] **Guided meditation room**
- [x] **Core UI & navigation**
- [ ] Intermediate & Advanced classes (content & sequencing)
- [ ] More environments & ambiance sets
- [ ] Expanded guidance (voice-over captions, pose safety notes)
- [ ] Basic analytics (session duration, completions) for research evaluation

---

## 👥 The Team

*   **Seymur Mammadov** - Developer & Engineering
*   **Sara Waldenberger** - UI/UX
*   **Dorota Bičárová** - Environments
*   **Eliška Zemanská** - Documentation

## 📄 License

This project is intended for educational and portfolio purposes.

---

*[Learn more about the development of AuraVibes.](https://mseymur.framer.website/projects/ar-vr-meditation-app)*
