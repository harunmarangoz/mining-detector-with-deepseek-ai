# Crypto Mining Detector with AI Demo

Bu proje, `.NET 9`, **Ollama** ve **DeepSeek-R1** kullanarak log kayıtlarında kripto madenciliği (crypto mining) faaliyetlerini tespit etmeye yönelik bir demo uygulamadır.


### Ollama ve DeepSeek-R1 Kurulumu
1. [Ollama](https://ollama.com/)'yı resmi sitesinden indirin ve kurun.
2. Terminalden modeli indirin:```ollama pull eepseek-r1:7b```
> Daha güçlü bir model için: deepseek-r1:32b
3. Modelin yüklendiğini kontrol etmek için şu adresi ziyaret edin: ```http://localhost:11434/api/tags```

### Proje Kurulumu
1. `git clone https://github.com/harunmarangoz/mining-detector-with-deepseek-ai.git`
2. `cd mining-detector-with-deepseek-ai`
3. `dotnet run`
4. Tarayıcıda `http://localhost:5227/swagger` adresine gidin.

### Kullanım
`/api/ai-analyse/file` endpoint'ine log dosyanızla birlikte POST isteği göndererek analiz başlatabilirsiniz.

prompt hakkında detaylı bilgi için [PROMPT.md](PROMPT.md) dosyasına bakabilirsiniz.

