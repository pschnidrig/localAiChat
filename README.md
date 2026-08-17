# LocalAI — Blazor + LM Studio Chat

A minimal Blazor Server app that streams responses from a locally running LLM via [LM Studio](https://lmstudio.ai/). Nothing leaves your machine.

---

## What it is

- **Blazor Server** app (.NET 10)
- Talks to **LM Studio's local OpenAI-compatible server**
- Sends the full conversation and displays the complete response when ready
- Keeps full conversation history per session

---

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [LM Studio](https://lmstudio.ai/) with at least one model downloaded

---

## Step 1 — Start LM Studio

1. Open LM Studio
2. Load any model (e.g. `qwen2.5-coder-1.5b-instruct`)
3. Go to the **Local Server** tab and click **Start Server**
4. Note the port — default is `1234`

---

## Step 2 — Configure the app

Open `LocalAI/appsettings.json` and update the `LmStudio` block:

```json
"LmStudio": {
  "Endpoint": "http://localhost:1234/v1",
  "ModelName": "local-model",
  "ApiKey": "lm-studio"
}
```

| Setting | What to change |
|---|---|
| `Endpoint` | Change the port if LM Studio runs on something other than `1234` |
| `ModelName` | Leave as `local-model` — LM Studio ignores this and uses whatever model is loaded |
| `ApiKey` | Leave as-is — LM Studio doesn't validate the key, any value works |

---

## Step 3 — Run the app

```bash
cd LocalAI
dotnet run
```

Open your browser at `https://localhost:PORT` and start chatting.

---

## Project structure

```
LocalAI/
├── Configuration/
│   └── LmStudioOptions.cs   # Strongly-typed config model
├── Components/
│   ├── Pages/
│   │   └── Home.razor       # Chat UI + response logic
│   ├── _Imports.razor       # Global usings
│   └── ...
├── appsettings.json         # LM Studio connection settings
└── Program.cs               # Registers IChatClient with DI
```

---

## Troubleshooting

**"Connection Error" on first message**
→ Make sure LM Studio's local server is running and the port in `appsettings.json` matches.

**App crashes after response (circuit error)**
→ Make sure you're on the latest code — this was a known issue with `StateHasChanged` being called off the Blazor circuit thread.

**Model not responding**
→ In LM Studio, check that a model is actually loaded (not just downloaded) before starting the server.
