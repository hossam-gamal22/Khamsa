# 🎮 Khamsa L3b

A social/party game built with **Unity 6.000.28f1**, designed for friends and families.  
Fast-paced matches (~5 minutes) with **25 unique mini-games** across trivia, music, movies, football, riddles, and acting.  
Supports both **offline** and **online** play.

---

## ⚡ Overview
- 25 mini-games (2 offline-only, 23 offline+online).
- 2–8 players per room.
- Match length: ~5 minutes.
- Built with modular, prefab-based UI and CSV-driven content.

---

## 📋 Mini-Games
**Offline Only**
1. Explain It – describe without saying the word.
2. One Minute, Collect the Cards – forehead guessing rounds (+1/+2/+3).

**Offline + Online Examples**
- Finish the Lyrics  
- Insert the Word in a Song  
- The Spy  
- Movies in Emojis  
- Football Trivia  
- True or False  
- Cheeky Riddles  
- Football XO (Tic-Tac-Toe)  

*(See full list in docs.)*

---

## ⏱️ Rules
- Max 10 questions or 3 rounds.  
- No immediate repeats.  
- Results after each round.  

---

## 🪙 Economy
- **Start**: 100 coins (+50 for linking account).  
- **Earn**: Daily login (+10), wins (+5), ads (+20), daily challenge (20–50).  
- **Spend**: Create room (≥10), boosts (time +5, skip+points +10, extra hint +5).  
- **Store**: Avatars (100–1000), Stickers (300–800), Sounds (200–2000), Coin Packages (CSV-based).  

---

## 🛠 Project Structure
```plaintext
Assets/_Project/
  Scenes/      Boot, App Shell, Home, Avatar_Page
  Scripts/
    Core/      (CoinsWallet, DailyRewardService, UsernameService, AvatarService, ...)
    UI/        (AppShellController, ProfileBarController, DayItem, ...)
    General/   shared logic
    Main Screen/ navigation
    Services/  (RewardedAdService, DataSyncService, AudioService, ...)
  Prefabs/     (Cards, Pages, Splash, Popups)
  Resources/
    DB/        characters.csv, stickers.csv, sounds.csv, coin_packages.csv
    Avatars/, Stickers/, Sounds/, Icons/, Music/, ...
  Fonts/
```

---

## 🧭 Architecture
- **BootLoader** initializes services → loads `App Shell`.  
- **AppShell** contains header, profile bar, footer nav, and dynamic page root.  
- **Stable APIs**: `Init()`, `Open()`, `Close()`, `Build()`, `ApplyState()`.  
- **Namespaces**: `Project.Core` • `Project.UI` • `Project.Systems`.

---

## 📺 UI Pages
- **Daily Reward Splash**: 7 days + bonus, claim/double logic, tomorrow countdown.  
- **Home**: header (info, gift, settings, coins), profile bar (username edit, avatar), main buttons.  
- **Shop**: characters, stickers, sounds, coins (CSV-driven).  
- **Friends**: list, add, requests.  
- **Profile**: stats, inventory.  

---

## 🖼️ Screens
Reference screenshots available in [Google Drive](https://drive.google.com/drive/folders/1LxgFjcOQ3oQvdsJrzfuZDTm1uLUEhnfU).  
Local previews also included in the repo (`/screens`).

---

## 🚀 Development Setup
1. Clone repo & open in **Unity 6.000.28f1**.  
2. Import CSV data into `Resources/DB/`.  
3. Assign services in **Boot scene**:
   - CoinsWallet: starting coins.  
   - AudioService: music + SFX clips.  
   - AvatarService: CSV + folder path.  
   - UsernameService: prefix, random digits, max length.  
4. Play from `Boot.unity`.  

---

## 📦 Delivery Rules
- Deliver only **.zip packages** (no snippets).  
- Always include dependent scripts if one changes.  
- Must be **compile-ready (0 errors)**.  
- Respect namespaces and APIs.

---

## 🔮 Next Steps
- Friends system + Profile statistics.  
- Multiplayer room networking.  
- Integration of all 25 mini-games.  
- Monetization: IAP + ads.  
- Final polish, animations, and release.

---

## 📚 References
- [Master Doc (HTML)](./KhamsaLa3b_MasterDoc_v5.html)  
- [Complete Setup Guide (PDF)](./Complete%20Setup%20Guide%20-%20Khamsa%20L3b%20(Final).pdf)  
- [CSV Data](./Resources/DB/)  
- [Screenshots](https://drive.google.com/drive/folders/1LxgFjcOQ3oQvdsJrzfuZDTm1uLUEhnfU)

---
