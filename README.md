# ChatBotAI

## Overview
**ChatBotAI** is a console-based interactive cybersecurity awareness chatbot designed to educate users about various cybersecurity topics. The chatbot offers dynamic responses based on user input, remembers user preferences, detects sentiment, and provides helpful tips and explanations to enhance cybersecurity knowledge in an engaging way.

## Features

- **Topic Menu:** Presents a list of cybersecurity topics the user can explore.
- **Memory and Recall:** Users can tell the chatbot what they like, and the chatbot remembers it during the session.
- **Sentiment Detection:** Detects positive, negative, and neutral user emotions and responds empathetically.
- **Dynamic Conversation:** Responds intelligently to requests for more information, clarification, and other conversational prompts.
- **Exit Command:** Users can type `exit` to gracefully end the session.
- **Colored Console Output:** Enhances user experience by using different colors for different message types.
- **User Input Validation:** Handles empty or unclear inputs with appropriate prompts.

## Technologies Used

- **C# (.NET Core/Framework)**
- Console application for cross-platform command-line interaction.
- Utilizes basic collections (`Dictionary`, `List`) and LINQ for data filtering.

## Project Structure

- `MenuDisplay.cs` - Main interaction logic and user interface.
- `Library.cs` - Data storage and retrieval of cybersecurity topics and content.
- `Border.cs` - Helper class for console UI styling and color output.
- `Program.cs` - Entry point to start the chatbot.



