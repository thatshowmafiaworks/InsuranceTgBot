# Telegram Insurance Bot

This bot allows users to process insurance applications via Telegram. It handles document photos, converts them to text, allows data editing, facilitates payment, generates the final document, and sends it to the user. All user responses are generated using Google`s Gemini.

Bot is working currently [here](https://t.me/insurance_very_original_bot)

## Table of Contents
- [Description](#description)
- [Requirements](#requirements)
- [Setup](#setup)
- [Running the Bot](#running-the-bot)
- [Usage](#usage)

## Description
This Telegram bot is designed to automate the process of handling insurance policies. The bot handles:
- Document photos
- Converting documents to text
- User data editing
- Final document generation
- Payment processing via an integrated payment system

## Requirements
Before running the bot, make sure you have:
- .NET 9.0
- Telegram Bot Token
- Gemini Api Token

## Setup

1. **Obtain a bot token from BotFather:**
   Go to [BotFather](https://core.telegram.org/bots#botfather) on Telegram and create a new bot. Save the token you receive.

2. **Set up your configuration:**
   Create an `appsettings.json` file in the project root directory to store the Telegram bot token and other settings. Example:

   ```json
   {
     "TelegramBotToken": "your-telegram-bot-token",
     "BotWebHookUrl": "url-to-your-webhook",
     "ConnectionString": "connection-string-to-your-db",
     "GeminiApiKey": "api-key-to-gemini-api"
   }
3. **Set up webhooks for Telegram: Just go to your domain**

## Running the Bot

Clone the repository:

  ```bash
  git clone https://github.com/yourusername/telegram-insurance-bot.git
  cd telegram-insurance-bot
  ```

Install the dependencies:

  ```bash
  dotnet restore
  ```
Run the project:

  ```bash
  dotnet run
  ```

The bot will start and wait for messages via Telegram.

## Usage

Once the bot is running, you can interact with it in Telegram. Here are some commands:

/start — start the insurance application process.
