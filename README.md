# auto_ai_c
日本語版READMEは[こちら](https://github.com/keikyufun/auto_ai_c/blob/main/README_ja.md)
## Description
This tool automates Microsoft Copilot.  
It only works on **Windows (x64)**.  
The following AI service is supported:

- [Microsoft Copilot](https://copilot.microsoft.com)

Below is an explanation of Copilot and its terms of use.
## About Copilot Terms of Use
This tool is designed **in full compliance** with the official  
[Microsoft Copilot Terms of Use](https://www.microsoft.com/en-us/microsoft-copilot/for-individuals/termsofuse).

> Do not use tools or computer programs (such as bots or scrapers) to access Copilot.  
> Copilot is for personal use only.

This tool does **not** violate the terms because:

- It is used **only for personal use**  
- It is **not exposed to the cloud** and is **not provided as an external service**  
- It does **not distribute Copilot’s output to third parties**  
- It does **not attempt to bypass Copilot’s restrictions or jailbreak it**

Additionally, it complies with the following:

> Do not impair Microsoft’s ability to provide Copilot to you or others.  
> Do not engage in technical attacks, excessive usage, prompt‑based manipulation, jailbreaks, or other misuse.

This tool does **not** remove limitations, bypass security, or interfere with Microsoft’s systems.  
It is simply a **local automation helper** that operates on the user’s own PC.
## auto_ai Terms of Use (for Copilot)
### User Agreement (Tool‑specific)
- Do **not** use this tool to turn Copilot into an API server  
- Do **not** violate the rights or privacy of others  
- Do **not** use this tool for illegal activities  
- Do **not** create or publish Copilot clones or imitation AI services  
- The developer assumes **no responsibility** for any issues or damages caused by using this tool  

**Always follow Copilot’s official Terms of Use.**
### Allowed Use Cases
- Using this tool as a **local automation utility**  
  Example: Running commands or editing files based on Copilot’s responses

- Using Flask to remotely control **your own PC’s Copilot** from your smartphone  
  - Only within LAN or VPN  
  - External/public access is prohibited  

All usage must comply with both Copilot’s terms and this tool’s terms.
### Prohibited Use Cases
- Providing Copilot functionality over the cloud  
- Distributing Copilot’s output to third parties  
- Turning Copilot into an external API service  
- Attempting to jailbreak or bypass Copilot’s restrictions  

All usage must comply with both Copilot’s terms and this tool’s terms.
## Usage
(Assuming the files are placed directly under the C drive.)
### argument.exe
Sends a prompt to Copilot.
On Windows CMD:
```terminal
cd C:\
argument.exe "your prompt"
```
On Ubuntu (WSL):
```terminal
cd /mnt/c
argument.exe "your prompt"
```

Make sure the **Copilot tab in Microsoft Edge is open and active**.  
Copilot’s response will be saved as **myfile.txt** in the same directory as the exe.

### clipboard.exe
Sends the current clipboard contents to Copilot.

On Windows CMD:
```terminal
cd C:\
clipboard.exe
```
On Ubuntu (WSL):
```terminal
cd /mnt/c
clipboard.exe
```

Again, ensure the **Copilot tab in Microsoft Edge is open and active**.  
The response will be saved as **myfile.txt** in the same directory as the exe.
