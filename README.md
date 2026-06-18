# 🎥 YouTube Videos Summarization

An AI-powered web application that automatically extracts transcripts from YouTube videos and generates concise, structured summaries — saving users time and effort.

---

## 📸 Screenshots

### Login Page
<img width="1918" height="1078" alt="login" src="https://github.com/user-attachments/assets/4fd83b43-fd81-4ba3-87f1-8fb4e8d09ccc" />

### Home Page
<img width="1918" height="1075" alt="home" src="https://github.com/user-attachments/assets/abaa0408-815e-4280-bd18-6976c8276e05" />

### Video Summarization
<img width="1918" height="1078" alt="summarize" src="https://github.com/user-attachments/assets/6a5333ba-e24a-4de3-98f0-e817bbbe25e8" />

### Summary Result
<img width="1918" height="1077" alt="result" src="https://github.com/user-attachments/assets/07048b24-e27d-465a-b956-f1954c69dc9a" />


---

## ✨ Features

- 🔗 Paste any YouTube video URL and get an instant summary
- 🎙️ Audio extraction using **FFmpeg** + transcription using **OpenAI Whisper**
- 🤖 AI-powered text summarization
- 📋 Side-by-side view of full transcript and summary
- 📥 Download summary as a file
- 👤 User authentication (Register / Login / Logout)
- 📊 Personal dashboard to track summarized videos

---

## 🛠️ Tech Stack

### Backend & AI (Django)
| Technology | Purpose |
|---|---|
| Python & Django | Backend logic and REST API |
| OpenAI Whisper | Speech-to-text transcription |
| FFmpeg | Audio extraction from YouTube videos |
| Django REST Framework | API endpoints |
| JWT Authentication | Secure user sessions |

### Frontend & Web App (ASP.NET)
| Technology | Purpose |
|---|---|
| ASP.NET Core MVC (C#) | Web interface and user dashboard |
| Entity Framework Core | Database management |
| Bootstrap | Responsive UI design |

---

## ⚙️ How It Works

```
YouTube URL
    ↓
Extract Audio (FFmpeg)
    ↓
Transcribe Audio to Text (Whisper AI)
    ↓
Summarize Text (AI Model)
    ↓
Display Transcript + Summary
```

---

## 🚀 Getting Started

### Prerequisites
- Python 3.9+
- .NET 6+
- FFmpeg installed on your system

### 1️⃣ Run the Django Backend (API)

```bash
# Navigate to the API directory
cd API

# Install dependencies
pip install -r requirements.txt

# Run the server
python manage.py runserver
```

### 2️⃣ Run the ASP.NET Frontend

```bash
# Navigate to the web app directory
cd VideoSummarizationWeb

# Run the application
dotnet run
```

### 3️⃣ Open in Browser

```
http://localhost:7016
```


---

## 📄 License

This project is licensed under the MIT License.

---

> Built with ❤️ as a graduation project — Jordan, 2025
