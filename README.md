# 🎥 YouTube Videos Summarization Project

### 📝 Project Description
An advanced web application designed to summarize YouTube videos efficiently. The system extracts transcripts and audio from YouTube videos and utilizes AI models to generate concise, accurate, and structured text summaries, saving users valuable time and effort.

---

## 🛠️ Technologies Used

### Backend & AI Processing (Django)
* **Python & Django Framework:** Powers the backend processing and robust REST APIs.
* **AI & NLP Models:** Utilized for smart text summarization and semantic analysis of video transcripts.
* **FFmpeg Core Tools:** Handles media, audio extraction, and processing (`ffmpeg`, `ffprobe`, `ffplay`).

### Web Application & UI (.NET C#)
* **ASP.NET Core MVC (C#):** Provides the main graphical user interface, user authentication, and core dashboard.
* **Entity Framework Core:** Manages database operations, user records, and generated summary history.
* **Bootstrap & Frontend Assets:** Ensures a clean, modern, and fully responsive user interface (UI).

---

## 🚀 Core Features
1. **Automated Transcript Extraction:** Fetches precise video transcripts and conversations directly from YouTube.
2. **AI-Powered Summarization:** condenses lengthy video content into clear, structured, and bulleted main points.
3. **User Management System:** Complete authentication workflow including secure registration, login, and profile control.
4. **Interactive Dashboard:** A seamless control panel to manage summarized videos and track user activities.

---

## ⚙️ How to Run

### 1️⃣ Running the Django Backend (API)
```bash
# Navigate to the backend directory
cd "API/Django final API/Foodiez_backend"

# Install the required Python dependencies
pip install -r requirements.txt

# Start the development server
python manage.py runserver