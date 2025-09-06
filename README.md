# **XR-Game — VR Yoga & Meditation for Meta Quest 3**

A calm, guided VR experience for Meta Quest 3 that teaches foundational yoga poses and offers a meditative space with ambient soundscapes. Built in Unity as part of a university course on VR/AR app development, the project emphasizes comfort-first UX, clear guidance, and robust controller navigation to keep users focused and relaxed—even when VR input gets finicky.

## **✨ What’s inside**

- **Beginner Yoga Class** (current) with a paced sequence of poses and a 3D instructor/character
- **Guided Meditation Room** with spatial/ambient audio for relaxation
- **Environment Selector** (e.g., multiple themed maps/biomes)
- **Controller-friendly UI** (world-space menus; “go back / replay step” affordances; joystick-safe navigation)
- **Future tiers** (planned): Intermediate & Advanced classes, more environments, additional guidance

---

## **🎯 Goals**

1. Deliver a **peaceful, distraction-free practice** space in VR.
2. Make sessions **easy to follow**: clear visuals, replay/previous controls, readable UI in VR.
3. Build & document a **Quest-ready pipeline** (Unity → Android/IL2CPP) and the lessons learned.

---

## **🧭 User Flow**

1. **Landing / Profile** → pick **Beginner** (Intermediate/Advanced planned).
2. **Environment** → choose a setting.
3. **Session Controls** → Start / Pause, **Previous / Next pose**, and **Replay current step**.
4. **Guided Meditation** → optional room to wind down.

---

---

## **🧪 UX decisions (comfort & clarity)**

- **Simple, legible world-space UI**: large hit-targets, readable at VR distances.
- **Session safety nets**: **Back** and **Replay step** are always available (users can recover after input hiccups).
- **Ambient soundscapes**: minimal but effective; encourages breathing & focus.
- **Fail-soft navigation**: joystick-friendly flows; no precision aiming required to proceed.

---

## **🧱 Development Process**

1. **Foundations** — created a Quest-ready Unity project, XR setup, controller input.
2. **Environment Pass** — blocked out maps, basic lighting, performance budgets.
3. **Character & Poses** — rig setup, pose states/animations, timing for guidance.
4. **UI** — world-space interface, session controls, accessible nav patterns.
5. **Audio** — ambient loops; volume controls; fade in/out between scenes.
6. **Polish** — iterate on timing, readability, and comfort settings.

---

## **🐛 Challenges & Lessons Learned (technical)**

> These are common in Unity XR/Quest development and came up during the course. Use/modify to match your exact issues.
> 
- **SDK fragmentation / package drift**
    - OpenXR vs. legacy Oculus Integration differences; feature sets don’t map 1:1.
    - **Symptom:** input actions or controller models randomly break after package updates.
    - **Mitigation:** lock package versions; prefer one XR path (OpenXR + Meta feature group); keep a “known-good” manifest.
- **Controller input edge cases**
    - UI ray vs. direct interactors, hover/click focus conflicts, and canvas physics settings.
    - **Mitigation:** explicit UI input module config; consistent action maps; simplified canvases.
- **Android/Gradle/IL2CPP pipeline**
    - Misaligned SDK/NDK/Gradle versions causing build failures; long IL2CPP times.
    - **Mitigation:** pin Editor + Android tools; cache builds; keep Player Settings minimal.
- **Performance on device**
    - Overdraw from UI, unbaked lighting on mobile, too many real-time lights/shadows.
    - **Mitigation:** baked lighting where possible, reduce transparent materials, occlusion culling, ASTC compression.
- **In-editor vs on-device behavior**
    - Input and audio mixing felt different on headset; some errors only reproduced on device.
    - **Mitigation:** frequent device testing; small repro scenes.
- **Ecosystem incoherence**
    - Docs/samples assume different combinations of packages; tutorials quickly go stale.
    - **Mitigation:** minimal stack; document working versions in the repo; create a troubleshooting log.

---

## **📈 What’s implemented vs. Roadmap**

- ✅ **Beginner Yoga Class**
- ✅ **Environment selection**
- ✅ **Guided meditation room**
- ✅ **Core UI & navigation**
- ◻️ Intermediate & Advanced classes (content & sequencing)
- ◻️ More environments & ambiance sets
- ◻️ Expanded guidance (voice-over captions, pose safety notes)
- ◻️ Basic analytics (session duration, completions) for research evaluation
- ◻️ Comfort & accessibility options (dominant hand toggle, larger UI preset)
